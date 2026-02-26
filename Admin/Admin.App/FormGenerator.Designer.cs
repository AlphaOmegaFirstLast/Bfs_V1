namespace CodeAdmin
{
    partial class FormGenerator
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
            panelBackend = new TableLayoutPanel();
            panelFrontend = new TableLayoutPanel();
            tabInfo = new TabControl();
            tabTables = new TabPage();
            ckAllDataTables = new CheckBox();
            dataGridTables = new DataGridView();
            tabReports = new TabPage();
            ckAllReports = new CheckBox();
            dataGridReports = new DataGridView();
            tabSeeds = new TabPage();
            ckAllSeedTables = new CheckBox();
            dataGridSeeds = new DataGridView();
            label1 = new Label();
            cbSystemInfo = new ComboBox();
            btnGenerateAll = new Button();
            btnModifyAll = new Button();
            btnModifyComponent = new Button();
            btnGenerateComponent = new Button();
            btnShowModify = new Button();
            btnShowGenerate = new Button();
            btnRollBackModifyComponent = new Button();
            btnRollBackGenerateComponent = new Button();
            txtNameCapital = new TextBox();
            txtNameSmall = new TextBox();
            txtMenuName = new TextBox();
            txtFileName = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            panelItems = new FlowLayoutPanel();
            txtMessage = new TextBox();
            label6 = new Label();
            panel1 = new Panel();
            ckDisableSecurity = new CheckBox();
            ckKeepExistingCode = new CheckBox();
            label7 = new Label();
            txtBfsRootDir = new TextBox();
            btnGenerateSystem = new Button();
            label8 = new Label();
            btnRefreshDb = new Button();
            tabInfo.SuspendLayout();
            tabTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridTables).BeginInit();
            tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridReports).BeginInit();
            tabSeeds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridSeeds).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelBackend
            // 
            panelBackend.ColumnCount = 1;
            panelBackend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            panelBackend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panelBackend.Location = new Point(10, 5);
            panelBackend.Name = "panelBackend";
            panelBackend.RowCount = 2;
            panelBackend.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panelBackend.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelBackend.Size = new Size(318, 171);
            panelBackend.TabIndex = 11;
            // 
            // panelFrontend
            // 
            panelFrontend.ColumnCount = 1;
            panelFrontend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            panelFrontend.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            panelFrontend.Location = new Point(339, 5);
            panelFrontend.Name = "panelFrontend";
            panelFrontend.RowCount = 2;
            panelFrontend.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panelFrontend.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelFrontend.Size = new Size(297, 171);
            panelFrontend.TabIndex = 10;
            // 
            // tabInfo
            // 
            tabInfo.Controls.Add(tabTables);
            tabInfo.Controls.Add(tabReports);
            tabInfo.Controls.Add(tabSeeds);
            tabInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabInfo.Location = new Point(12, 136);
            tabInfo.Name = "tabInfo";
            tabInfo.Padding = new Point(60, 10);
            tabInfo.SelectedIndex = 0;
            tabInfo.Size = new Size(565, 700);
            tabInfo.TabIndex = 9;
            // 
            // tabTables
            // 
            tabTables.Controls.Add(ckAllDataTables);
            tabTables.Controls.Add(dataGridTables);
            tabTables.ForeColor = SystemColors.ActiveCaptionText;
            tabTables.Location = new Point(4, 48);
            tabTables.Name = "tabTables";
            tabTables.Padding = new Padding(3);
            tabTables.Size = new Size(557, 648);
            tabTables.TabIndex = 0;
            tabTables.Text = "Tables";
            tabTables.UseVisualStyleBackColor = true;
            // 
            // ckAllDataTables
            // 
            ckAllDataTables.AutoSize = true;
            ckAllDataTables.Location = new Point(14, 8);
            ckAllDataTables.Name = "ckAllDataTables";
            ckAllDataTables.Size = new Size(164, 29);
            ckAllDataTables.TabIndex = 1;
            ckAllDataTables.Text = "All Data Tables";
            ckAllDataTables.UseVisualStyleBackColor = true;
            ckAllDataTables.CheckedChanged += AllCheckBox_Click;
            // 
            // dataGridTables
            // 
            dataGridTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridTables.Location = new Point(10, 40);
            dataGridTables.Name = "dataGridTables";
            dataGridTables.RowHeadersWidth = 62;
            dataGridTables.Size = new Size(530, 600);
            dataGridTables.TabIndex = 0;
            // 
            // tabReports
            // 
            tabReports.Controls.Add(ckAllReports);
            tabReports.Controls.Add(dataGridReports);
            tabReports.ForeColor = SystemColors.ActiveCaptionText;
            tabReports.Location = new Point(4, 48);
            tabReports.Name = "tabReports";
            tabReports.Padding = new Padding(3);
            tabReports.Size = new Size(557, 648);
            tabReports.TabIndex = 1;
            tabReports.Text = "Reports";
            tabReports.UseVisualStyleBackColor = true;
            // 
            // ckAllReports
            // 
            ckAllReports.AutoSize = true;
            ckAllReports.Location = new Point(12, 7);
            ckAllReports.Name = "ckAllReports";
            ckAllReports.Size = new Size(133, 29);
            ckAllReports.TabIndex = 2;
            ckAllReports.Text = "All Reports";
            ckAllReports.UseVisualStyleBackColor = true;
            ckAllReports.CheckedChanged += AllCheckBox_Click;
            // 
            // dataGridReports
            // 
            dataGridReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridReports.Location = new Point(10, 40);
            dataGridReports.Name = "dataGridReports";
            dataGridReports.RowHeadersWidth = 62;
            dataGridReports.Size = new Size(530, 600);
            dataGridReports.TabIndex = 1;
            // 
            // tabSeeds
            // 
            tabSeeds.Controls.Add(ckAllSeedTables);
            tabSeeds.Controls.Add(dataGridSeeds);
            tabSeeds.ForeColor = SystemColors.ActiveCaptionText;
            tabSeeds.Location = new Point(4, 48);
            tabSeeds.Name = "tabSeeds";
            tabSeeds.Size = new Size(557, 648);
            tabSeeds.TabIndex = 2;
            tabSeeds.Text = "Seeds";
            tabSeeds.UseVisualStyleBackColor = true;
            // 
            // ckAllSeedTables
            // 
            ckAllSeedTables.AutoSize = true;
            ckAllSeedTables.Location = new Point(10, 8);
            ckAllSeedTables.Name = "ckAllSeedTables";
            ckAllSeedTables.Size = new Size(165, 29);
            ckAllSeedTables.TabIndex = 2;
            ckAllSeedTables.Text = "All Seed Tables";
            ckAllSeedTables.UseVisualStyleBackColor = true;
            ckAllSeedTables.CheckedChanged += AllCheckBox_Click;
            // 
            // dataGridSeeds
            // 
            dataGridSeeds.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridSeeds.Location = new Point(10, 40);
            dataGridSeeds.Name = "dataGridSeeds";
            dataGridSeeds.RowHeadersWidth = 62;
            dataGridSeeds.Size = new Size(530, 600);
            dataGridSeeds.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(17, 54);
            label1.Name = "label1";
            label1.Size = new Size(131, 28);
            label1.TabIndex = 8;
            label1.Text = "System Name";
            // 
            // cbSystemInfo
            // 
            cbSystemInfo.FormattingEnabled = true;
            cbSystemInfo.Location = new Point(154, 52);
            cbSystemInfo.Name = "cbSystemInfo";
            cbSystemInfo.Size = new Size(228, 33);
            cbSystemInfo.TabIndex = 7;
            cbSystemInfo.SelectedIndexChanged += cbSystemInfo_SelectedIndexChanged;
            // 
            // btnGenerateAll
            // 
            btnGenerateAll.Location = new Point(618, 254);
            btnGenerateAll.Name = "btnGenerateAll";
            btnGenerateAll.Size = new Size(230, 35);
            btnGenerateAll.TabIndex = 12;
            btnGenerateAll.Text = "Generate All Components";
            btnGenerateAll.UseVisualStyleBackColor = true;
            btnGenerateAll.Click += btnGenerateAll_Click;
            // 
            // btnModifyAll
            // 
            btnModifyAll.Location = new Point(618, 295);
            btnModifyAll.Name = "btnModifyAll";
            btnModifyAll.Size = new Size(230, 35);
            btnModifyAll.TabIndex = 13;
            btnModifyAll.Text = "Modify All Components";
            btnModifyAll.UseVisualStyleBackColor = true;
            btnModifyAll.Click += btnModifyAll_Click;
            // 
            // btnModifyComponent
            // 
            btnModifyComponent.Location = new Point(895, 295);
            btnModifyComponent.Name = "btnModifyComponent";
            btnModifyComponent.Size = new Size(230, 35);
            btnModifyComponent.TabIndex = 15;
            btnModifyComponent.Text = "Modify Component";
            btnModifyComponent.UseVisualStyleBackColor = true;
            btnModifyComponent.Click += btnModifyComponent_Click;
            // 
            // btnGenerateComponent
            // 
            btnGenerateComponent.Location = new Point(895, 254);
            btnGenerateComponent.Name = "btnGenerateComponent";
            btnGenerateComponent.Size = new Size(230, 35);
            btnGenerateComponent.TabIndex = 14;
            btnGenerateComponent.Text = "Generate Component";
            btnGenerateComponent.UseVisualStyleBackColor = true;
            btnGenerateComponent.Click += btnGenerateComponent_Click;
            // 
            // btnShowModify
            // 
            btnShowModify.Location = new Point(1172, 295);
            btnShowModify.Name = "btnShowModify";
            btnShowModify.Size = new Size(230, 35);
            btnShowModify.TabIndex = 17;
            btnShowModify.Text = "Show Modify";
            btnShowModify.UseVisualStyleBackColor = true;
            btnShowModify.Click += btnShowModify_Click;
            // 
            // btnShowGenerate
            // 
            btnShowGenerate.Location = new Point(1172, 254);
            btnShowGenerate.Name = "btnShowGenerate";
            btnShowGenerate.Size = new Size(230, 35);
            btnShowGenerate.TabIndex = 16;
            btnShowGenerate.Text = "Show Generate";
            btnShowGenerate.UseVisualStyleBackColor = true;
            btnShowGenerate.Click += btnShowGenerate_Click;
            // 
            // btnRollBackModifyComponent
            // 
            btnRollBackModifyComponent.Location = new Point(1449, 295);
            btnRollBackModifyComponent.Name = "btnRollBackModifyComponent";
            btnRollBackModifyComponent.Size = new Size(230, 35);
            btnRollBackModifyComponent.TabIndex = 19;
            btnRollBackModifyComponent.Text = "Roll Back Modify Component";
            btnRollBackModifyComponent.UseVisualStyleBackColor = true;
            btnRollBackModifyComponent.Click += btnRollBackModifyComponent_Click;
            // 
            // btnRollBackGenerateComponent
            // 
            btnRollBackGenerateComponent.Location = new Point(1449, 254);
            btnRollBackGenerateComponent.Name = "btnRollBackGenerateComponent";
            btnRollBackGenerateComponent.Size = new Size(230, 35);
            btnRollBackGenerateComponent.TabIndex = 18;
            btnRollBackGenerateComponent.Text = "Roll Back Component";
            btnRollBackGenerateComponent.UseVisualStyleBackColor = true;
            btnRollBackGenerateComponent.Click += btnRollBackGenerateComponent_Click;
            // 
            // txtNameCapital
            // 
            txtNameCapital.ForeColor = SystemColors.HotTrack;
            txtNameCapital.Location = new Point(615, 214);
            txtNameCapital.Name = "txtNameCapital";
            txtNameCapital.Size = new Size(230, 31);
            txtNameCapital.TabIndex = 20;
            // 
            // txtNameSmall
            // 
            txtNameSmall.ForeColor = SystemColors.HotTrack;
            txtNameSmall.Location = new Point(893, 214);
            txtNameSmall.Name = "txtNameSmall";
            txtNameSmall.Size = new Size(230, 31);
            txtNameSmall.TabIndex = 21;
            // 
            // txtMenuName
            // 
            txtMenuName.ForeColor = SystemColors.HotTrack;
            txtMenuName.Location = new Point(1171, 214);
            txtMenuName.Name = "txtMenuName";
            txtMenuName.Size = new Size(230, 31);
            txtMenuName.TabIndex = 22;
            // 
            // txtFileName
            // 
            txtFileName.ForeColor = SystemColors.HotTrack;
            txtFileName.Location = new Point(1449, 214);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(230, 31);
            txtFileName.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Red;
            label2.Location = new Point(614, 186);
            label2.Name = "label2";
            label2.Size = new Size(118, 25);
            label2.TabIndex = 24;
            label2.Text = "Capital Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Red;
            label3.Location = new Point(1445, 186);
            label3.Name = "label3";
            label3.Size = new Size(167, 25);
            label3.TabIndex = 25;
            label3.Text = "Frontend File Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Red;
            label4.Location = new Point(1167, 186);
            label4.Name = "label4";
            label4.Size = new Size(175, 25);
            label4.TabIndex = 26;
            label4.Text = "Frontend Menu Item";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Red;
            label5.Location = new Point(890, 186);
            label5.Name = "label5";
            label5.Size = new Size(107, 25);
            label5.TabIndex = 27;
            label5.Text = "Small Name";
            // 
            // panelItems
            // 
            panelItems.AutoScroll = true;
            panelItems.Location = new Point(617, 337);
            panelItems.Name = "panelItems";
            panelItems.Size = new Size(1270, 632);
            panelItems.TabIndex = 28;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(16, 876);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.ScrollBars = ScrollBars.Both;
            txtMessage.Size = new Size(557, 90);
            txtMessage.TabIndex = 29;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(12, 841);
            label6.Name = "label6";
            label6.Size = new Size(64, 28);
            label6.TabIndex = 30;
            label6.Text = "Result";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(ckDisableSecurity);
            panel1.Controls.Add(ckKeepExistingCode);
            panel1.Controls.Add(panelBackend);
            panel1.Controls.Add(panelFrontend);
            panel1.Location = new Point(618, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1061, 183);
            panel1.TabIndex = 31;
            // 
            // ckDisableSecurity
            // 
            ckDisableSecurity.AutoSize = true;
            ckDisableSecurity.Location = new Point(674, 60);
            ckDisableSecurity.Name = "ckDisableSecurity";
            ckDisableSecurity.Size = new Size(163, 29);
            ckDisableSecurity.TabIndex = 38;
            ckDisableSecurity.Text = "Disable Security";
            ckDisableSecurity.UseVisualStyleBackColor = true;
            // 
            // ckKeepExistingCode
            // 
            ckKeepExistingCode.AutoSize = true;
            ckKeepExistingCode.Checked = true;
            ckKeepExistingCode.CheckState = CheckState.Checked;
            ckKeepExistingCode.Location = new Point(674, 11);
            ckKeepExistingCode.Name = "ckKeepExistingCode";
            ckKeepExistingCode.Size = new Size(189, 29);
            ckKeepExistingCode.TabIndex = 37;
            ckKeepExistingCode.Text = "Keep Existing Code";
            ckKeepExistingCode.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(17, 19);
            label7.Name = "label7";
            label7.Size = new Size(115, 28);
            label7.TabIndex = 32;
            label7.Text = "Bfs Root Dir";
            // 
            // txtBfsRootDir
            // 
            txtBfsRootDir.Enabled = false;
            txtBfsRootDir.Location = new Point(154, 12);
            txtBfsRootDir.Name = "txtBfsRootDir";
            txtBfsRootDir.Size = new Size(228, 31);
            txtBfsRootDir.TabIndex = 33;
            // 
            // btnGenerateSystem
            // 
            btnGenerateSystem.Location = new Point(388, 52);
            btnGenerateSystem.Name = "btnGenerateSystem";
            btnGenerateSystem.Size = new Size(185, 33);
            btnGenerateSystem.TabIndex = 34;
            btnGenerateSystem.Text = "Select System";
            btnGenerateSystem.UseVisualStyleBackColor = true;
            btnGenerateSystem.Click += btnGenerateSystem_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(388, 7);
            label8.Name = "label8";
            label8.Size = new Size(193, 28);
            label8.TabIndex = 35;
            label8.Text = "Set manually in code";
            // 
            // btnRefreshDb
            // 
            btnRefreshDb.Location = new Point(388, 97);
            btnRefreshDb.Name = "btnRefreshDb";
            btnRefreshDb.Size = new Size(185, 33);
            btnRefreshDb.TabIndex = 36;
            btnRefreshDb.Text = "Refresh Database";
            btnRefreshDb.UseVisualStyleBackColor = true;
            btnRefreshDb.Click += btnRefreshDb_Click;
            // 
            // FormGenerator
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1050);
            Controls.Add(btnRefreshDb);
            Controls.Add(label8);
            Controls.Add(btnGenerateSystem);
            Controls.Add(txtBfsRootDir);
            Controls.Add(label7);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(txtMessage);
            Controls.Add(panelItems);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtFileName);
            Controls.Add(txtMenuName);
            Controls.Add(txtNameSmall);
            Controls.Add(txtNameCapital);
            Controls.Add(btnRollBackModifyComponent);
            Controls.Add(btnRollBackGenerateComponent);
            Controls.Add(btnShowModify);
            Controls.Add(btnShowGenerate);
            Controls.Add(btnModifyComponent);
            Controls.Add(btnGenerateComponent);
            Controls.Add(btnModifyAll);
            Controls.Add(btnGenerateAll);
            Controls.Add(tabInfo);
            Controls.Add(label1);
            Controls.Add(cbSystemInfo);
            Name = "FormGenerator";
            Text = "FormGenerator";
            WindowState = FormWindowState.Maximized;
            Load += FormGenerator_Load;
            tabInfo.ResumeLayout(false);
            tabTables.ResumeLayout(false);
            tabTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridTables).EndInit();
            tabReports.ResumeLayout(false);
            tabReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridReports).EndInit();
            tabSeeds.ResumeLayout(false);
            tabSeeds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridSeeds).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel panelBackend;
        private TableLayoutPanel panelFrontend;
        private TabControl tabInfo;
        private TabPage tabTables;
        private DataGridView dataGridTables;
        private TabPage tabReports;
        private DataGridView dataGridReports;
        private TabPage tabSeeds;
        private DataGridView dataGridSeeds;
        private Label label1;
        private ComboBox cbSystemInfo;
        private Button btnGenerateAll;
        private Button btnModifyAll;
        private Button btnModifyComponent;
        private Button btnGenerateComponent;
        private Button btnShowModify;
        private Button btnShowGenerate;
        private Button btnRollBackModifyComponent;
        private Button btnRollBackGenerateComponent;
        private TextBox txtNameCapital;
        private TextBox txtNameSmall;
        private TextBox txtMenuName;
        private TextBox txtFileName;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private FlowLayoutPanel panelItems;
        private TextBox txtMessage;
        private Label label6;
        private Panel panel1;
        private Label label7;
        private TextBox txtBfsRootDir;
        private CheckBox ckAllDataTables;
        private CheckBox ckAllReports;
        private CheckBox ckAllSeedTables;
        private Button btnGenerateSystem;
        private Label label8;
        private CheckBox ckDisableSecurity;
        private CheckBox ckKeepExistingCode;
        private Button btnRefreshDb;
    }
}