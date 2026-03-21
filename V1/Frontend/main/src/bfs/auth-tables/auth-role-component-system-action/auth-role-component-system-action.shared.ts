
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AuthRoleComponentSystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponent', width: '50px', isVisible:true },
{ fieldName: 'systemActionId', displayName: 'System Action', sortName: 'SystemAction', width: '50px', isVisible:true },
{ fieldName: 'authRoleId', displayName: 'Role', sortName: 'AuthRole', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IAuthRoleComponentSystemAction {
    isDeleted?: boolean;
id?: string;

    bfsComponentId?: string;
systemActionId?: string;
authRoleId?: string;

}
//---------------------------------------------------------
export function initAuthRoleComponentSystemAction(): IAuthRoleComponentSystemAction {
    let entity: IAuthRoleComponentSystemAction = {
        isDeleted: false,
id: '0',

        bfsComponentId: '0',
systemActionId: '0',
authRoleId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function authRoleComponentSystemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    bfsComponentId: ['0'],
systemActionId: ['0'],
authRoleId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IAuthRoleComponentSystemActionWithLookup extends IAuthRoleComponentSystemAction{

    bfsComponentName?: string;
systemActionName?: string;
authRoleName?: string;

}
//---------------------------------------------------------
export interface IAuthRoleComponentSystemActionRequest extends IEntityRequest<IAuthRoleComponentSystemActionFilter> {}

//---------------------------------------------------------
export interface IAuthRoleComponentSystemActionFilter {
    [key: string]: any;

    BfsComponentId?: string;
SystemActionId?: string;
AuthRoleId?: string;

}
//---------------------------------------------------------
export function initAuthRoleComponentSystemActionRequest(): IAuthRoleComponentSystemActionRequest {
    let request: IAuthRoleComponentSystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AuthRoleComponentSystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            BfsComponentId: undefined ,
SystemActionId: undefined ,
AuthRoleId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAuthRoleComponentSystemActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-role-component-system-action/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-role-component-system-action/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-role-component-system-action/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/bfs/bfs-component/view', displayText:'Go to BfsComponent'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemActionId'], route:'/bfs/system-action/view', displayText:'Go to SystemAction'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['authRoleId'], route:'/bfs/auth-role/view', displayText:'Go to AuthRole'
});

        return links;
    }
    //---------------------------------------------------------

