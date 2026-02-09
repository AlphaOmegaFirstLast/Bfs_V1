using Admin.App.Constants;
using Admin.App;
using Admin.App;
using Admin.App;
using Admin.App;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using Admin.App.CodeManagers;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace CodeAdmin
{
    public partial class FormGenerator : Form
    {
        private CodeGeneratorV3 _codeGenerator = new CodeGeneratorV3();
        private string lastControlsKey = "";
        private bool isGenerateSystemClicked = false;
        private List<CheckBox> checkboxList = new List<CheckBox>();

        public FormGenerator()
        {
            InitializeComponent();

            dataGridTables.CellContentClick += DataGridView_CellContentClick;
            dataGridReports.CellContentClick += DataGridView_CellContentClick;
            dataGridSeeds.CellContentClick += DataGridView_CellContentClick;
        }

        private void FormGenerator_Load(object sender, EventArgs e)
        {
            // init UI controls
            //tabInfo.SizeMode = TabSizeMode.Fixed;
            //tabInfo.ItemSize = new Size(120, 35);
            foreach (var control in this.Controls)
            {
                if (control is Button button)
                    button.Enabled = false;
            }

            btnGenerateSystem.Enabled = true;

            txtBfsRootDir.Text = _codeGenerator.BfsRootDir;
            LoadSystemInfo();
            LoadComponentTemplateList(DataType.Tables);
        }

        private void LoadSystemInfo()
        {
            var systemList = _codeGenerator.SystemList;

            cbSystemInfo.DataSource = null;
            cbSystemInfo.Items.Clear();
            cbSystemInfo.ValueMember = "Id";
            cbSystemInfo.DisplayMember = "Name";
            cbSystemInfo.DataSource = systemList;
        }

        private void btnGenerateSystem_Click(object sender, EventArgs e)
        {
            isGenerateSystemClicked = true;
            //show system templates.
            var list = new List<TemplateElementType>() {
                TemplateElementType.System
              , TemplateElementType.Api_Project
              , TemplateElementType.Contracts_Project
              , TemplateElementType.Data_Project
              , TemplateElementType.Domain_Project
              , TemplateElementType.Client_Project
              , TemplateElementType.AngularFramework
            };
            LoadTemplateList(list, true);
            ClearComponentSelection();
            SetUIControls("Backend System is Selected ", "");
        }

        private void LoadComponentTemplateList(DataType dataType, bool enabled = false)
        {
            _codeGenerator.SelectedTemplateList.Clear();
            var templateElementTypeList = new List<TemplateElementType>();
            switch (dataType)
            {
                case DataType.Reports:
                    templateElementTypeList.Add(TemplateElementType.Report);
                    templateElementTypeList.Add(TemplateElementType.Chart);
                    break;
                case DataType.Tables:
                case DataType.Seed:
                    templateElementTypeList.Add(TemplateElementType.Table);
                    templateElementTypeList.Add(TemplateElementType.List);
                    templateElementTypeList.Add(TemplateElementType.Matrix);
                    templateElementTypeList.Add(TemplateElementType.Validator);
                    break;
            }

            LoadTemplateList(templateElementTypeList, enabled);
        }

        private void LoadTemplateList(List<TemplateElementType> templateElementTypeList, bool enabled = false)
        {
            _codeGenerator.SelectedTemplateList.Clear();
            var templateList = new List<BestFitTemplate>();

            templateList = _codeGenerator.TemplateList.Where(x => templateElementTypeList.Contains(x.TemplateElementType)).ToList();

            checkboxList.Clear();
            FillTemplatePanel(templateList, CodeType.Backend, panelBackend, enabled);
            FillTemplatePanel(templateList, CodeType.Frontend, panelFrontend, enabled);
        }

        public void FillTemplatePanel(List<BestFitTemplate> templateList, CodeType CodeType, TableLayoutPanel panel, bool enabled)
        {
            // create checkbox for each template
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            var list = templateList.Where(x => x.CodeType == CodeType).ToList();
            panel.RowCount = list.Count();

            var rowIndex = 0;
            foreach (var componentTemplate in list)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

                var templateId = $@"{componentTemplate.CodeType.ToString()} - {componentTemplate.TemplateElementType.ToString()}";
                var checkbox = new CheckBox() { AutoSize = true, Enabled = enabled, Text = templateId };
                checkbox.Click += TemplateCheckBox_Click;
                checkboxList.Add(checkbox);
                panel.Controls.Add(checkbox, 0, rowIndex);
                rowIndex++;
            }
        }

        private void TemplateCheckBox_Click(object sender, EventArgs e)
        {
            // codeInfo holds templates selected by user
            _codeGenerator.SelectedTemplateList = _codeGenerator.TemplateList.Where(x =>
                checkboxList.Where(c => c.Checked).Select(c => c.Text).Contains(
                ($@"{x.CodeType.ToString()} - {x.TemplateElementType.ToString()}"))).ToList();

            SetUIControls("Selected Templates change", "");
        }

        private void ClearComponentSelection()
        {
            ckAllDataTables.Checked = false;
            ckAllReports.Checked = false;
            ckAllSeedTables.Checked = false;

            _codeGenerator.SelectedComponentList.Clear();
            _codeGenerator.SetComponent(null);
        }

        private void AllCheckBox_Click(object sender, EventArgs e)
        {
            // Only one "All [DataTable, Report, SeedTable]" selected checkbox at a time
            var checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                isGenerateSystemClicked = false;
                ckAllDataTables.Checked = checkbox.Name == ckAllDataTables.Name;
                ckAllReports.Checked = checkbox.Name == ckAllReports.Name;
                ckAllSeedTables.Checked = checkbox.Name == ckAllSeedTables.Name;
            }

            var dataType = ckAllDataTables.Checked ? DataType.Tables
                : ckAllSeedTables.Checked ? DataType.Seed
                : ckAllReports.Checked ? DataType.Reports
                : DataType.None;

            _codeGenerator.SelectedComponentList = _codeGenerator.ComponentList.Where(x => (DataType)x.DataTypeId == dataType).ToList();
            _codeGenerator.CurrentComponent = null;

            //If only one component is selected, set it as current component. so that SetUIControls() logic enables the right buttons.
            if (_codeGenerator.SelectedComponentList.Count == 1)
            {
                _codeGenerator.SetComponent(_codeGenerator.SelectedComponentList.First());
            }

            LoadComponentTemplateList(dataType, checkbox.Checked);
            SetUIControls("Select ", "");
        }

        private void cbSystemInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var systemEntity = cbSystemInfo.SelectedItem == null ? null : (IBestFitSystem)cbSystemInfo.SelectedItem;
            _codeGenerator.SetSystem(systemEntity);

            // Fill Tables Grid
            SetDataGrid(_codeGenerator.CurrentSystem?.Id, DataType.Tables, dataGridTables);
            // Fill reports Grid
            SetDataGrid(_codeGenerator.CurrentSystem?.Id, DataType.Reports, dataGridReports);
            // Fill Seed Tables Grid
            SetDataGrid(_codeGenerator.CurrentSystem?.Id, DataType.Seed, dataGridSeeds);
        }

        private void SetDataGrid(long? systemInfoId, DataType dataType, DataGridView dataGridView)
        {
            // Display BfsComponents in 3 Grids, each Grid is inside a tab, displays BfsComponents of a certain DataType
            dataGridView.DataSource = string.Empty;
            dataGridView.Columns.Clear();
            dataGridView.DataSource = _codeGenerator.ComponentList.Where(x => x.BfsSystemId == systemInfoId && (DataType)x.DataTypeId == dataType).ToList();

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.Visible = (column.Name == "Name" || column.Name == "DisplayName");
                column.Width = column.Visible ? 250 : 0;
            }

            var selectButton = new DataGridViewButtonColumn() { Name = "ActionColumn", HeaderText = "Action", Text = "select", Width = 90 };
            selectButton.UseColumnTextForButtonValue = true; // Sets the button text to appear in every row
            dataGridView.Columns.Add(selectButton);
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // when a component (table - report - seed) is selected in the data grid, do _codeInfo.SetComponent
            var dataGridView = sender as DataGridView;
            if (dataGridView?.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;
                var component = (IBestFitComponent?)(dataGridView?.Rows[e.RowIndex].DataBoundItem);
                if (component != null)
                {
                    isGenerateSystemClicked = false;
                    ckAllDataTables.Checked = false;
                    ckAllReports.Checked = false;
                    ckAllSeedTables.Checked = false;

                    _codeGenerator.SelectedTemplateList.Clear();
                    _codeGenerator.SelectedComponentList.Clear();
                    _codeGenerator.SetComponent(component);
                    SetUIControls("Selected: ", _codeGenerator.CurrentComponent?.Name);
                    LoadComponentTemplateList((DataType)component.DataTypeId, true);
                }
            }
        }

        public void CreateControlsOfGeneratedFiles(BestFitTemplate componentTemplate)
        {
            panelItems.Controls.Clear();
            foreach (var generatorTemplate in componentTemplate.GeneratorTemplateList)
            {
                var control = new UserControlGenerateItem(generatorTemplate, this.Generate, this.RollbackGenerate);
                panelItems.Controls.Add(control);
            }

            panelItems.Refresh();
        }

        public void CreateControlsOfModifiedFiles(BestFitTemplate componentTemplate)
        {
            panelItems.Controls.Clear();
            foreach (var modifierTemplate in componentTemplate.ModifierTemplateList)
            {
                var control = new UserControlGenerateItem(modifierTemplate, this.Modify, this.RollbackModify);
                panelItems.Controls.Add(control);
            }

            panelItems.Refresh();
        }

        public void Generate(CodeBase codeInfo, TemplateInfo generatorTemplate)
        {
            try
            {
                codeInfo.KeepExistingCode = ckKeepExistingCode.Checked;
                if (generatorTemplate.TemplateFile.EndsWith("*.*"))
                {
                    TemplateManager.InitFrameWork(codeInfo, generatorTemplate);
                }
                else
                {
                    var result = TemplateManager.Generate(codeInfo, generatorTemplate);
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Error: {ex.Message}");
            }
            SetMessage("Done. ");
        }

        public void Modify(CodeBase codeInfo, TemplateInfo modifierTemplate)
        {
            try
            {
                if (modifierTemplate.TemplateFile.EndsWith("*.*"))
                {
                    TemplateManager.InitFrameWork(codeInfo, modifierTemplate);
                }
                else
                {
                    TemplateManager.Modify(codeInfo, modifierTemplate);
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Error: {ex.Message}");
            }
            SetMessage("Done. ");
        }

        public void RollbackGenerate(CodeBase codeInfo, TemplateInfo generatorTemplate)
        {
            try
            {
                if (generatorTemplate.TemplateFile.EndsWith("*.*"))
                {
                    SetMessage("Roll back require manuall deletetion of files");
                }
                else
                {
                    var result = TemplateManager.DeleteFile(codeInfo, generatorTemplate);
                    SetMessage("Done." + result);
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Error: {ex.Message}");
            }
        }

        public void RollbackModify(CodeBase codeInfo, TemplateInfo modifierTemplate)
        {
            try
            {
                if (modifierTemplate.TemplateFile.EndsWith("*.*"))
                {
                    SetMessage("Roll back require manuall deletetion of files");
                }
                else
                {
                    TemplateManager.RollBackModify(codeInfo, modifierTemplate);
                    SetMessage("Done.");
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Error: {ex.Message}");
            }
        }

        private void SetMessage(string message)
        {
            txtMessage.Text = message;
            txtMessage.ForeColor = message.Contains("Error") ? Color.Red : Color.Blue;
        }

        private static string GetControlsKey(CodeBase _codeInfo, string key)
        {
            if (_codeInfo.CurrentTemplate != null)
            {
                return $"{key}-{_codeInfo.CurrentTemplate.TemplateElementType}-{_codeInfo.CurrentTemplate.CodeType}";
            }
            return null;
        }

        private void SetUIControls(string operation, string itemName)
        {
            if (_codeGenerator.CurrentComponent != null)
            {
                SetMessage($@"{operation} {_codeGenerator.CurrentComponent.NameCapital}...");

                txtNameCapital.Text = _codeGenerator.CurrentComponent.NameCapital;
                txtNameSmall.Text = _codeGenerator.CurrentComponent.NameSmall;
                txtFileName.Text = _codeGenerator.CurrentComponent.FileName; ;
                txtMenuName.Text = _codeGenerator.CurrentComponent.MenuName;
            }
            else
            {
                var txt = ckAllDataTables.Checked ? ckAllDataTables.Text
                    : ckAllSeedTables.Checked ? ckAllSeedTables.Text
                    : ckAllReports.Checked ? ckAllReports.Text
                    : "";
                txtNameCapital.Text = txt;
                txtNameSmall.Text = txt;
                txtFileName.Text = txt;
                txtMenuName.Text = txt;
            }

            var isTemplate = _codeGenerator.SelectedTemplateList.Count > 0;
            var isComponent = _codeGenerator.SelectedComponentList.Count == 1;
            var isAllComponent = _codeGenerator.SelectedComponentList.Count > 1;
            var isSystem = isGenerateSystemClicked;

            btnShowGenerate.Enabled = isTemplate && (isSystem || isComponent || isAllComponent);
            btnShowModify.Enabled = isTemplate && (isSystem || isComponent || isAllComponent);

            btnGenerateComponent.Enabled = isTemplate && (isSystem || isComponent);
            btnModifyComponent.Enabled = isTemplate && (isSystem || isComponent);

            btnRollBackGenerateComponent.Enabled = isTemplate && (isSystem || isComponent);
            btnRollBackModifyComponent.Enabled = isTemplate && (isSystem || isComponent);

            btnGenerateAll.Enabled = isTemplate && isAllComponent;
            btnModifyAll.Enabled = isTemplate && isAllComponent;
        }

        private void btnRollBackGenerateComponent_Click(object sender, EventArgs e)
        {
            SetUIControls("Deleteing generated files ", _codeGenerator.CurrentComponent?.NameCapital);

            foreach (var componentTemplate in _codeGenerator.SelectedTemplateList)
            {
                _codeGenerator.CurrentTemplate = componentTemplate;

                var currentControlsKey = GetControlsKey(_codeGenerator, "Generate");
                if (lastControlsKey != currentControlsKey)
                {
                    lastControlsKey = currentControlsKey;
                    CreateControlsOfGeneratedFiles(_codeGenerator.CurrentTemplate);
                }

                foreach (var control in panelItems.Controls)
                {
                    if (control is UserControlGenerateItem generateItemControl)
                    {
                        generateItemControl.SetUp(_codeGenerator);
                        generateItemControl.btnRollBackItem_Click(sender, e);
                    }
                }
            }
        }

        private void btnRollBackModifyComponent_Click(object sender, EventArgs e)
        {
            SetUIControls("Remove Entry ", _codeGenerator.CurrentComponent.NameCapital);
            foreach (var componentTemplate in _codeGenerator.SelectedTemplateList)
            {
                _codeGenerator.CurrentTemplate = componentTemplate;

                var currentControlsKey = GetControlsKey(_codeGenerator, "Modify");
                if (lastControlsKey != currentControlsKey)
                {
                    lastControlsKey = currentControlsKey;
                    CreateControlsOfModifiedFiles(_codeGenerator.CurrentTemplate);
                }

                foreach (var control in panelItems.Controls)
                {
                    if (control is UserControlGenerateItem modifyItemControl)
                    {
                        modifyItemControl.SetUp(_codeGenerator);
                        modifyItemControl.btnRollBackItem_Click(sender, e);
                    }
                }
            }
        }

        private void btnGenerateComponent_Click(object sender, EventArgs e)
        {
            SetUIControls("Generating ", _codeGenerator.CurrentComponent?.NameCapital);
            foreach (var componentTemplate in _codeGenerator.SelectedTemplateList)
            {
                _codeGenerator.CurrentTemplate = componentTemplate;

                var currentControlsKey = GetControlsKey(_codeGenerator, "Generate");
                if (lastControlsKey != currentControlsKey)
                {
                    lastControlsKey = currentControlsKey;
                    CreateControlsOfGeneratedFiles(_codeGenerator.CurrentTemplate);
                }

                foreach (var control in panelItems.Controls)
                {
                    if (control is UserControlGenerateItem generateItemControl)
                    {
                        generateItemControl.SetUp(_codeGenerator);
                        generateItemControl.btnExecuteItem_Click(sender, e);
                    }
                }
            }
        }

        private void btnModifyComponent_Click(object sender, EventArgs e)
        {
            SetUIControls("Modifying ", _codeGenerator?.CurrentComponent?.NameCapital);

            foreach (var componentTemplate in _codeGenerator.SelectedTemplateList)
            {
                _codeGenerator.CurrentTemplate = componentTemplate;

                var currentControlsKey = GetControlsKey(_codeGenerator, "Modify");
                if (lastControlsKey != currentControlsKey)
                {
                    lastControlsKey = currentControlsKey;
                    CreateControlsOfModifiedFiles(_codeGenerator.CurrentTemplate);
                }

                foreach (var control in panelItems.Controls)
                {
                    if (control is UserControlGenerateItem modifyItemControl)
                    {
                        modifyItemControl.SetUp(_codeGenerator);
                        modifyItemControl.btnExecuteItem_Click(sender, e);
                    }
                }
            }
        }

        private async void btnGenerateAll_Click(object sender, EventArgs e)
        {
            var filteredComponents = _codeGenerator.SelectedComponentList;
            foreach (var item in filteredComponents)
            {
                _codeGenerator.SetComponent(item);
                await Task.Yield();   // Let UI repaint before continuing
                await Task.Delay(1000);
                btnGenerateComponent_Click(sender, e);
            }
        }

        private async void btnModifyAll_Click(object sender, EventArgs e)
        {
            var filteredComponents = _codeGenerator.SelectedComponentList;
            foreach (var item in filteredComponents)
            {
                _codeGenerator.SetComponent(item);
                await Task.Yield();   // Let UI repaint before continuing
                await Task.Delay(1000);
                btnModifyComponent_Click(sender, e);
            }
        }

        private void btnGenerateAndModifyAll_Click(object sender, EventArgs e)
        {
            btnGenerateAll_Click(sender, e);
            btnModifyAll_Click(sender, e);
        }

        private void btnShowGenerate_Click(object sender, EventArgs e)
        {
            foreach (var componentTemplate in _codeGenerator.SelectedTemplateList)
            {
                _codeGenerator.CurrentTemplate = componentTemplate;

                var currentControlsKey = GetControlsKey(_codeGenerator, "Generate");
                if (lastControlsKey != currentControlsKey)
                {
                    lastControlsKey = currentControlsKey;
                    CreateControlsOfGeneratedFiles(_codeGenerator.CurrentTemplate);
                }
                foreach (var control in panelItems.Controls)
                {
                    if (control is UserControlGenerateItem generateItemControl)
                    {
                        generateItemControl.SetUp(_codeGenerator);
                    }
                }
            }
        }

        private void btnShowModify_Click(object sender, EventArgs e)
        {
            foreach (var componentTemplate in _codeGenerator.SelectedTemplateList)
            {
                _codeGenerator.CurrentTemplate = componentTemplate;
                var currentControlsKey = GetControlsKey(_codeGenerator, "Modify");
                if (lastControlsKey != currentControlsKey)
                {
                    lastControlsKey = currentControlsKey;
                    CreateControlsOfModifiedFiles(_codeGenerator.CurrentTemplate);
                }
                foreach (var control in panelItems.Controls)
                {
                    if (control is UserControlGenerateItem modifyItemControl)
                    {
                        modifyItemControl.SetUp(_codeGenerator);
                    }
                }
            }
        }
    }
}
