
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'shortName', displayName: 'Short Name', sortName: 'ShortNameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },
{ fieldName: 'actionTypeId', displayName: 'Action Type', sortName: 'ActionTypeName', width: '50px', isVisible:true },
{ fieldName: 'writerTypeId', displayName: 'Writer Type', sortName: 'WriterTypeName', width: '50px', isVisible:true },
{ fieldName: 'matchProperty', displayName: 'Writer Matching Property', sortName: 'MatchPropertyName', width: '50px', isVisible:true },
{ fieldName: 'matchValues', displayName: 'Writer Matching Values', sortName: 'MatchValuesName', width: '50px', isVisible:true },
{ fieldName: 'actionTemplate', displayName: 'Action Template', sortName: 'ActionTemplateName', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ISystemAction {
    isDeleted?: boolean;
id?: string;
shortName?: string;
notes?: string;
matchProperty?: string;
matchValues?: string;
actionTemplate?: string;
name?: string;

    actionTypeId?: number;
writerTypeId?: number;

}
//---------------------------------------------------------
export function initSystemAction(): ISystemAction {
    let entity: ISystemAction = {
        isDeleted: false,
id: '0',
shortName: '',
notes: '',
matchProperty: '',
matchValues: '',
actionTemplate: '',
name: '',

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
notes: [''],
matchProperty: [''],
matchValues: [''],
actionTemplate: [''],
name: [''],

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

