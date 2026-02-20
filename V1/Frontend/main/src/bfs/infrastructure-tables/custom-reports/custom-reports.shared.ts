
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomReportsColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'request', displayName: 'Request', sortName: 'Request', width: '50px', isVisible:false },
{ fieldName: 'baseReport', displayName: 'Base Report', sortName: 'BaseReport', width: '50px', isVisible:true },
{ fieldName: 'isPrivate', displayName: 'Private', sortName: 'IsPrivate', width: '50px', isVisible:true },
{ fieldName: 'createdBy', displayName: 'Created By', sortName: 'CreatedBy', width: '50px', isVisible:true },
{ fieldName: 'url', displayName: 'Base Report Url', sortName: 'Url', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ICustomReports {
    id?: string;
name?: string;
request?: string;
baseReport?: string;
isPrivate?: boolean;
isDeleted?: boolean;
createdBy?: string;
url?: string;

}
//---------------------------------------------------------
export function initCustomReports(): ICustomReports {
    let entity: ICustomReports = {
        id: '0',
name: '',
request: '',
baseReport: '',
isPrivate: false,
isDeleted: false,
createdBy: '',
url: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function customReportsUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    id: ['0'],
name: [''],
request: [''],
baseReport: [''],
isPrivate: [false],
isDeleted: [false],
createdBy: [''],
url: [''],

    };
} 
//---------------------------------------------------------
export interface ICustomReportsWithLookup extends ICustomReports{

}
//---------------------------------------------------------
export interface ICustomReportsRequest extends IEntityRequest<ICustomReportsFilter> {}

//---------------------------------------------------------
export interface ICustomReportsFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initCustomReportsRequest(): ICustomReportsRequest {
    let request: ICustomReportsRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CustomReportsColumns.map(column => ({ ...column })),
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

export function getCustomReportsActions(component: any, record: IQueryColumn): IAction[] {
        let links: IAction[] = [];
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/custom-reports/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/custom-reports/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/custom-reports/delete', displayText: 'Delete...' 
});

links.push({
    actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: component.goToCustomReport, displayText: 'Go To Custom Report', data: {'record':record}
});

        return links;
    }
    //---------------------------------------------------------

