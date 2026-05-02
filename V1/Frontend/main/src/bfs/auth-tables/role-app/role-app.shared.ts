
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const RoleAppColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'roleId', displayName: 'Role', sortName: 'RoleName', width: '50px', isVisible:true },
{ fieldName: 'appId', displayName: 'System Application', sortName: 'AppName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IRoleApp {
    isDeleted?: boolean;
id?: string;

    roleId?: string;
appId?: string;

}
//---------------------------------------------------------
export function initRoleApp(): IRoleApp {
    let entity: IRoleApp = {
        isDeleted: false,
id: '0',

        roleId: '0',
appId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleAppUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    roleId: ['0'],
appId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IRoleAppWithLookup extends IRoleApp{

    roleName?: string;
appName?: string;

}
//---------------------------------------------------------
export interface IRoleAppRequest extends IEntityRequest<IRoleAppFilter> {}

//---------------------------------------------------------
export interface IRoleAppFilter {
    [key: string]: any;
    Id?: string;

    RoleId?: string;
AppId?: string;

}
//---------------------------------------------------------
export function initRoleAppRequest(): IRoleAppRequest {
    let request: IRoleAppRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleAppColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            RoleId: undefined ,
AppId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getRoleAppActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

