
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AuthRoleUserColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'authRoleId', displayName: 'Role', sortName: 'AuthRole', width: '50px', isVisible:true },
{ fieldName: 'authUserId', displayName: 'Users', sortName: 'AuthUser', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IAuthRoleUser {
    isDeleted?: boolean;
id?: string;

    authRoleId?: string;
authUserId?: string;

}
//---------------------------------------------------------
export function initAuthRoleUser(): IAuthRoleUser {
    let entity: IAuthRoleUser = {
        isDeleted: false,
id: '0',

        authRoleId: '0',
authUserId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function authRoleUserUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    authRoleId: ['0'],
authUserId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IAuthRoleUserWithLookup extends IAuthRoleUser{

    authRoleName?: string;
authUserName?: string;

}
//---------------------------------------------------------
export interface IAuthRoleUserRequest extends IEntityRequest<IAuthRoleUserFilter> {}

//---------------------------------------------------------
export interface IAuthRoleUserFilter {
    [key: string]: any;

    AuthRoleId?: string;
AuthUserId?: string;

}
//---------------------------------------------------------
export function initAuthRoleUserRequest(): IAuthRoleUserRequest {
    let request: IAuthRoleUserRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AuthRoleUserColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            AuthRoleId: undefined ,
AuthUserId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAuthRoleUserActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

