import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ActionTypeColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },

];
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

export interface IActionType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export interface IActionTypeWithLookup extends IActionType{

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

