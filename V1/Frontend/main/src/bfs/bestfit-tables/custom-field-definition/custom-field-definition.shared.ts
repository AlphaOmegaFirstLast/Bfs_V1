import { IEntityRequest } from "@bfs/_shared/interfaces";
import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomFieldDefinitionColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName:'DisplayName', width: '50px', isVisible:true },

    { fieldName: 'componentId', displayName: 'Component', sortName:'Component', width: '50px', isVisible:false },

    { fieldName: 'fieldValidation', displayName: 'Field Validation', sortName:'FieldValidation', width: '50px', isVisible:false },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function customFieldDefinitionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
displayName: [''],

    componentId: ['0'],

    fieldValidation: fieldValidationUntypedFormGroup(formBuilder),

    };
} 
//---------------------------------------------------------

export interface ICustomFieldDefinition {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
displayName?: string;

    componentId?: string;

    fieldValidation?: IFieldValidation;

}
//---------------------------------------------------------
export interface ICustomFieldDefinitionWithLookup extends ICustomFieldDefinition{

    bfsComponentName?: string;

}
//---------------------------------------------------------

export function initCustomFieldDefinition(): ICustomFieldDefinition {
    let entity: ICustomFieldDefinition = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
displayName: '',

        componentId: '0',

        fieldValidation: initFieldValidation(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface ICustomFieldDefinitionRequest extends IEntityRequest<ICustomFieldDefinitionFilter> {}

//---------------------------------------------------------
export interface ICustomFieldDefinitionFilter {
    [key: string]: any;

    Name?: string;

    ComponentId?: string;

}
//---------------------------------------------------------
export function initCustomFieldDefinitionRequest(): ICustomFieldDefinitionRequest {
    let request: ICustomFieldDefinitionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CustomFieldDefinitionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            ComponentId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

