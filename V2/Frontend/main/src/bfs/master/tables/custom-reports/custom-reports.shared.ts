import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const CustomReportsColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'request', displayName: 'Request', sortName: 'Request', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'baseReport', displayName: 'Base Report', sortName: 'BaseReport', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'isPrivate', displayName: 'Private', sortName: 'IsPrivate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'createdBy', displayName: 'Created By', sortName: 'CreatedBy', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'url', displayName: 'Base Report Url', sortName: 'Url', width: '50px', isVisible:true, columnOrder:1 },

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
    id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
request: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
baseReport: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isPrivate: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
createdBy: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
url: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

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
    Id?: string;

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
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderCustomReports(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
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
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

