using Admin.App.Constants;
using Admin.App;
using Admin.App.CodeWriters;

namespace Admin.App
{
    public class CodeWriter
    {
        public WriterType? writerType { get; set; } = null;
        public SystemWriter? system { get; set; } = null;
        public ComponentWriter? component { get; set; } = null;
        public FieldSetWriter? fieldSet { get; set; } = null;
        public FieldWriter? field { get; set; } = null;
        public List<ActionWriter> actionList { get; set; } = new List<ActionWriter>();

        // return list of code writers based on WriterInfo, Each PlaceHolder maps to one type of writer.
        public IEnumerable<ICodeWriter> GetWriterList(WriterType writerType)
        {
            var list = Enumerable.Empty<ICodeWriter>();
            switch (writerType)
            {
                case WriterType.System:
                     list = system != null ? new List<ICodeWriter>() { system } : Enumerable.Empty<ICodeWriter>();
                    break;
                case WriterType.Component:
                    list = component != null ? new List<ICodeWriter>() { component } : Enumerable.Empty<ICodeWriter>();
                    break;
                case WriterType.FieldSet:
                    var currentFieldSet = component != null ? new FieldSetWriter(component.FieldList) : null;
                    list = currentFieldSet != null ? new List<ICodeWriter>() { currentFieldSet } : Enumerable.Empty<ICodeWriter>();
                    break;
                case WriterType.Field:
                    list = component != null ? component.FieldList : Enumerable.Empty<ICodeWriter>();
                    break;
                case WriterType.Action:
                    list = new List<ICodeWriter>() { new ActionWriter() };  
                    break;
            }

            return list;
        }
    }
}