
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BackendDataTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBackendDataType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initBackendDataType(): IBackendDataType {
    let entity: IBackendDataType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function backendDataTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IBackendDataTypeWithLookup extends IBackendDataType{

}
//---------------------------------------------------------
export interface IBackendDataTypeRequest extends IEntityRequest<IBackendDataTypeFilter> {}

//---------------------------------------------------------
export interface IBackendDataTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initBackendDataTypeRequest(): IBackendDataTypeRequest {
    let request: IBackendDataTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BackendDataTypeColumns.map(column => ({ ...column })),
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

export function getBackendDataTypeActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/backend-data-type/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/backend-data-type/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/backend-data-type/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/backend-data-type/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl: '/BackendDataType', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/BackendDataType/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

