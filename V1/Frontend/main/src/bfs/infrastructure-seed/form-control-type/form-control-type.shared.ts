
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const FormControlTypeColumns = [
    { fieldName: 'formControlTypeId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'formControlTypeName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'formControlTypeNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IFormControlType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initFormControlType(): IFormControlType {
    let entity: IFormControlType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function formControlTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IFormControlTypeWithLookup extends IFormControlType{

}
//---------------------------------------------------------
export interface IFormControlTypeRequest extends IEntityRequest<IFormControlTypeFilter> {}

//---------------------------------------------------------
export interface IFormControlTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initFormControlTypeRequest(): IFormControlTypeRequest {
    let request: IFormControlTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: FormControlTypeColumns.map(column => ({ ...column })),
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

export function getFormControlTypeActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/form-control-type/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['formControlTypeId'], route:'/bfs/form-control-type/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['formControlTypeId'], route:'/bfs/form-control-type/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['formControlTypeId'], route:'/bfs/form-control-type/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['formControlTypeId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/FormControlType', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['formControlTypeId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/FormControlType/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

