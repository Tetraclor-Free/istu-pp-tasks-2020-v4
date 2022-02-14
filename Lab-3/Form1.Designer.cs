
namespace Lab_3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ViewRecordsListBox = new System.Windows.Forms.ListBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CreateReportButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ToNumeric = new System.Windows.Forms.NumericUpDown();
            this.FromNumeric = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewRecordsListBox
            // 
            this.ViewRecordsListBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ViewRecordsListBox.FormattingEnabled = true;
            this.ViewRecordsListBox.ItemHeight = 20;
            this.ViewRecordsListBox.Location = new System.Drawing.Point(0, 0);
            this.ViewRecordsListBox.Name = "ViewRecordsListBox";
            this.ViewRecordsListBox.Size = new System.Drawing.Size(879, 384);
            this.ViewRecordsListBox.TabIndex = 0;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(218, 405);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(160, 40);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Location = new System.Drawing.Point(384, 405);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(160, 40);
            this.ChangeButton.TabIndex = 2;
            this.ChangeButton.Text = "Изменить";
            this.ChangeButton.UseVisualStyleBackColor = true;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(550, 405);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(160, 40);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CreateReportButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ToNumeric);
            this.groupBox1.Controls.Add(this.FromNumeric);
            this.groupBox1.Location = new System.Drawing.Point(12, 469);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(855, 132);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Отчет";
            // 
            // CreateReportButton
            // 
            this.CreateReportButton.Location = new System.Drawing.Point(502, 72);
            this.CreateReportButton.Name = "CreateReportButton";
            this.CreateReportButton.Size = new System.Drawing.Size(160, 40);
            this.CreateReportButton.TabIndex = 5;
            this.CreateReportButton.Text = "Построить отчет";
            this.CreateReportButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "до";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Оклад: от";
            // 
            // ToNumeric
            // 
            this.ToNumeric.Location = new System.Drawing.Point(512, 26);
            this.ToNumeric.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.ToNumeric.Name = "ToNumeric";
            this.ToNumeric.Size = new System.Drawing.Size(150, 27);
            this.ToNumeric.TabIndex = 1;
            // 
            // FromNumeric
            // 
            this.FromNumeric.Location = new System.Drawing.Point(256, 26);
            this.FromNumeric.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.FromNumeric.Name = "FromNumeric";
            this.FromNumeric.Size = new System.Drawing.Size(150, 27);
            this.FromNumeric.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 613);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ViewRecordsListBox);
            this.Name = "Form1";
            this.Text = "Учет сотрудников";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ViewRecordsListBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CreateReportButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ToNumeric;
        private System.Windows.Forms.NumericUpDown FromNumeric;
    }
}

