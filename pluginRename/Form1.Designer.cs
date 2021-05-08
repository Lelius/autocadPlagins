
namespace PluginRename
{
    partial class FormMyPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelOldText = new System.Windows.Forms.Label();
            this.labelNewText = new System.Windows.Forms.Label();
            this.textBoxOldText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStartReplace = new System.Windows.Forms.Button();
            this.buttonCancelReplace = new System.Windows.Forms.Button();
            this.checkBoxScaleText = new System.Windows.Forms.CheckBox();
            this.labelXls = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSelectXlsFile = new System.Windows.Forms.Button();
            this.buttonCancelXlsFile = new System.Windows.Forms.Button();
            this.labelSelectXlsFile = new System.Windows.Forms.Label();
            this.textBoxNewText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.97248F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.02752F));
            this.tableLayoutPanel1.Controls.Add(this.labelOldText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelNewText, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxOldText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxScaleText, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelXls, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNewText, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(682, 403);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelOldText
            // 
            this.labelOldText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOldText.AutoSize = true;
            this.labelOldText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOldText.Location = new System.Drawing.Point(79, 41);
            this.labelOldText.Name = "labelOldText";
            this.labelOldText.Size = new System.Drawing.Size(163, 20);
            this.labelOldText.TabIndex = 2;
            this.labelOldText.Text = "Какой текст ищем";
            // 
            // labelNewText
            // 
            this.labelNewText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNewText.AutoSize = true;
            this.labelNewText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNewText.Location = new System.Drawing.Point(84, 141);
            this.labelNewText.Name = "labelNewText";
            this.labelNewText.Size = new System.Drawing.Size(154, 20);
            this.labelNewText.TabIndex = 3;
            this.labelNewText.Text = "На какой меняем";
            // 
            // textBoxOldText
            // 
            this.textBoxOldText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOldText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxOldText.Location = new System.Drawing.Point(332, 13);
            this.textBoxOldText.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxOldText.MinimumSize = new System.Drawing.Size(200, 50);
            this.textBoxOldText.Multiline = true;
            this.textBoxOldText.Name = "textBoxOldText";
            this.textBoxOldText.Size = new System.Drawing.Size(337, 77);
            this.textBoxOldText.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonStartReplace, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonCancelReplace, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(325, 306);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(351, 91);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // buttonStartReplace
            // 
            this.buttonStartReplace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStartReplace.AutoSize = true;
            this.buttonStartReplace.BackColor = System.Drawing.Color.Honeydew;
            this.buttonStartReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartReplace.Location = new System.Drawing.Point(50, 30);
            this.buttonStartReplace.Name = "buttonStartReplace";
            this.buttonStartReplace.Size = new System.Drawing.Size(75, 30);
            this.buttonStartReplace.TabIndex = 0;
            this.buttonStartReplace.Text = "Старт";
            this.buttonStartReplace.UseVisualStyleBackColor = false;
            this.buttonStartReplace.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCancelReplace
            // 
            this.buttonCancelReplace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancelReplace.AutoSize = true;
            this.buttonCancelReplace.BackColor = System.Drawing.Color.MistyRose;
            this.buttonCancelReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancelReplace.Location = new System.Drawing.Point(220, 30);
            this.buttonCancelReplace.Name = "buttonCancelReplace";
            this.buttonCancelReplace.Size = new System.Drawing.Size(84, 30);
            this.buttonCancelReplace.TabIndex = 1;
            this.buttonCancelReplace.Text = "Отмена";
            this.buttonCancelReplace.UseVisualStyleBackColor = false;
            this.buttonCancelReplace.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxScaleText
            // 
            this.checkBoxScaleText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxScaleText.AutoSize = true;
            this.checkBoxScaleText.Checked = true;
            this.checkBoxScaleText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxScaleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxScaleText.Location = new System.Drawing.Point(76, 340);
            this.checkBoxScaleText.Name = "checkBoxScaleText";
            this.checkBoxScaleText.Size = new System.Drawing.Size(170, 22);
            this.checkBoxScaleText.TabIndex = 5;
            this.checkBoxScaleText.Text = "Сохранять масштаб";
            this.checkBoxScaleText.UseVisualStyleBackColor = true;
            // 
            // labelXls
            // 
            this.labelXls.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelXls.AutoSize = true;
            this.labelXls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelXls.Location = new System.Drawing.Point(49, 241);
            this.labelXls.Name = "labelXls";
            this.labelXls.Size = new System.Drawing.Size(223, 20);
            this.labelXls.TabIndex = 6;
            this.labelXls.Text = "Замена по таблице .xls(x)";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelSelectXlsFile, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(325, 206);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(351, 91);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.buttonSelectXlsFile, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonCancelXlsFile, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(341, 36);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // buttonSelectXlsFile
            // 
            this.buttonSelectXlsFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSelectXlsFile.AutoSize = true;
            this.buttonSelectXlsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectXlsFile.Location = new System.Drawing.Point(47, 3);
            this.buttonSelectXlsFile.Name = "buttonSelectXlsFile";
            this.buttonSelectXlsFile.Size = new System.Drawing.Size(75, 30);
            this.buttonSelectXlsFile.TabIndex = 0;
            this.buttonSelectXlsFile.Text = "Выбор";
            this.buttonSelectXlsFile.UseVisualStyleBackColor = true;
            this.buttonSelectXlsFile.Click += new System.EventHandler(this.buttonSelectXlsFile_Click);
            // 
            // buttonCancelXlsFile
            // 
            this.buttonCancelXlsFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCancelXlsFile.AutoSize = true;
            this.buttonCancelXlsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancelXlsFile.Location = new System.Drawing.Point(218, 3);
            this.buttonCancelXlsFile.Name = "buttonCancelXlsFile";
            this.buttonCancelXlsFile.Size = new System.Drawing.Size(75, 30);
            this.buttonCancelXlsFile.TabIndex = 1;
            this.buttonCancelXlsFile.Text = "Сброс";
            this.buttonCancelXlsFile.UseVisualStyleBackColor = true;
            this.buttonCancelXlsFile.Click += new System.EventHandler(this.buttonCancelXlsFile_Click);
            // 
            // labelSelectXlsFile
            // 
            this.labelSelectXlsFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectXlsFile.AutoSize = true;
            this.labelSelectXlsFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSelectXlsFile.Location = new System.Drawing.Point(150, 58);
            this.labelSelectXlsFile.Name = "labelSelectXlsFile";
            this.labelSelectXlsFile.Size = new System.Drawing.Size(50, 18);
            this.labelSelectXlsFile.TabIndex = 1;
            this.labelSelectXlsFile.Text = "No file";
            // 
            // textBoxNewText
            // 
            this.textBoxNewText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNewText.Location = new System.Drawing.Point(332, 113);
            this.textBoxNewText.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxNewText.MinimumSize = new System.Drawing.Size(200, 50);
            this.textBoxNewText.Multiline = true;
            this.textBoxNewText.Name = "textBoxNewText";
            this.textBoxNewText.Size = new System.Drawing.Size(337, 77);
            this.textBoxNewText.TabIndex = 1;
            // 
            // FormMyPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(682, 403);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "FormMyPlugin";
            this.Text = "Замена текста в строках";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMyPlugin_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxNewText;
        private System.Windows.Forms.Label labelOldText;
        private System.Windows.Forms.Label labelNewText;
        private System.Windows.Forms.TextBox textBoxOldText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonStartReplace;
        private System.Windows.Forms.Button buttonCancelReplace;
        private System.Windows.Forms.CheckBox checkBoxScaleText;
        private System.Windows.Forms.Label labelXls;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button buttonSelectXlsFile;
        private System.Windows.Forms.Button buttonCancelXlsFile;
        private System.Windows.Forms.Label labelSelectXlsFile;
    }
}