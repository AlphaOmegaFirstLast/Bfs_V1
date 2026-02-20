using Admin.App.Constants;
using Admin.App;
using Admin.App;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Admin.App
{
    public class CodeGeneratorV4 : CodeGeneratorBase
    {

        public override void ReadDbEntities()
        {
            //var z = BfsSystemActionEntity.GenerateTestData().Select(x => new BestFitAction(x)).ToList();
            //ComponentSystemActionList = z; //.Select( x=> (IBestFitAction)x).ToList();
            var componentSystemActionList = new List<BfsComponentSystemActionEntity>();
            // populate database objects
            using (var context = new V4DbContext())
            {
                try
                {
                    SystemList = context.BfsSystem.Select(x => (ISystemEntity)x).ToList();
                    ComponentList = context.BfsComponent.Select(x => (IComponentEntity)x).ToList();
                    FieldList = context.BfsField.Select(x => (IFieldEntity)x).ToList();
                    SystemActionList = context.SystemAction.Select(x => (IActionEntity)x).ToList();
                    componentSystemActionList = context.BfsComponentSystemAction.ToList();

                    ComponentActionList = (from cs in componentSystemActionList
                                           join sa in SystemActionList on cs.SystemActionId equals sa.Id into gj
                                           from sa in gj.DefaultIfEmpty()
                                           select new ActionWriter
                                           {
                                               Id = sa?.Id ?? cs.Id,
                                               Name = sa?.Name ?? string.Empty,
                                               BfsComponentId = cs.BfsComponentId,
                                               ActionTemplate = sa?.ActionTemplate ?? string.Empty,
                                               ActionTypeId = sa?.ActionTypeId ?? ActionType.None,
                                               ActionLocationId = cs.ActionLocationId,
                                               ActionSourceId = ActionSource.System,
                                               WriterTypeId = sa?.WriterTypeId ?? WriterType.None,
                                               MatchProperty = sa?.MatchProperty ?? string.Empty,
                                               MatchValues = (sa?.MatchValues ?? string.Empty)
                                                             .Split(',')
                                                             .Select(s => s.Trim())
                                                             .Where(s => !string.IsNullOrEmpty(s))
                                                             .ToArray(),
                                               Notes = sa?.Notes ?? string.Empty,
                                           }).ToList();


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}