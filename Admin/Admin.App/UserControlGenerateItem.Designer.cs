namespace CodeAdmin
{
    partial class UserControlGenerateItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTemplate = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtOutputFolder = new TextBox();
            btnExecuteItem = new Button();
            btnRollBackItem = new Button();
            lblItem = new Label();
            btnWriters = new Button();
            SuspendLayout();
            // 
            // txtTemplate
            // 
            txtTemplate.Font = new Font("Segoe UI", 8F);
            txtTemplate.ForeColor = Color.Firebrick;
            txtTemplate.Location = new Point(118, 8);
            txtTemplate.Name = "txtTemplate";
            txtTemplate.Size = new Size(664, 29);
            txtTemplate.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8F);
            label1.Location = new Point(9, 11);
            label1.Name = "label1";
            label1.Size = new Size(72, 21);
            label1.TabIndex = 1;
            label1.Text = "Template";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F);
            label2.Location = new Point(9, 47);
            label2.Name = "label2";
            label2.Size = new Size(107, 21);
            label2.TabIndex = 3;
            label2.Text = "Output Folder";
            // 
            // txtOutputFolder
            // 
            txtOutputFolder.Font = new Font("Segoe UI", 8F);
            txtOutputFolder.ForeColor = Color.Blue;
            txtOutputFolder.Location = new Point(118, 44);
            txtOutputFolder.Name = "txtOutputFolder";
            txtOutputFolder.Size = new Size(664, 29);
            txtOutputFolder.TabIndex = 2;
            // 
            // btnExecuteItem
            // 
            btnExecuteItem.Location = new Point(789, 41);
            btnExecuteItem.Name = "btnExecuteItem";
            btnExecuteItem.Size = new Size(98, 34);
            btnExecuteItem.TabIndex = 4;
            btnExecuteItem.Text = "Execute";
            btnExecuteItem.UseVisualStyleBackColor = true;
            btnExecuteItem.Click += btnExecuteItem_Click;
            // 
            // btnRollBackItem
            // 
            btnRollBackItem.Location = new Point(896, 41);
            btnRollBackItem.Name = "btnRollBackItem";
            btnRollBackItem.Size = new Size(98, 34);
            btnRollBackItem.TabIndex = 5;
            btnRollBackItem.Text = "Roll Back";
            btnRollBackItem.UseVisualStyleBackColor = true;
            btnRollBackItem.Click += btnRollBackItem_Click;
            // 
            // lblItem
            // 
            lblItem.AutoSize = true;
            lblItem.ForeColor = Color.Blue;
            lblItem.Location = new Point(794, 6);
            lblItem.Name = "lblItem";
            lblItem.Size = new Size(48, 25);
            lblItem.TabIndex = 6;
            lblItem.Text = "Item";
            // 
            // btnWriters
            // 
            btnWriters.Location = new Point(1004, 43);
            btnWriters.Name = "btnWriters";
            btnWriters.Size = new Size(120, 30);
            btnWriters.TabIndex = 7;
            btnWriters.Text = "Writers";
            btnWriters.UseVisualStyleBackColor = true;
            btnWriters.Click += btnWriters_Click;
            // 
            // UserControlGenerateItem
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            Controls.Add(btnWriters);
            Controls.Add(lblItem);
            Controls.Add(btnRollBackItem);
            Controls.Add(btnExecuteItem);
            Controls.Add(label2);
            Controls.Add(txtOutputFolder);
            Controls.Add(label1);
            Controls.Add(txtTemplate);
            Name = "UserControlGenerateItem";
            Size = new Size(1240, 79);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTemplate;
        private Label label1;
        private Label label2;
        private TextBox txtOutputFolder;
        private Button btnExecuteItem;
        private Button btnRollBackItem;
        private Label lblItem;
        private Button btnWriters;
    }
}
