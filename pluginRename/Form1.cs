using System;
using System.IO;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Windows;
using ExcelDataReader;
using System.Data;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;

namespace PluginRename
{
    public partial class FormMyPlugin : Form
    {
        enum ReplaseWorkMode { SingleMode, MultiplieMode };
        byte flagSingleOrMultiplieWork;      // SingleMode - работа в текущем чертеже,
                                             // MultiplieMode - работа по таблице Excel
        string fileNameXls;
        ToolTip fileNameXlsToolTip;

        int counterReplaceObjects;

        string configTempFileName = "configFileNameXls.tmp";

        // Инициализация
        public FormMyPlugin()
        {
            InitializeComponent();

            // загружаем путь к файлу таблице Excel для текущей сессии работы в Autocad
            // из временного файла
            fileNameXls = loadFileNameXls();
            labelSelectXlsFile.Text = fileNameXls;

            if (fileNameXls == "No file")
            {
                flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.SingleMode;
            }
            else
            {
                flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.MultiplieMode;
                textBoxOldText.Enabled = false;
                textBoxNewText.Enabled = false;
            }
            fileNameXlsToolTip = new ToolTip();
            createToolTip(labelSelectXlsFile, fileNameXls);

            counterReplaceObjects = 0;          //счетчик изменённых объектов
        }


        // Событие кнопки отмены
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Событие кнопки старта работы
        private void buttonStart_Click(object sender, EventArgs e)
        {
            replaceForMyText();
        }


        // Запуск работы, закрытие формы по окончанию, перерисовка окна Autocad
        public void replaceForMyText()
        {
            counterReplaceObjects = 0;
            if (flagSingleOrMultiplieWork == (byte)ReplaseWorkMode.SingleMode)
            {
                iterateThroughAllObjects(textBoxOldText.Text, textBoxNewText.Text);
            }
            else if (flagSingleOrMultiplieWork == (byte)ReplaseWorkMode.MultiplieMode)
            {
                workWithTableXls();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так. Ошибка в выборе режима работы.");
            }

            this.Close();
            acad.DocumentManager.MdiActiveDocument.Editor.Regen();
            MessageBox.Show("Изменено " + counterReplaceObjects + " объектов.", "Результаты.");
        }


