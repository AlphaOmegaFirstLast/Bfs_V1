using Admin.App.Constants;
using Admin.App;
using Admin.App;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Admin.App
{
    public class CodeGeneratorBase
    {
        public string BfsRootDir { get; set; } = @"C:\Bfs_V1\V1";
        public string TemplateRootDir { get; set; } = @".\Templates";
        private string TemplateInfoFile { get; set; } = @".\Templates\ComponentTemplateInfo.json";  // the compiler copies it to the output foldr
        private string PlaceHolderInfoFile { get; set; } = @".\Templates\WriterInfo.json";  // the compiler copies it to the output foldr

        public string SystemRootDir { get { return CurrentTemplate?.CodeType == CodeType.Backend ? SystemBackendDir : SystemFrontendDir; } }
        public string SystemBackendDir { get { return $@"{BfsRootDir}\Backend\Bfs.{CurrentSystem?.Name}"; } }
        public string SystemFrontendDir { get { return $@"{BfsRootDir}\Frontend\main\src\bfs"; } }
        public string AppDir { get { return $@"{BfsRootDir}\Frontend\main"; } }
        public bool KeepExistingCode { get; set; } = false;

        public CodeTracker CodeTracker { get; set; } = new CodeTracker();
        public CodeWriter CodeWriter { get; set; }

        // Lists are public for user interaction in the UI
        public List<BestFitTemplate> TemplateList { get; set; } = new List<BestFitTemplate>();
        public List<IBestFitSystem> SystemList { get; set; } = new List<IBestFitSystem>();
        public List<IBestFitComponent> ComponentList { get; set; } = new List<IBestFitComponent>();

        public List<BestFitTemplate> SelectedTemplateList { get; set; } = new List<BestFitTemplate>();
        public List<IBestFitComponent> SelectedComponentList { get; set; } = new List<IBestFitComponent>();

        public BestFitTemplate? CurrentTemplate { get; set; } = null;
        public SystemWriter? CurrentSystem { get; set; }
        public ComponentWriter? CurrentComponent { get; set; }


        public List<PlaceHolderInfo> FlatPlaceHolderList { get; set; } = new List<PlaceHolderInfo>();

        public List<IBestFitField> FieldList { get; set; } = new List<IBestFitField>();
        public List<IBestFitAction> SystemActionList { get; set; } = new List<IBestFitAction>();
        public List<IBestFitAction> BusinessActionList { get; set; } = new List<IBestFitAction>();
        public List<ActionWriter> ComponentActionList { get; set; } = new List<ActionWriter>();

        public CodeGeneratorBase()
        {
            // populate Template Lists
            TemplateList = BestFitTemplate.GetList(TemplateInfoFile);
            var flatTemplateList = BestFitTemplate.GetFlatList(TemplateList);
            var bestFitPlaceHolderList = BestFitPlaceHolder.GetList(PlaceHolderInfoFile);
            FlatPlaceHolderList = BestFitPlaceHolder.GetFlatList(bestFitPlaceHolderList);

            CodeWriter = new CodeWriter();

            ReadDbEntities();
        }

        public virtual void ReadDbEntities()
        {
            //overridden in descendant classes
        }


        public void SetSystem(IBestFitSystem systemEntity)
        {
            try
            {
                CodeWriter.system = new SystemWriter(systemEntity);
                this.CurrentSystem = new SystemWriter(systemEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error in CodeInfo.SetSystem [{systemEntity.Name}]. {ex.Message}");
            }
        }

        public void SetComponent(IBestFitComponent componentEntity)
        {
            if (componentEntity !=null && !SelectedComponentList.Any(x => x.Id == componentEntity.Id))
            {
                SelectedComponentList.Add(componentEntity);
            }

            try
            {
                if (componentEntity != null && componentEntity.Name.ToLower() != "all")
                {
                    CodeWriter.component = new ComponentWriter(componentEntity, CodeWriter?.system?.Name, FieldList);
                    CodeWriter.actionList = ComponentActionList.Where(x => x.BfsComponentId == CodeWriter.component.Id)
                                            .Select(x=> (ActionWriter)x).ToList();
                    this.CurrentComponent = new ComponentWriter(componentEntity, this.CurrentSystem?.Name, FieldList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error in CodeInfo.SetComponent [{componentEntity.Name}]. {ex.Message}");
            }
        }

        public List<PlaceHolderInfo> GetPlaceHolderListOfTemplate(TemplateInfo templateInfo)
        {
            var writers = FlatPlaceHolderList.Where(writer => writer.ParentId.Contains(templateInfo.Id)).ToList();
            return writers;
        }

        public static Tuple<string, string, string> GetNames(string input)
        {
            //split input string by capital and underscore letters
            var outputs = Regex.Split(input, @"(?=[A-Z])|_");
            var capitals = new List<string>();
            var smalls = new List<string>();
            var fronendFileNames = new List<string>();


            //use as input SspTransaction instead of Ssp_Transaction
            for (var i = 1; i < outputs.Count(); i++)
            {
                var output = outputs[i];
                capitals.Add(output.Substring(0, 1).ToUpper() + output.Substring(1).ToLower());
                if (i == 1)
                {
                    smalls.Add(output.ToLower());
                }
                else
                {
                    smalls.Add(output.Substring(0, 1).ToUpper() + output.Substring(1).ToLower());
                }
                fronendFileNames.Add(output.ToLower());
            }
            var capital = string.Join("", capitals.ToArray());
            var small = string.Join("", smalls.ToArray());
            var frontendFileName = string.Join("-", fronendFileNames.ToArray());

            return new Tuple<string, string, string>(capital, small, frontendFileName);
        }

    }
}