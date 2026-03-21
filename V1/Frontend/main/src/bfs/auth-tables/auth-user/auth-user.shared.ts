
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const AuthUserColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'aspNetUserId', displayName: 'AspNetUserId', sortName: 'AspNetUser', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IAuthUser {
    isDeleted?: boolean;
id?: string;
aspNetUserId?: string;
notes?: string;
name?: string;

}
//---------------------------------------------------------
export function initAuthUser(): IAuthUser {
    let entity: IAuthUser = {
        isDeleted: false,
id: '0',
aspNetUserId: '',
notes: '',
name: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function authUserUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
aspNetUserId: [''],
notes: [''],
name: [''],

    };
} 
//---------------------------------------------------------
export interface IAuthUserWithLookup extends IAuthUser{

}
//---------------------------------------------------------
export interface IAuthUserRequest extends IEntityRequest<IAuthUserFilter> {}

//---------------------------------------------------------
export interface IAuthUserFilter {
    [key: string]: any;

    AspNetUserId?: string;
Name?: string;

}
//---------------------------------------------------------
export function initAuthUserRequest(): IAuthUserRequest {
    let request: IAuthUserRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: AuthUserColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            AspNetUserId: undefined ,
Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getAuthUserActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('authUser', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/auth-user/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('authUser', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-user/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('authUser', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-user/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('authUser', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/auth-user/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