        // Основная работа по замене строки
        public void iterateThroughAllObjects(string sampleOldText, string sampleNewText)
        {
            // получаем текущую БД 
            Database db = HostApplicationServices.WorkingDatabase;

            using (acad.DocumentManager.MdiActiveDocument.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    // получаем ссылку на пространство модели (ModelSpace)
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(db), OpenMode.ForRead);

                    // "пробегаем" по всем объектам в пространстве модели
                    foreach (ObjectId id in ms)
                    {
                        // DBText
                        if (id.ObjectClass == RXObject.GetClass(typeof(DBText)))
                        {
                            var text = (DBText)tr.GetObject(id, OpenMode.ForRead);
                            // Высоту текста потом вернем, почему-то меняется
                            var heightText = text.Height;
                            if (text.TextString.Contains(sampleOldText))
                            {
                                //Меняем в промежуточной переменной, напрямую не работает
                                var textOld = text.TextString;
                                var textNew = textOld.Replace(sampleOldText, sampleNewText);
                                //Подгоняем размер по длине под старый
                                var lengthOld = textOld.Length;
                                var lengthNew = textNew.Length;
                                double scaleText = (double)text.WidthFactor * (double)lengthOld / (double)lengthNew;

                                //Запись в объект
                                tr.GetObject(id, OpenMode.ForWrite);
                                text.TextString = textNew;
                                if (checkBoxScaleText.Checked == true)
                                    text.WidthFactor = scaleText;
                                text.Height = heightText;

                                counterReplaceObjects++;
                            }
                        }

                        // MText

                        if (id.ObjectClass == RXObject.GetClass(typeof(MText)))
                        {
                            var text = (MText)tr.GetObject(id, OpenMode.ForRead);
                            // Высоту текста потом вернем, почему-то меняется
                            var heightText = text.Height;
                            if (text.Text.Contains(sampleOldText))
                            {
                                //Достаем значение масштабирования после /W
                                string textWithCodes = text.Contents;
                                string widthScale = "";
                                if (textWithCodes.Contains("\\W"))
                                {
                                    widthScale = textWithCodes.Substring(textWithCodes.IndexOf('W') + 1, (textWithCodes.IndexOf(';') - (textWithCodes.IndexOf('W')) - 1));
                                }
                                else
                                    widthScale = "1.0";

                                //Меняем сам текст
                                string textOld = text.Text;
                                string textNew = textOld.Replace(sampleOldText, sampleNewText);

                                // Меняем масштаб если изменилось количество знаков
                                float delta = 1;
                                if (textOld.Length != textNew.Length)
                                {
                                    delta = (float)textOld.Length / (float)textNew.Length;
                                }
                                widthScale = (float.Parse(widthScale) * delta).ToString();

                                //Составляем новую строку MText
                                if (checkBoxScaleText.Checked == true)
                                    textNew = "\\A1;{\\W" + widthScale + ";" + textNew + "}";
                                else
                                    textNew = "\\A1;{\\W1.0;" + textNew + "}";

                                //Запись в объект
                                tr.GetObject(id, OpenMode.ForWrite);
                                text.Contents = textNew;
                                text.Height = heightText;

                                counterReplaceObjects++;
                            }
                        }
                    }

                    tr.Commit();
                }
            }
        }


        // Основная работа по замене строки по таблице .xls(x)
        public void workWithTableXls()
        {
            using (var stream = File.Open(fileNameXls, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    var results = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables

                    // Работаем только с одной таблицей
                    if (results.Tables.Count == 1)
                    {
                        foreach (System.Data.DataTable table in results.Tables)
                        {
                            if (table.Columns.Count > 2)
                                MessageBox.Show("Из таблицы будут использоваться только первые две колонки.", "Предупреждение.");
                            // Колонок в таблице должно быть не меньше двух.
                            if (table.Columns.Count >= 2)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    iterateThroughAllObjects(row[0].ToString(), row[1].ToString());
                                }
                            }
                            else
                                MessageBox.Show("В таблице меньше двух колонок.", "Ошибка!");
                        }
                    }
                    else
                        MessageBox.Show("В файле .xls(x) должна быть одна твблица.", "Ошибка!");
                }
            }
            
        }


        // Диалог выбора файла таблицы Excel
        private void buttonSelectXlsFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Выберите файл таблицы соответствий";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileDialog.Filter = "Файлы Excel (*.xls, *.xlsx, *.xlsb)|*.xls;*.xlsx;*.xlsb";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileNameXls = fileDialog.FileName;
            }
            labelSelectXlsFile.Text = fileNameXls;
            createToolTip(labelSelectXlsFile, fileNameXls);

            if (fileNameXls != "No file")
            {
                flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.MultiplieMode;
                textBoxOldText.Enabled = false;
                textBoxNewText.Enabled = false;
            }

            // сохраняем путь к файлу таблице Excel для текущей сессии работы в Autocad
            // во временном файле
            saveFileNameXls(fileNameXls);
        }


        // Очистка данных о файле таблицы Excel
        private void buttonCancelXlsFile_Click(object sender, EventArgs e)
        {
            fileNameXls = "No file";
            labelSelectXlsFile.Text = fileNameXls;
            createToolTip(labelSelectXlsFile, fileNameXls);

            flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.SingleMode;
            textBoxOldText.Enabled = true;
            textBoxNewText.Enabled = true;

            // сохраняем путь к файлу таблице Excel для текущей сессии работы в Autocad
            // во временном файле
            saveFileNameXls(fileNameXls);
        }


        // Создание подсказки о файле таблицы Excel (если путь к нему очень длинный)
        private void createToolTip(Control controlForToolTip, string toolTipText)
        {
            fileNameXlsToolTip.Active = true;
            fileNameXlsToolTip.SetToolTip(controlForToolTip, toolTipText);
            fileNameXlsToolTip.IsBalloon = true;
        }


        // Событие нажатия клавиши
        private void FormMyPlugin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }


        // сохраняем путь к файлу таблице Excel для текущей сессии работы в Autocad
        private void saveFileNameXls(string text)
        {
            if (File.Exists(Path.GetTempPath() + configTempFileName))
            {
                File.Delete(Path.GetTempPath() + configTempFileName);
            }

            using (var stream = File.Open(Path.GetTempPath() + configTempFileName, FileMode.Create, FileAccess.Write))
            {
                StreamWriter output = new StreamWriter(stream);
                output.Write(text);
                output.Close();
            }
        }


        // загружаем путь к файлу таблице Excel для текущей сессии работы в Autocad
        private string loadFileNameXls()
        {
            string text;
            if (File.Exists(Path.GetTempPath() + configTempFileName))
            {
                using (var stream = File.Open(Path.GetTempPath() + configTempFileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader output = new StreamReader(stream);
                    text = output.ReadToEnd();
                    output.Close();
                }
            }
            else
            {
                text = "No file";
            }
            return text;
        }
    }
}
