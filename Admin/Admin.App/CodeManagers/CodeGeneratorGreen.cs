using Admin.App.Constants;
using Admin.App;
using Admin.App;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Admin.App
{
    public class CodeGeneratorGreen : CodeGeneratorBase
    {

        public override void ReadDbEntities()
        {
            var z = BfsSystemActionEntity.GenerateTestData().Select(x => new BestFitAction(x)).ToList();
            SystemActionList = z; //.Select( x=> (IBestFitAction)x).ToList();

            // populate database objects
            using (var context = new V4DbContext())
            {
                try
                {
                    SystemList = context.BfsSystem.Select(x => (IBestFitSystem)x).ToList();
                    ComponentList = context.BfsComponent.Select(x => (IBestFitComponent)x).ToList();
                    FieldList = context.BfsField.Select(x => (IBestFitField)x).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}