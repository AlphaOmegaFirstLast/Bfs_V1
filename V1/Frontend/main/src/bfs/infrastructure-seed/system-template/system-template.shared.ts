
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemTemplateColumns = [
    { fieldName: 'systemTemplateId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateNotes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateProjectType', displayName: 'Project Type', sortName: 'ProjectType', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateOutputDirectory', displayName: 'Output Directory', sortName: 'OutputDirectory', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateSolutionDirectory', displayName: 'Solution Directory', sortName: 'SolutionDirectory', width: '50px', isVisible:true },
{ fieldName: 'systemTemplateTemplate', displayName: 'Template', sortName: 'Template', width: '50px', isVisible:true },

];
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
export interface ISystemTemplateWithLookup extends ISystemTemplate{

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

export function getSystemTemplateActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/system-template/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemTemplateId'], route:'/bfs/system-template/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemTemplateId'], route:'/bfs/system-template/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemTemplateId'], route:'/bfs/system-template/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['systemTemplateId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/SystemTemplate', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['systemTemplateId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/SystemTemplate/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

