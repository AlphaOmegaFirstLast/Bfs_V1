
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BusinessActionColumns = [
    { fieldName: 'businessActionId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'businessActionName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'businessActionNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },
{ fieldName: 'businessActionActionTypeId', displayName: 'Action Type', sortName: 'ActionType', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBusinessAction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    actionTypeId?: number;

}
//---------------------------------------------------------
export function initBusinessAction(): IBusinessAction {
    let entity: IBusinessAction = {
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
export function businessActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    actionTypeId: [0],

    };
} 
//---------------------------------------------------------
export interface IBusinessActionWithLookup extends IBusinessAction{

    actionTypeName?: string;

}
//---------------------------------------------------------
export interface IBusinessActionRequest extends IEntityRequest<IBusinessActionFilter> {}

//---------------------------------------------------------
export interface IBusinessActionFilter {
    [key: string]: any;

    Name?: string;

    ActionTypeId?: number;

}
//---------------------------------------------------------
export function initBusinessActionRequest(): IBusinessActionRequest {
    let request: IBusinessActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BusinessActionColumns.map(column => ({ ...column })),
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

export function getBusinessActionActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/business-action/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['businessActionId'], route:'/bfs/business-action/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['businessActionId'], route:'/bfs/business-action/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['businessActionId'], route:'/bfs/business-action/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionTypeId'], route:'/bfs/action-type/view', displayText:'Go to ActionType' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['businessActionId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/BusinessAction', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['businessActionId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/BusinessAction/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

