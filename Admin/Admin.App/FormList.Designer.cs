namespace Admin.App
{
    partial class FormList
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
            dataGridView1 = new DataGridView();
            txtTemplateId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtTemplateOutputDir = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 109);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1054, 256);
            dataGridView1.TabIndex = 0;
            // 
            // txtTemplateId
            // 
            txtTemplateId.Location = new Point(140, 12);
            txtTemplateId.Name = "txtTemplateId";
            txtTemplateId.Size = new Size(108, 31);
            txtTemplateId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 13);
            label1.Name = "label1";
            label1.Size = new Size(104, 25);
            label1.TabIndex = 2;
            label1.Text = "Template Id";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 56);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 4;
            label2.Text = "Output File";
            // 
            // txtTemplateOutputDir
            // 
            txtTemplateOutputDir.Location = new Point(140, 55);
            txtTemplateOutputDir.Name = "txtTemplateOutputDir";
            txtTemplateOutputDir.Size = new Size(590, 31);
            txtTemplateOutputDir.TabIndex = 3;
            // 
            // FormList
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1069, 370);
            Controls.Add(label2);
            Controls.Add(txtTemplateOutputDir);
            Controls.Add(label1);
            Controls.Add(txtTemplateId);
            Controls.Add(dataGridView1);
            Name = "FormList";
            Text = "FormList";
            FormClosed += FormList_FormClosed;
            Load += FormList_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox txtTemplateId;
        private Label label1;
        private Label label2;
        private TextBox txtTemplateOutputDir;
    }
}