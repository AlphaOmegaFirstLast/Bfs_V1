
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const RoleColumns = [
    { fieldName: 'role_Id', displayName: 'ID', sortName: 'Role_Id', width: '50px', isVisible:false },
{ fieldName: 'role_Name', displayName: 'Name', sortName: 'Role_Name', width: '50px', isVisible:true },
{ fieldName: 'role_Notes', displayName: 'Notes', sortName: 'Role_Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IRole {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initRole(): IRole {
    let entity: IRole = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IRoleWithLookup extends IRole{

}
//---------------------------------------------------------
export interface IRoleRequest extends IEntityRequest<IRoleFilter> {}

//---------------------------------------------------------
export interface IRoleFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initRoleRequest(): IRoleRequest {
    let request: IRoleRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getRoleActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('role', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/ath/role/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('role', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('role', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('role', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

