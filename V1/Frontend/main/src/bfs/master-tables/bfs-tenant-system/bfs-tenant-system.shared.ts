
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsTenantSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'bfsTenantId', displayName: 'Tenant Name', sortName: 'BfsTenant_Name', width: '50px', isVisible:true },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsTenantSystem {
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
    isDeleted: [false],
id: ['0'],

    bfsTenantId: ['0'],
bfsSystemId: ['0'],

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
if (component.accessService.isActionAllowed('bfsTenantSystem', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/mstr/bfs-system/view', displayText:'Go to BfsSystem'
});
}

        return links;
    }
    //---------------------------------------------------------

