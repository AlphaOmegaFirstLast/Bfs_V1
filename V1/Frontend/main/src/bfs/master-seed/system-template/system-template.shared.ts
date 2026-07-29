
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemTemplateColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'projectType', displayName: 'Project Type', sortName: 'ProjectType', width: '50px', isVisible:true },
{ fieldName: 'outputDirectory', displayName: 'Output Directory', sortName: 'OutputDirectory', width: '50px', isVisible:true },
{ fieldName: 'solutionDirectory', displayName: 'Solution Directory', sortName: 'SolutionDirectory', width: '50px', isVisible:true },
{ fieldName: 'template', displayName: 'Template', sortName: 'Template', width: '50px', isVisible:true },

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

export function getSystemTemplateActions(component: any, record: IEntity): IAction[] {
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

