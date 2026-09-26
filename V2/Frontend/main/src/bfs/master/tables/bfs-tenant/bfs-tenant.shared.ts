import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

import { ICustomField, initCustomFields } from "@bfs/_shared/customFields";

// Output Columns of a Query  [used in entity Query]
export const BfsTenantColumns = [
    { fieldName: 'dbConnection', displayName: 'Database Connection', sortName: 'DbConnection', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'customFields', displayName: 'Custom Fields', sortName: 'CustomFields', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'companyName', displayName: 'Company Name', sortName: 'CompanyName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'logo', displayName: 'Logo', sortName: 'Logo', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'theme', displayName: 'Theme', sortName: 'Theme', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBfsTenant {
    dbConnection?: string;
isDeleted?: boolean;
id?: string;
notes?: string;
name?: string;
companyName?: string;
logo?: string;
theme?: string;

    customFields?: ICustomField[];

}
//---------------------------------------------------------
export function initBfsTenant(): IBfsTenant {
    let entity: IBfsTenant = {
        dbConnection: '',
isDeleted: false,
id: '0',
notes: '',
name: '',
companyName: '',
logo: '',
theme: '',

        customFields: initCustomFields(),

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsTenantUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    dbConnection: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"300","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
companyName: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
logo: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"300","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
theme: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    customFields: formBuilder.array([]),

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

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
export function renderBfsTenant(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
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
//Template_Start_Code_DontOverwrite_3
export function isVisible(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'toPortfolioId':
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
        case 'toPortfolioId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//Template_End_Code_DontOverwrite_3

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

