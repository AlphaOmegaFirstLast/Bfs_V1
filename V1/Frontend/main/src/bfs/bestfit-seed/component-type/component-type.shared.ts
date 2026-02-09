import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ComponentTypeColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function componentTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------

export interface IComponentType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export interface IComponentTypeWithLookup extends IComponentType{

}
//---------------------------------------------------------

export function initComponentType(): IComponentType {
    let entity: IComponentType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IComponentTypeRequest extends IEntityRequest<IComponentTypeFilter> {}

//---------------------------------------------------------
export interface IComponentTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initComponentTypeRequest(): IComponentTypeRequest {
    let request: IComponentTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ComponentTypeColumns.map(column => ({ ...column })),
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

