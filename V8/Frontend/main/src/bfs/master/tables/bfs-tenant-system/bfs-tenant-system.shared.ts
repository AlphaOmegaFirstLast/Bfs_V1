import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const BfsTenantSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsTenantId', displayName: 'Tenant Name', sortName: 'BfsTenant_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBfsTenantSystem extends IEntity{
    isDeleted?: boolean;
id?: string;

    bfsTenantId?: string;
bfsSystemId?: string;

}
//---------------------------------------------------------
export function initBfsTenantSystem(): IBfsTenantSystem {
    let entity: IBfsTenantSystem = {
        isDeleted: false,
id: '0',

        bfsTenantId: '0',
bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsTenantSystemUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsTenantId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
bfsSystemId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IBfsTenantSystemRequest extends IEntityRequest<IBfsTenantSystemFilter> {}

//---------------------------------------------------------
export interface IBfsTenantSystemFilter {
    [key: string]: any;
    Id?: string;

    BfsTenantId?: string;
BfsSystemId?: string;

}
//---------------------------------------------------------
export function initBfsTenantSystemRequest(): IBfsTenantSystemRequest {
    let request: IBfsTenantSystemRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsTenantSystemColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            BfsTenantId: undefined ,
BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBfsTenantSystem(record: IBfsTenantSystem, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IBfsTenantSystem];
        switch (column.fieldName) {
            case 'bfsTenantId':
                return record['bfsTenantName']?.toString();
case 'bfsSystemId':
                return record['bfsSystemName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getBfsTenantSystemActions(component: any, record: IBfsTenantSystem): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-tenant-system/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-tenant-system/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/bfs-tenant-system/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/mstr/bfs-tenant-system/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsTenantId'], route:'/mstr/bfs-tenant/view', displayText:'Go to BfsTenant'
});
}
if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/mstr/bfs-system/view', displayText:'Go to BfsSystem'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IBfsTenantSystem, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
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
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

