using Admin.App.Constants;
using System;

namespace Admin.App
{
    public class BfsSystemActionEntity : IBestFitAction
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public long BfsComponentId { get; set; }
        public string ActionTemplate { get; set; }

        public ActionType ActionType { get; set; }
        public ActionLocation ActionLocation { get; set; }
        public ActionSource ActionSource { get; set; }
        public WriterType WriterType { get; set; }
        public string MatchProperty { get; set; }
        public string[] MatchValues { get; set; }
        public string Notes { get; set; }


        public static List<BfsSystemActionEntity> GenerateTestData()
        {
            var list = new List<BfsSystemActionEntity>
            {
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "Add New Record",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionType = ActionType.FrontendLink,
                ActionLocation = ActionLocation.ListHeader,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: 0, route:'/[SystemPrefixSmall]/[ComponentFileName]/add', displayText: 'Add New record' " ,
                Notes = "Redirect user to the form in view mode."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "View Record",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionType = ActionType.FrontendLink,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: record['[ComponentNameSmall]Id'], route:'/[SystemPrefixSmall]/[ComponentFileName]/view', displayText: 'View...' " ,
                Notes = "Redirect user to the form in view mode."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 2,
                Name = "Edit Record",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionType = ActionType.FrontendLink,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: record['[ComponentNameSmall]Id'], route:'/[SystemPrefixSmall]/[ComponentFileName]/edit', displayText: 'Edit...' " ,
                Notes = "Redirect user to the form in edit mode."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "Delete Record",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionType = ActionType.FrontendLink,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: record['[ComponentNameSmall]Id'], route:'/[SystemPrefixSmall]/[ComponentFileName]/delete', displayText: 'Delete...' " ,
                Notes = "Redirect user to the form in delete mode."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "Go To Lookup",
                BfsComponentId = 1,
                WriterType = WriterType.Field,
                ActionType = ActionType.FrontendLink,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[FieldDefinition]",
                MatchValues = "Lookup".Split(','),
                ActionTemplate = "recordId: record['[LookupNameSmall]Id'], route:'/[SystemPrefixSmall]/[LookupFileName]/view', displayText:'Go to [LookupNameCapital]' ",
                Notes = "Redirect user to the Lookup form in view mode."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "Duplicate Record",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionType = ActionType.FrontendFunction,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: record['[ComponentNameSmall]Id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['[ComponentNameSmall]Id'], postUrl: '/[ComponentNameCapital]', onSuccessMethodName: 'getReport' } " ,
                Notes = "Execute frontend function 'duplicateRecord' which uses the data arguments to post data and refresh list."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "Duplicate Tree",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionType = ActionType.FrontendFunction,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: record['[ComponentNameSmall]Id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['[ComponentNameSmall]Id'], postUrl: '/Operations/[ComponentNameCapital]/DuplicateTree', onSuccessMethodName: 'getReport' } " ,
                Notes = "Execute frontend function 'duplicateTree' to duplicate the record and its children. it uses the data arguments to post data and refresh list."
            },
             new BfsSystemActionEntity
            {
                TenantId = 0,
                IsDeleted = false,
                Id = 1,
                Name = "Delete Tree",
                BfsComponentId = 1,
                WriterType = WriterType.Component,
                ActionLocation = ActionLocation.ListRow,
                ActionSource = ActionSource.System,
                MatchProperty = "[ComponentType]",
                MatchValues = "Table".Split(','),
                ActionTemplate = "recordId: record['[ComponentNameSmall]Id'], action: deleteTree, displayText: 'Delete Tree', data: { recordId: record['[ComponentNameSmall]Id'], postUrl: '/Operations/[ComponentNameCapital]/DeleteTree', onSuccessMethodName: 'getReport' } " ,
                Notes = "Execute frontend function 'deleteTree' to delete the record and its children. it uses the data arguments to post data and refresh list."
            },

            };
            return list;
        }     
    }
}