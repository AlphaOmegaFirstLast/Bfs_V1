import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

// Output Columns of a Query  [used in entity Query]
export const BfsTenantSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsTenantId', displayName: 'Tenant Name', sortName: 'BfsTenant_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBfsTenantSystem {
    isDeleted?: boolean;
id?: string;

    bfsTenantId?: string;

    bfsSystemId?: string;
    bfsSystemName?: string;

}
//---------------------------------------------------------
export function initBfsTenantSystem(): IBfsTenantSystem {
    let entity: IBfsTenantSystem = {
        isDeleted: false,
id: '0',

        bfsTenantId: '0',

        bfsSystemId: '0',
        bfsSystemName: '',

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
    bfsSystemName: [''],

    };
} 
//---------------------------------------------------------
export interface IBfsTenantSystemWithLookup extends IBfsTenantSystem{

    bfsTenantName?: string;

    bfsSystemName?: string;

}
//---------------------------------------------------------
export interface IBfsTenantSystemRequest extends IEntityRequest<IBfsTenantSystemFilter> {}

//---------------------------------------------------------
export interface IBfsTenantSystemFilter {
    [key: string]: any;
    Id?: string;

    BfsTenantId?: string;

    BfsSystemId?: string;
    BfsSystemName?: string,

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
            BfsSystemName: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBfsTenantSystem(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
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
export function getBfsTenantSystemActions(component: any, record: IEntity): IAction[] {
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

        return links;
    }
    //---------------------------------------------------------

