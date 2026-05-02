
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const RoleUserColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'roleId', displayName: 'Role', sortName: 'RoleName', width: '50px', isVisible:true },
{ fieldName: 'userId', displayName: 'User', sortName: 'UserName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IRoleUser {
    isDeleted?: boolean;
id?: string;

    roleId?: string;
userId?: string;

}
//---------------------------------------------------------
export function initRoleUser(): IRoleUser {
    let entity: IRoleUser = {
        isDeleted: false,
id: '0',

        roleId: '0',
userId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleUserUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    roleId: ['0'],
userId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IRoleUserWithLookup extends IRoleUser{

    roleName?: string;
userName?: string;

}
//---------------------------------------------------------
export interface IRoleUserRequest extends IEntityRequest<IRoleUserFilter> {}

//---------------------------------------------------------
export interface IRoleUserFilter {
    [key: string]: any;
    Id?: string;

    RoleId?: string;
UserId?: string;

}
//---------------------------------------------------------
export function initRoleUserRequest(): IRoleUserRequest {
    let request: IRoleUserRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleUserColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            RoleId: undefined ,
UserId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getRoleUserActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

