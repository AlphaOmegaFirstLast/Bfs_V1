import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const SystemTemplateColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'projectType', displayName: 'Project Type', sortName: 'ProjectType', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'outputDirectory', displayName: 'Output Directory', sortName: 'OutputDirectory', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'solutionDirectory', displayName: 'Solution Directory', sortName: 'SolutionDirectory', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'template', displayName: 'Template', sortName: 'Template', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ISystemTemplate extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
projectType?: string;
outputDirectory?: string;
solutionDirectory?: string;
template?: string;

}
//---------------------------------------------------------
export function initSystemTemplate(): ISystemTemplate {
    let entity: ISystemTemplate = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
projectType: '',
outputDirectory: '',
solutionDirectory: '',
template: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function systemTemplateUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
projectType: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
outputDirectory: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
solutionDirectory: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
template: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ISystemTemplateRequest extends IEntityRequest<ISystemTemplateFilter> {}

//---------------------------------------------------------
export interface ISystemTemplateFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initSystemTemplateRequest(): ISystemTemplateRequest {
    let request: ISystemTemplateRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SystemTemplateColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderSystemTemplate(record: ISystemTemplate, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ISystemTemplate];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getSystemTemplateActions(component: any, record: ISystemTemplate): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('systemTemplate', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/system-template/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('systemTemplate', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/system-template/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('systemTemplate', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/system-template/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('systemTemplate', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/system-template/delete', displayText: 'Delete...' 
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ISystemTemplate, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

