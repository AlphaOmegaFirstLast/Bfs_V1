import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";

// Output Columns of a Query  [used in entity Query]
export const CustomFieldDefinitionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'bfsComponentId', displayName: 'Component', sortName: 'BfsComponent_Name', width: '50px', isVisible:true, columnOrder:1 },

    { fieldName: 'fieldValidation', displayName: 'FieldValidation', sortName: 'jsonFieldValidation', width: '50px', isVisible:false },
    { fieldName: 'jsonFieldValidation', displayName: 'Json FieldValidation', sortName: 'jsonFieldValidation', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface ICustomFieldDefinition extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
displayName?: string;

    bfsComponentId?: string;

    fieldValidation?: IFieldValidation;
    jsonFieldValidation?: string;

}
//---------------------------------------------------------
export function initCustomFieldDefinition(): ICustomFieldDefinition {
    let entity: ICustomFieldDefinition = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
displayName: '',

        bfsComponentId: '0',

        fieldValidation: initFieldValidation(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function customFieldDefinitionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
displayName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    fieldValidation: fieldValidationUntypedFormGroup(formBuilder),

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ICustomFieldDefinitionRequest extends IEntityRequest<ICustomFieldDefinitionFilter> {}

//---------------------------------------------------------
export interface ICustomFieldDefinitionFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    BfsComponentId?: string;

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
            Id: undefined ,

            Name: undefined ,

            BfsComponentId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderCustomFieldDefinition(record: ICustomFieldDefinition, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ICustomFieldDefinition];
        switch (column.fieldName) {
            case 'bfsComponentId':
                return record['bfsComponentName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getCustomFieldDefinitionActions(component: any, record: ICustomFieldDefinition): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('customFieldDefinition', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/custom-field-definition/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('customFieldDefinition', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/custom-field-definition/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('customFieldDefinition', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/custom-field-definition/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('customFieldDefinition', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/custom-field-definition/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('customFieldDefinition', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/mstr/bfs-component/view', displayText:'Go to BfsComponent'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ICustomFieldDefinition, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//---------------------------------------------------------
export function isEnabled(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

