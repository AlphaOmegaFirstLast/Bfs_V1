using Admin.App;
using Admin.App;
using Admin.App;

namespace CodeAdmin
{
    public partial class UserControlGenerateItem : UserControl
    {
        private readonly TemplateInfo _generatorTemplate;
        private Action<CodeGeneratorBase, TemplateInfo> _generateMethod;
        private Action<CodeGeneratorBase, TemplateInfo> _rollBackMethod;
        private Action<CodeGeneratorBase, TemplateInfo> _saveManualMethod;
        private Action<CodeGeneratorBase, TemplateInfo> _applyManualMethod;
        private CodeGeneratorBase _codeInfo;
        private string _outputFile;
        public UserControlGenerateItem(TemplateInfo generatorTemplate
            , Action<CodeGeneratorBase, TemplateInfo> generateMethod, Action<CodeGeneratorBase, TemplateInfo> rollBackMethod,
            Action<CodeGeneratorBase, TemplateInfo> saveManualMethod, Action<CodeGeneratorBase, TemplateInfo> applyManualMethod)
        {
            InitializeComponent();

            _generatorTemplate = generatorTemplate;
            _generateMethod = generateMethod;
            _rollBackMethod = rollBackMethod;
            _saveManualMethod = saveManualMethod;
            _applyManualMethod = applyManualMethod;
        }

        public void SetUp(CodeGeneratorBase codeInfo)
        {
            _codeInfo = codeInfo;
            btnExecuteItem.Text = _generateMethod.Method.Name;
            txtTemplate.Text = TemplateHelper.GetTemplateFilePath(codeInfo.TemplateRootDir, _generatorTemplate.TemplateFile);
            txtOutputFolder.Text = _generatorTemplate.GetOutputFilePath(codeInfo);

            var templateName = txtTemplate.Text;
            var startIndex = templateName.LastIndexOf(@"\") + 1;
            var endIndex = templateName.LastIndexOf(@".");
            templateName = endIndex > startIndex ? templateName.Substring(startIndex, endIndex - startIndex) : string.Empty;

            _outputFile = txtOutputFolder.Text;
            startIndex = _outputFile.LastIndexOf(@"\") + 1;
            endIndex = _outputFile.LastIndexOf(@".");
            _outputFile = endIndex > startIndex ? _outputFile.Substring(startIndex, endIndex - startIndex) : string.Empty;

            lblItem.Text = $@"{templateName} | {_outputFile}";
        }

        public void btnExecuteItem_Click(object sender, EventArgs e)
        {
            // Call the delegate if it's assigned
            _generateMethod?.Invoke(_codeInfo, _generatorTemplate);
        }

        public void btnRollBackItem_Click(object sender, EventArgs e)
        {
            _rollBackMethod?.Invoke(_codeInfo, _generatorTemplate);
        }

        private void btnWriters_Click(object sender, EventArgs e)
        {
            var form = FormList.GetSingleton();
            form.TemplateId = _generatorTemplate.Id.ToString();
            form.TemplateOutputDir = _outputFile;
            form.List = _codeInfo.GetPlaceHolderListOfTemplate(_generatorTemplate);
            form.RefreshGrid();
        }

        public void btnSaveManualItem_Click(object sender, EventArgs e)
        {
            _saveManualMethod?.Invoke(_codeInfo, _generatorTemplate);
        }

        public void btnApplyManualItem_Click(object sender, EventArgs e)
        {
            _applyManualMethod?.Invoke(_codeInfo, _generatorTemplate);
        }
    }
}

