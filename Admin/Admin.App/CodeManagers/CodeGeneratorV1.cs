using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.App.CodeManagers
{
    public class CodeGeneratorV1: CodeGeneratorBase
    {

        public override void ReadDbEntities()

        {
            // populate database objects
            using (var context = new V1DbContext())
            {
                try
                {
                    SystemList = context.SystemInfo.Select(x => (IBestFitSystem)x).ToList();
                    ComponentList = context.Component.Select(x => (IBestFitComponent)x).ToList();
                    FieldList = context.TableField.Select(x => (IBestFitField)x).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

    }
}
