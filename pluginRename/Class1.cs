using System;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Windows;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;
using PluginRename;
//using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.EditorInput;

namespace MyAutoCADDll
{
    public class Commands : IExtensionApplication
    {
        string configTempFileName = "configFileNameXls.tmp";
        string fileNameXls = "No file";

        // функция инициализации (выполняется при загрузке плагина)
        public void Initialize()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.Idle += new EventHandler(on_ApplicationIdle);
            //MessageBox.Show("Плагин загружен");

            // инициализируем временный файл для храненния пути к файлу таблицы Excel
            // (и возможно других настроек в будущем)
            if (File.Exists(Path.GetTempPath() + configTempFileName))
            {
                File.Delete(Path.GetTempPath() + configTempFileName);
            }

            using (var stream = File.Open(Path.GetTempPath() + configTempFileName, FileMode.Create, FileAccess.Write))
            {
                StreamWriter output = new StreamWriter(stream);
                output.Write(fileNameXls);
                output.Close();
            }
        }


        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            if (File.Exists(Path.GetTempPath() + configTempFileName))
            {
                File.Delete(Path.GetTempPath() + configTempFileName);
            }
        }


        public void on_ApplicationIdle(object sender, EventArgs e)
        {
            addRibbonTab();
        }


        public void addRibbonTab()
        {
            RibbonControl rbCtrl = ComponentManager.Ribbon;
            if (rbCtrl != null)
            {
                // Добавление вкладки

                createMyTab();

                Autodesk.AutoCAD.ApplicationServices.Application.Idle -= on_ApplicationIdle;
            }
        }


        [CommandMethod("CreateMyTab")]
        public void createMyTab()
        {
            // создаем кнопку В чертеже
            Autodesk.Windows.RibbonButton buttonInDrawing = new Autodesk.Windows.RibbonButton();
            buttonInDrawing.Name = "Замена";
            buttonInDrawing.Text = "Замена";
            buttonInDrawing.Id = "_buttonChangeTextInDrawing";
            buttonInDrawing.ShowText = true;
            buttonInDrawing.Size = RibbonItemSize.Large;
            buttonInDrawing.Orientation = System.Windows.Controls.Orientation.Horizontal;
            buttonInDrawing.LargeImage = getBitmap("button_my_plugin_32.png");
            buttonInDrawing.ShowImage = true;
            //buttonInDrawing.Orientation = 
            // привязываем к кнопке обработчик нажатия
            buttonInDrawing.CommandHandler = new CommandHandler_buttonInDrawing();

            RibbonRowPanel rowPanel = new RibbonRowPanel();
            rowPanel.Items.Add(buttonInDrawing);

            // создаем контейнер для элементов
            Autodesk.Windows.RibbonPanelSource panelSource = new Autodesk.Windows.RibbonPanelSource();
            panelSource.Items.Add(rowPanel);
            panelSource.Id = "_panelSource";
            panelSource.Title = "Текст в чертежах";

            // создаем панель
            RibbonPanel rbPanel = new RibbonPanel();
            // добавляем на панель контейнер для элементов
            rbPanel.Source = panelSource;

            // создаем вкладку
            RibbonTab rbTab = new RibbonTab();
            rbTab.Title = "Дополнения";
            rbTab.Id = "MyPlugins";
            // добавляем на вкладку панель
            rbTab.Panels.Add(rbPanel);

            // получаем указатель на ленту AutoCAD
            Autodesk.Windows.RibbonControl rbCtrl = ComponentManager.Ribbon;
            // добавляем на ленту вкладку
            rbCtrl.Tabs.Add(rbTab);
            // делаем созданную вкладку активной ("выбранной")
            //rbTab.IsActive = true;
        }


        // обработчик нажатия кнопки В чертеже
        public class CommandHandler_buttonInDrawing : System.Windows.Input.ICommand
        {
            public event EventHandler CanExecuteChanged;
            public bool CanExecute(object param)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                FormMyPlugin formMyPlugin = new FormMyPlugin();
                //formMyPlugin.Show();
                acad.ShowModalDialog(acad.MainWindow.Handle, formMyPlugin, false);
            }
        }


        BitmapImage getBitmap(string fileName)

        {

            BitmapImage bmp = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.             
            bmp.BeginInit();
            bmp.UriSource = new Uri(string.Format(
              "pack://application:,,,/{0};component/Resources/{1}",
              Assembly.GetExecutingAssembly().GetName().Name,
              fileName));
            bmp.EndInit();

            return bmp;
        }
    }
}