
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'shortName', displayName: 'Short Name', sortName: 'ShortName', width: '50px', isVisible:true },
{ fieldName: 'actionTypeId', displayName: 'Action Type', sortName: 'ActionType_Name', width: '50px', isVisible:true },
{ fieldName: 'writerTypeId', displayName: 'Writer Type', sortName: 'WriterType_Name', width: '50px', isVisible:true },
{ fieldName: 'matchProperty', displayName: 'Writer Matching Property', sortName: 'MatchProperty', width: '50px', isVisible:true },
{ fieldName: 'matchValues', displayName: 'Writer Matching Values', sortName: 'MatchValues', width: '50px', isVisible:true },
{ fieldName: 'actionTemplate', displayName: 'Action Template', sortName: 'ActionTemplate', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

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
    isDeleted: [false],
id: ['0'],
shortName: [''],
matchProperty: [''],
matchValues: [''],
actionTemplate: [''],
name: [''],
notes: [''],

    actionTypeId: [0],
writerTypeId: [0],

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

