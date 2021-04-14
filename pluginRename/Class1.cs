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
            MessageBox.Show("Плагин загружен.");
            createMyTab();
        }


        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            MessageBox.Show("Goodbye!");
        }


        // эта функция будет вызываться при выполнении в AutoCAD команды «TestCommand»
        [CommandMethod("TestCommand")]
        public void MyCommand()
        {
            MessageBox.Show("Habr!");
        }


        [CommandMethod("Habr_IterateThroughAllObjects_1")]
        public void iterateThroughAllObjects()
        {
            // получаем текущую БД 
            Database db = HostApplicationServices.WorkingDatabase;

            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // получаем ссылку на пространство модели (ModelSpace)
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(db), OpenMode.ForRead);

                // "пробегаем" по всем объектам в пространстве модели
                foreach (ObjectId id in ms)
                {
                    // приводим каждый из них к типу Entity
                    Entity entity = (Entity)tr.GetObject(id, OpenMode.ForRead);

                    // выводим в консоль слой (entity.Layer), тип (entity.GetType().ToString()) и цвет (entity.Color) каждого объекта
                    acad.DocumentManager.MdiActiveDocument.Editor.WriteMessage(string.Format("\nLayer:{0}; Type:{1}; Color: {2},{3},{4}\n",
                        entity.Layer, entity.GetType().ToString(), entity.Color.Red.ToString(), entity.Color.Green.ToString(), entity.Color.Blue.ToString()));

                    if (entity.GetType().ToString() == "Autodesk.AutoCAD.DatabaseServices.MText")
                    {
                        MText text = (MText)tr.GetObject(id, OpenMode.ForRead);
                        acad.DocumentManager.MdiActiveDocument.Editor.WriteMessage(text.getMTextWithFieldCodes().ToString());
                        acad.DocumentManager.MdiActiveDocument.Editor.WriteMessage(text.Text);
                    }
                    if (entity.GetType().ToString() == "Autodesk.AutoCAD.DatabaseServices.DBText")
                    {
                        DBText text = (DBText)tr.GetObject(id, OpenMode.ForRead);
                        acad.DocumentManager.MdiActiveDocument.Editor.WriteMessage(text.getTextWithFieldCodes().ToString());
                    }
                }

                tr.Commit();
            }
        }


        [CommandMethod("CreateMyTab")]
        public void createMyTab()
        {
            // создаем выпадающий список
            //Autodesk.Windows.RibbonCombo comboBox1 = new RibbonCombo();
            //comboBox1.Id = "_combobox1";

            // создаем кнопку В чертеже
            Autodesk.Windows.RibbonButton buttonInDrawing = new Autodesk.Windows.RibbonButton();
            buttonInDrawing.Name = "В чертеже";
            buttonInDrawing.Text = "В чертеже";
            buttonInDrawing.Id = "_buttonChangeTextInDrawing";
            buttonInDrawing.ShowText = true;
            //buttonInDrawing.Orientation = 
            // привязываем к кнопке обработчик нажатия
            buttonInDrawing.CommandHandler = new CommandHandler_buttonInDrawing();

            // создаем кнопку В имени файла
            Autodesk.Windows.RibbonButton buttonInFileName = new Autodesk.Windows.RibbonButton();
            buttonInFileName.Name = "В имени файла";
            buttonInFileName.Text = "В имени файла";
            buttonInFileName.Id = "_buttonChangeTextInFileName";
            buttonInFileName.ShowText = true;
            // привязываем к кнопке обработчик нажатия
            buttonInFileName.CommandHandler = new CommandHandler_buttonInFileName();

            Autodesk.Windows.RibbonFlowPanel flowPanel = new RibbonFlowPanel();
            flowPanel.Items.Add(buttonInDrawing);
            flowPanel.Items.Add(buttonInFileName);
            flowPanel.Id = "_flowPanel";

            // создаем контейнер для элементов
            Autodesk.Windows.RibbonPanelSource rbPanelSource = new Autodesk.Windows.RibbonPanelSource();
            rbPanelSource.Title = "Замена текста в строках";
            // добавляем в контейнер элементы управления
            //rbPanelSource.Items.Add(comboBox1);
            //rbPanelSource.Items.Add(new RibbonSeparator());
            rbPanelSource.Items.Add(flowPanel);

            // создаем панель
            RibbonPanel rbPanel = new RibbonPanel();
            // добавляем на панель контейнер для элементов
            rbPanel.Source = rbPanelSource;

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

        // обработчик нажатия кнопки В имени файла
        public class CommandHandler_buttonInFileName : System.Windows.Input.ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object param)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                MessageBox.Show("Не реализовано.");
            }
        }


    }
}