using Admin.App;
using Admin.App;
using Admin.App;

namespace CodeAdmin
{
    public partial class UserControlGenerateItem : UserControl
    {
        private readonly TemplateInfo _generatorTemplate;
        private Action<CodeBase, TemplateInfo> _methodName;
        private Action<CodeBase, TemplateInfo> _rollBackMethodName;
        private CodeBase _codeInfo ;
        private string _outputFile;
        public UserControlGenerateItem(TemplateInfo generatorTemplate, Action<CodeBase, TemplateInfo> method, Action<CodeBase, TemplateInfo> rollBackMethodName)
        {
            InitializeComponent();

            _generatorTemplate = generatorTemplate;
            _methodName = method;
            _rollBackMethodName = rollBackMethodName;
        }

        public void SetUp(CodeBase codeInfo)
        {
            _codeInfo = codeInfo;
            btnExecuteItem.Text = _methodName.Method.Name;
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
            _methodName?.Invoke(_codeInfo, _generatorTemplate);
        }

        public void btnRollBackItem_Click(object sender, EventArgs e)
        {
            _rollBackMethodName?.Invoke(_codeInfo, _generatorTemplate);
        }

        private void btnWriters_Click(object sender, EventArgs e)
        {
            var form = FormList.GetSingleton();
            form.TemplateId = _generatorTemplate.Id.ToString();
            form.TemplateOutputDir = _outputFile;
            form.List = _codeInfo.GetPlaceHolderListOfTemplate(_generatorTemplate);
            form.RefreshGrid();
        }
    }
}

