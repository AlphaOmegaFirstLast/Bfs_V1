
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomFieldDefinitionColumns = [
    { fieldName: 'customFieldDefinitionId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'customFieldDefinitionName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'customFieldDefinitionNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },
{ fieldName: 'customFieldDefinitionFieldValidation', displayName: 'Field Validation', sortName: 'FieldValidation', width: '50px', isVisible:true },
{ fieldName: 'customFieldDefinitionDisplayName', displayName: 'DisplayName', sortName: 'DisplayName', width: '50px', isVisible:true },
{ fieldName: 'customFieldDefinitionBfsComponentId', displayName: 'Component', sortName: 'BfsComponent', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ICustomFieldDefinition {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
displayName?: string;

    bfsComponentId?: string;

    fieldValidation?: IFieldValidation;

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
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
displayName: [''],

    bfsComponentId: ['0'],

    fieldValidation: fieldValidationUntypedFormGroup(formBuilder),

    };
} 
//---------------------------------------------------------
export interface ICustomFieldDefinitionWithLookup extends ICustomFieldDefinition{

    bfsComponentName?: string;

}
//---------------------------------------------------------
export interface ICustomFieldDefinitionRequest extends IEntityRequest<ICustomFieldDefinitionFilter> {}

//---------------------------------------------------------
export interface ICustomFieldDefinitionFilter {
    [key: string]: any;

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

            Name: undefined ,

            BfsComponentId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getCustomFieldDefinitionActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/custom-field-definition/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['customFieldDefinitionId'], route:'/bfs/custom-field-definition/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['customFieldDefinitionId'], route:'/bfs/custom-field-definition/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['customFieldDefinitionId'], route:'/bfs/custom-field-definition/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/view', displayText:'Go to BfsComponent' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['customFieldDefinitionId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/CustomFieldDefinition', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['customFieldDefinitionId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/CustomFieldDefinition/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

