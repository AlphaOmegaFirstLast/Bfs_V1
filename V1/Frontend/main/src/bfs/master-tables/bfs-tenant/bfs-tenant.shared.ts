
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { ICustomField, initCustomFields } from "@bfs/_shared/customFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsTenantColumns = [
    { fieldName: 'dbConnection', displayName: 'Database Connection', sortName: 'DbConnection', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'theme', displayName: 'Theme', sortName: 'Theme', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'customFields', displayName: 'Custom Fields', sortName: 'CustomFields', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'companyName', displayName: 'Company Name', sortName: 'CompanyName', width: '50px', isVisible:true },
{ fieldName: 'logo', displayName: 'Logo', sortName: 'Logo', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBfsTenant {
    dbConnection?: string;
isDeleted?: boolean;
id?: string;
theme?: string;
notes?: string;
name?: string;
companyName?: string;
logo?: string;

    customFields?: ICustomField[];

}
//---------------------------------------------------------
export function initBfsTenant(): IBfsTenant {
    let entity: IBfsTenant = {
        dbConnection: '',
isDeleted: false,
id: '0',
theme: '',
notes: '',
name: '',
companyName: '',
logo: '',

        customFields: initCustomFields(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsTenantUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    dbConnection: [''],
isDeleted: [false],
id: ['0'],
theme: [''],
notes: [''],
name: [''],
companyName: [''],
logo: [''],

    customFields: formBuilder.array([]),

    };
} 
//---------------------------------------------------------
export interface IBfsTenantWithLookup extends IBfsTenant{

}
//---------------------------------------------------------
export interface IBfsTenantRequest extends IEntityRequest<IBfsTenantFilter> {}

//---------------------------------------------------------
export interface IBfsTenantFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;
CompanyName?: string;
Logo?: string;

}
//---------------------------------------------------------
export function initBfsTenantRequest(): IBfsTenantRequest {
    let request: IBfsTenantRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsTenantColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,
CompanyName: undefined ,
Logo: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsTenantActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('bfsTenant', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/bfs-tenant/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('bfsTenant', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-tenant/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('bfsTenant', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-tenant/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('bfsTenant', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-tenant/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

