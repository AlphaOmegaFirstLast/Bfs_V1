
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomReportsColumns = [
    { fieldName: 'customReportsId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'customReportsName', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'customReportsRequest', displayName: 'Request', sortName: 'Request', width: '50px', isVisible:true },
{ fieldName: 'customReportsBaseReport', displayName: 'Base Report', sortName: 'BaseReport', width: '50px', isVisible:true },
{ fieldName: 'customReportsIsPrivate', displayName: 'Private', sortName: 'IsPrivate', width: '50px', isVisible:true },
{ fieldName: 'customReportsCreatedBy', displayName: 'Created By', sortName: 'CreatedBy', width: '50px', isVisible:true },
{ fieldName: 'customReportsUrl', displayName: 'Base Report Url', sortName: 'Url', width: '50px', isVisible:true },

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

export function getCustomReportsActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/custom-reports/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['customReportsId'], route:'/bfs/custom-reports/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['customReportsId'], route:'/bfs/custom-reports/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['customReportsId'], route:'/bfs/custom-reports/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['customReportsId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/CustomReports', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['customReportsId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/CustomReports/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

