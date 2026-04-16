
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { IFieldValidation, initFieldValidation, fieldValidationUntypedFormGroup } from "@bfs/_shared/objectFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomFieldDefinitionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },
{ fieldName: 'fieldValidation', displayName: 'Field Validation', sortName: 'FieldValidationName', width: '50px', isVisible:false },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName: 'DisplayNameName', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentId', displayName: 'Component', sortName: 'BfsComponentName', width: '50px', isVisible:true },

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

export function getCustomFieldDefinitionActions(component: any, record: IEntity): IAction[] {
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

