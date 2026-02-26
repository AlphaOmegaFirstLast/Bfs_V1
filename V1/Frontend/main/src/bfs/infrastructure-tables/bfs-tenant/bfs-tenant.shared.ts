
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { ICustomField, initCustomFields } from "@bfs/_shared/customFields";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsTenantColumns = [
    { fieldName: 'dbConnection', displayName: 'Database Connection', sortName: 'DbConnection', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'customFields', displayName: 'Custom Fields', sortName: 'CustomFields', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBfsTenant {
    dbConnection?: string;
isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    customFields?: ICustomField[];

}
//---------------------------------------------------------
export function initBfsTenant(): IBfsTenant {
    let entity: IBfsTenant = {
        dbConnection: '',
isDeleted: false,
id: '0',
name: '',
notes: '',

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
name: [''],
notes: [''],

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

    Name?: string;

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

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsTenantActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

