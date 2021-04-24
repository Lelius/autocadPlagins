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

        // Инициализация
        public FormMyPlugin()
        {
            InitializeComponent();

            flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.SingleMode;
            fileNameXls = "No file";
            fileNameXlsToolTip = new ToolTip();

            createToolTip(labelSelectXlsFile, fileNameXls);
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
            if (flagSingleOrMultiplieWork == (byte)ReplaseWorkMode.SingleMode)
            {
                iterateThroughAllObjects();
            }
            else if (flagSingleOrMultiplieWork == (byte)ReplaseWorkMode.MultiplieMode)
            {
                workWithTableXls();
            }
            else
                MessageBox.Show("Что-то пошло не так. Ошибка в выборе режима работы.");
            this.Close();
            acad.DocumentManager.MdiActiveDocument.Editor.Regen();
        }


        // Основная работа по замене строки
        public void iterateThroughAllObjects()
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
                            var heightText = text.Height;
                            if (text.TextString.Contains(textBoxOldText.Text))
                            {
                                //Меняем в промежуточной переменной, напрямую не работает
                                var textOld = text.TextString;
                                var textNew = textOld.Replace(textBoxOldText.Text, textBoxNewText.Text);
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
                            }
                        }

                        // MText

                        if (id.ObjectClass == RXObject.GetClass(typeof(MText)))
                        {
                            var text = (MText)tr.GetObject(id, OpenMode.ForRead);
                            var heightText = text.Height;
                            if (text.Text.Contains(textBoxOldText.Text))
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
                                string textNew = textOld.Replace(textBoxOldText.Text, textBoxNewText.Text);

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
                            foreach (DataRow row in table.Rows)
                            {
                                // Колонок в таблице должно быть не меньше двух.
                                if (table.Columns.Count >= 2)
                                {
                                    if (table.Columns.Count > 2)
                                        MessageBox.Show("Из таблицы будут использоваться только первые две колонки.", "Предупреждение.");
                                    foreach (System.Data.DataColumn column in table.Columns)
                                    {
                                        MessageBox.Show(row[column].ToString());
                                    }
                                }
                                else
                                    MessageBox.Show("В таблице меньше двух колонок.", "Ошибка!");
                            }
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

            flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.MultiplieMode;
        }


        // Очистка данных о файле таблицы Excel
        private void buttonCancelXlsFile_Click(object sender, EventArgs e)
        {
            fileNameXls = "No file";
            labelSelectXlsFile.Text = fileNameXls;
            createToolTip(labelSelectXlsFile, fileNameXls);

            flagSingleOrMultiplieWork = (byte)ReplaseWorkMode.SingleMode;
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
    }
}
