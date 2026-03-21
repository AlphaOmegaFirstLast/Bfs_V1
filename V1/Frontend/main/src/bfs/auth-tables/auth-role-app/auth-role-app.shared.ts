
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AuthRoleAppColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'authRoleId', displayName: 'Role', sortName: 'AuthRole', width: '50px', isVisible:true },
{ fieldName: 'authAppId', displayName: 'System Application', sortName: 'AuthApp', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IAuthRoleApp {
    isDeleted?: boolean;
id?: string;

    authRoleId?: string;
authAppId?: string;

}
//---------------------------------------------------------
export function initAuthRoleApp(): IAuthRoleApp {
    let entity: IAuthRoleApp = {
        isDeleted: false,
id: '0',

        authRoleId: '0',
authAppId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function authRoleAppUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    authRoleId: ['0'],
authAppId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IAuthRoleAppWithLookup extends IAuthRoleApp{

    authRoleName?: string;
authAppName?: string;

}
//---------------------------------------------------------
export interface IAuthRoleAppRequest extends IEntityRequest<IAuthRoleAppFilter> {}

//---------------------------------------------------------
export interface IAuthRoleAppFilter {
    [key: string]: any;

    AuthRoleId?: string;
AuthAppId?: string;

}
//---------------------------------------------------------
export function initAuthRoleAppRequest(): IAuthRoleAppRequest {
    let request: IAuthRoleAppRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AuthRoleAppColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            AuthRoleId: undefined ,
AuthAppId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAuthRoleAppActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

