
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsTenantSystemColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsTenantId', displayName: 'Tenant Name', sortName: 'BfsTenant', width: '50px', isVisible:true },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem', width: '50px', isVisible:true },

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

            BfsTenantId: undefined ,
BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsTenantSystemActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

