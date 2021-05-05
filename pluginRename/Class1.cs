using System;
using System.Windows.Forms;
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
        // функция инициализации (выполняется при загрузке плагина)
        public void Initialize()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.Idle += new EventHandler(on_ApplicationIdle);
            //MessageBox.Show("Плагин загружен");
        }


        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            MessageBox.Show("Goodbye!");
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
            buttonInDrawing.Name = "В чертеже";
            buttonInDrawing.Text = "В чертеже";
            buttonInDrawing.Id = "_buttonChangeTextInDrawing";
            buttonInDrawing.ShowText = true;
            buttonInDrawing.Size = RibbonItemSize.Large;
            //buttonInDrawing.Orientation = 
            // привязываем к кнопке обработчик нажатия
            buttonInDrawing.CommandHandler = new CommandHandler_buttonInDrawing();

            // создаем контейнер для элементов
            Autodesk.Windows.RibbonPanelSource pS = new Autodesk.Windows.RibbonPanelSource();
            pS.Items.Add(buttonInDrawing);
            pS.Id = "_panelSource";
            pS.Title = "Замена текста в строках";

            // создаем панель
            RibbonPanel rbPanel = new RibbonPanel();
            // добавляем на панель контейнер для элементов
            rbPanel.Source = pS;

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
    }
}