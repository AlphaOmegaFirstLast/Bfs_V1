
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomReportsColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'request', displayName: 'Request', sortName: 'RequestName', width: '50px', isVisible:false },
{ fieldName: 'baseReport', displayName: 'Base Report', sortName: 'BaseReportName', width: '50px', isVisible:true },
{ fieldName: 'isPrivate', displayName: 'Private', sortName: 'IsPrivateName', width: '50px', isVisible:true },
{ fieldName: 'createdBy', displayName: 'Created By', sortName: 'CreatedByName', width: '50px', isVisible:true },
{ fieldName: 'url', displayName: 'Base Report Url', sortName: 'UrlName', width: '50px', isVisible:true },

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

export function getCustomReportsActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('customReports', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/custom-reports/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('customReports', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/custom-reports/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('customReports', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/custom-reports/delete', displayText: 'Delete...' 
});
}

if (component.accessService.isActionAllowed('customReports', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: component.goToCustomReport, displayText: 'Go To Custom Report', data: {'record':record}
});
}

        return links;
    }
    //---------------------------------------------------------

