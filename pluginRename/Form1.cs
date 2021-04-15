using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Windows;
using acad = Autodesk.AutoCAD.ApplicationServices.Application;

namespace PluginRename
{
    public partial class FormMyPlugin : Form
    {
        public FormMyPlugin()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            replaceForMyText();
        }

        public void replaceForMyText()
        {
            //MessageBox.Show(this.textBox1.Text);
            iterateThroughAllObjects();
            this.Close();
            acad.DocumentManager.MdiActiveDocument.Editor.Regen();
            //utodesk.AutoCAD.ApplicationServices.Document doc = acad.DocumentManager.MdiActiveDocument;
            //doc.SendStringToExecute("REGENALL ", true, false, true);
        }

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
                            if (text.TextString.Contains(textBox1.Text))
                            {
                                //Меняем в промежуточной переменной, напрямую не работает
                                var textOld = text.TextString;
                                var textNew = textOld.Replace(textBox1.Text, textBox2.Text);
                                //Подгоняем размер по длине под старый
                                var lengthOld = textOld.Length;
                                var lengthNew = textNew.Length;
                                double scaleText = (double)text.WidthFactor * (double)lengthOld / (double)lengthNew;

                                tr.GetObject(id, OpenMode.ForWrite);
                                text.TextString = textNew;
                                text.WidthFactor = scaleText;
                            }
                        }
                        // MText
                        if (id.ObjectClass == RXObject.GetClass(typeof(MText)))
                        {
                            var text = (MText)tr.GetObject(id, OpenMode.ForRead);
                            if (text.Text.Contains(textBox1.Text))
                            {
                                tr.GetObject(id, OpenMode.ForWrite);
                                text.Contents = textBox2.Text;
                            }
                        }
                    }

                    tr.Commit();
                }
            }
        }
    }
}
