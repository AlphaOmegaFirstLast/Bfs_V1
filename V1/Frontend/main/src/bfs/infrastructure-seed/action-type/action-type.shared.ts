
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ActionTypeColumns = [
    { fieldName: 'actionTypeId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'actionTypeName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'actionTypeNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IActionType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initActionType(): IActionType {
    let entity: IActionType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function actionTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IActionTypeWithLookup extends IActionType{

}
//---------------------------------------------------------
export interface IActionTypeRequest extends IEntityRequest<IActionTypeFilter> {}

//---------------------------------------------------------
export interface IActionTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initActionTypeRequest(): IActionTypeRequest {
    let request: IActionTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ActionTypeColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getActionTypeActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/action-type/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionTypeId'], route:'/bfs/action-type/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionTypeId'], route:'/bfs/action-type/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['actionTypeId'], route:'/bfs/action-type/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['actionTypeId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/ActionType', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['actionTypeId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/ActionType/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

