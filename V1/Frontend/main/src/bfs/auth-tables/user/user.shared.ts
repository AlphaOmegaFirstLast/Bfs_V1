
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const UserColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'aspNetUserId', displayName: 'AspNetUserId', sortName: 'AspNetUserName', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IUser {
    isDeleted?: boolean;
id?: string;
aspNetUserId?: string;
notes?: string;
name?: string;

}
//---------------------------------------------------------
export function initUser(): IUser {
    let entity: IUser = {
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
export function userUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
aspNetUserId: [''],
notes: [''],
name: [''],

    };
} 
//---------------------------------------------------------
export interface IUserWithLookup extends IUser{

}
//---------------------------------------------------------
export interface IUserRequest extends IEntityRequest<IUserFilter> {}

//---------------------------------------------------------
export interface IUserFilter {
    [key: string]: any;

    AspNetUserId?: string;
Name?: string;

}
//---------------------------------------------------------
export function initUserRequest(): IUserRequest {
    let request: IUserRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: UserColumns.map(column => ({ ...column })),
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

export function getUserActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('user', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/ath/user/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('user', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('user', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('user', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

