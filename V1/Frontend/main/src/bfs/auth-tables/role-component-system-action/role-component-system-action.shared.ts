
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const RoleComponentSystemActionColumns = [
    { fieldName: 'roleComponentSystemAction_Id', displayName: 'ID', sortName: 'RoleComponentSystemAction_Id', width: '50px', isVisible:false },
{ fieldName: 'roleComponentSystemAction_BfsComponentId', displayName: 'Component Name', sortName: 'BfsComponent_Name', width: '50px', isVisible:true },
{ fieldName: 'roleComponentSystemAction_SystemActionId', displayName: 'System Action', sortName: 'SystemAction_Name', width: '50px', isVisible:true },
{ fieldName: 'roleComponentSystemAction_RoleId', displayName: 'Role', sortName: 'Role_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IRoleComponentSystemAction {
    isDeleted?: boolean;
id?: string;

    bfsComponentId?: string;
systemActionId?: string;
roleId?: string;

}
//---------------------------------------------------------
export function initRoleComponentSystemAction(): IRoleComponentSystemAction {
    let entity: IRoleComponentSystemAction = {
        isDeleted: false,
id: '0',

        bfsComponentId: '0',
systemActionId: '0',
roleId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleComponentSystemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    bfsComponentId: ['0'],
systemActionId: ['0'],
roleId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IRoleComponentSystemActionWithLookup extends IRoleComponentSystemAction{

    bfsComponentName?: string;
systemActionName?: string;
roleName?: string;

}
//---------------------------------------------------------
export interface IRoleComponentSystemActionRequest extends IEntityRequest<IRoleComponentSystemActionFilter> {}

//---------------------------------------------------------
export interface IRoleComponentSystemActionFilter {
    [key: string]: any;
    Id?: string;

    BfsComponentId?: string;
SystemActionId?: string;
RoleId?: string;

}
//---------------------------------------------------------
export function initRoleComponentSystemActionRequest(): IRoleComponentSystemActionRequest {
    let request: IRoleComponentSystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleComponentSystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            BfsComponentId: undefined ,
SystemActionId: undefined ,
RoleId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getRoleComponentSystemActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role-component-system-action/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role-component-system-action/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role-component-system-action/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/ath/bfs-component/view', displayText:'Go to BfsComponent'
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemActionId'], route:'/ath/system-action/view', displayText:'Go to SystemAction'
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['roleId'], route:'/ath/role/view', displayText:'Go to Role'
});
}

        return links;
    }
    //---------------------------------------------------------

