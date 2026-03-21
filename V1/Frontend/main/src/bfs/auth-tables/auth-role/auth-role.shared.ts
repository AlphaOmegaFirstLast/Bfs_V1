
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AuthRoleColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IAuthRole {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initAuthRole(): IAuthRole {
    let entity: IAuthRole = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function authRoleUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IAuthRoleWithLookup extends IAuthRole{

}
//---------------------------------------------------------
export interface IAuthRoleRequest extends IEntityRequest<IAuthRoleFilter> {}

//---------------------------------------------------------
export interface IAuthRoleFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initAuthRoleRequest(): IAuthRoleRequest {
    let request: IAuthRoleRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AuthRoleColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAuthRoleActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/auth-role/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-role/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-role/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-role/delete', displayText: 'Delete...' 
});

        return links;
    }
    //---------------------------------------------------------

