import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemTemplateColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },
{ fieldName: 'projectType', displayName: 'Project Type', sortName:'ProjectType', width: '50px', isVisible:true },
{ fieldName: 'outputDirectory', displayName: 'Output Directory', sortName:'OutputDirectory', width: '50px', isVisible:true },
{ fieldName: 'solutionDirectory', displayName: 'Solution Directory', sortName:'SolutionDirectory', width: '50px', isVisible:true },
{ fieldName: 'template', displayName: 'Template', sortName:'Template', width: '50px', isVisible:true },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function systemTemplateUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
projectType: [''],
outputDirectory: [''],
solutionDirectory: [''],
template: [''],

    };
} 
//---------------------------------------------------------

export interface ISystemTemplate {
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
export interface ISystemTemplateWithLookup extends ISystemTemplate{

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
export interface ISystemTemplateRequest extends IEntityRequest<ISystemTemplateFilter> {}

//---------------------------------------------------------
export interface ISystemTemplateFilter {
    [key: string]: any;

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

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

