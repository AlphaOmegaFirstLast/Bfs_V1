
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ActionLocationColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IActionLocation {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initActionLocation(): IActionLocation {
    let entity: IActionLocation = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function actionLocationUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IActionLocationWithLookup extends IActionLocation{

}
//---------------------------------------------------------
export interface IActionLocationRequest extends IEntityRequest<IActionLocationFilter> {}

//---------------------------------------------------------
export interface IActionLocationFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initActionLocationRequest(): IActionLocationRequest {
    let request: IActionLocationRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ActionLocationColumns.map(column => ({ ...column })),
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

export function getActionLocationActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/action-location/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/action-location/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/action-location/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/action-location/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl: '/ActionLocation', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/ActionLocation/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

