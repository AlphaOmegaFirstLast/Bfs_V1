import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const SystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'shortName', displayName: 'Short Name', sortName: 'ShortName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'actionTypeId', displayName: 'Action Type', sortName: 'ActionType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'writerTypeId', displayName: 'Writer Type', sortName: 'WriterType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'matchProperty', displayName: 'Writer Matching Property', sortName: 'MatchProperty', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'matchValues', displayName: 'Writer Matching Values', sortName: 'MatchValues', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'actionTemplate', displayName: 'Action Template', sortName: 'ActionTemplate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface ISystemAction {
    isDeleted?: boolean;
id?: string;
shortName?: string;
matchProperty?: string;
matchValues?: string;
actionTemplate?: string;
name?: string;
notes?: string;

    actionTypeId?: number;
writerTypeId?: number;

}
//---------------------------------------------------------
export function initSystemAction(): ISystemAction {
    let entity: ISystemAction = {
        isDeleted: false,
id: '0',
shortName: '',
matchProperty: '',
matchValues: '',
actionTemplate: '',
name: '',
notes: '',

        actionTypeId: 0,
writerTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function systemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
shortName: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"1","MaxLength":"3","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
matchProperty: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
matchValues: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
actionTemplate: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    actionTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
writerTypeId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ISystemActionWithLookup extends ISystemAction{

    actionTypeName?: string;
writerTypeName?: string;

}
//---------------------------------------------------------
export interface ISystemActionRequest extends IEntityRequest<ISystemActionFilter> {}

//---------------------------------------------------------
export interface ISystemActionFilter {
    [key: string]: any;
    Id?: string;

    ShortName?: string;
MatchProperty?: string;
MatchValues?: string;
Name?: string;

    ActionTypeId?: number;
WriterTypeId?: number;

}
//---------------------------------------------------------
export function initSystemActionRequest(): ISystemActionRequest {
    let request: ISystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            ShortName: undefined ,
MatchProperty: undefined ,
MatchValues: undefined ,
Name: undefined ,

            ActionTypeId: undefined ,
WriterTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderSystemAction(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'actionTypeId':
                return record['actionTypeName']?.toString();
case 'writerTypeId':
                return record['writerTypeName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getSystemActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('systemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/system-action/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('systemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/system-action/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('systemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/system-action/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('systemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/system-action/delete', displayText: 'Delete...' 
});
}

if (component.accessService.isActionAllowed('systemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl:'/SystemAction', onSuccessMethodName: 'getReport' }
});
}

        return links;
    }
//---------------------------------------------------------
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

