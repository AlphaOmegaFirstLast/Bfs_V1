
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemActionColumns = [
    { fieldName: 'systemActionId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'systemActionName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'systemActionNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },
{ fieldName: 'systemActionActionTypeId', displayName: 'Action Type', sortName: 'ActionType', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ISystemAction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    actionTypeId?: number;

}
//---------------------------------------------------------
export function initSystemAction(): ISystemAction {
    let entity: ISystemAction = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        actionTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function systemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    actionTypeId: [0],

    };
} 
//---------------------------------------------------------
export interface ISystemActionWithLookup extends ISystemAction{

    actionTypeName?: string;

}
//---------------------------------------------------------
export interface ISystemActionRequest extends IEntityRequest<ISystemActionFilter> {}

//---------------------------------------------------------
export interface ISystemActionFilter {
    [key: string]: any;

    Name?: string;

    ActionTypeId?: number;

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

            Name: undefined ,

            ActionTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getSystemActionActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/system-action/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemActionId'], route:'/bfs/system-action/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemActionId'], route:'/bfs/system-action/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemActionId'], route:'/bfs/system-action/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionTypeId'], route:'/bfs/action-type/view', displayText:'Go to ActionType' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['systemActionId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/SystemAction', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['systemActionId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/SystemAction/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

