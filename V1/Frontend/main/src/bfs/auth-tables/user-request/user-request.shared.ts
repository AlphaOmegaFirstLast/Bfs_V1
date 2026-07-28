
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const UserRequestColumns = [
    { fieldName: 'userRequest_Id', displayName: 'ID', sortName: 'UserRequest_Id', width: '50px', isVisible:false },
{ fieldName: 'userRequest_Notes', displayName: 'Notes', sortName: 'UserRequest_Notes', width: '50px', isVisible:false },
{ fieldName: 'userRequest_Name', displayName: 'Name', sortName: 'UserRequest_Name', width: '50px', isVisible:true },
{ fieldName: 'userRequest_Email', displayName: 'Email', sortName: 'UserRequest_Email', width: '50px', isVisible:true },
{ fieldName: 'userRequest_UserId', displayName: 'User ID', sortName: 'UserRequest_UserId', width: '50px', isVisible:false },
{ fieldName: 'userRequest_RequestDate', displayName: 'Request Date', sortName: 'UserRequest_RequestDate', width: '50px', isVisible:true },
{ fieldName: 'userRequest_ResponseDate', displayName: 'Response Date', sortName: 'UserRequest_ResponseDate', width: '50px', isVisible:false },
{ fieldName: 'userRequest_UserRequestStatusId', displayName: 'User Request Status', sortName: 'UserRequestStatus_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IUserRequest {
    isDeleted?: boolean;
id?: string;
aspNetUserId?: string;
notes?: string;
name?: string;
email?: string;
userId?: string;
requestDate?: Date | null;
responseDate?: Date | null;

    userRequestStatusId?: string;

}
//---------------------------------------------------------
export function initUserRequest(): IUserRequest {
    let entity: IUserRequest = {
        isDeleted: false,
id: '0',
aspNetUserId: '',
notes: '',
name: '',
email: '',
userId: '0',
requestDate: null,
responseDate: null,

        userRequestStatusId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function userRequestUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
aspNetUserId: [''],
notes: [''],
name: [''],
email: [''],
userId: ['0'],
requestDate: [null],
responseDate: [null],

    userRequestStatusId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IUserRequestWithLookup extends IUserRequest{

    userRequestStatusName?: string;

}
//---------------------------------------------------------
export interface IUserRequestRequest extends IEntityRequest<IUserRequestFilter> {}

//---------------------------------------------------------
export interface IUserRequestFilter {
    [key: string]: any;
    Id?: string;
UserId?: string;

    AspNetUserId?: string;
Name?: string;
Email?: string;

    UserRequestStatusId?: string;

    RequestDate?: { from?: Date | null ; to?: Date | null} ;
ResponseDate?: { from?: Date | null ; to?: Date | null} ;

}
//---------------------------------------------------------
export function initUserRequestRequest(): IUserRequestRequest {
    let request: IUserRequestRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: UserRequestColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,
UserId: undefined ,

            AspNetUserId: undefined ,
Name: undefined ,
Email: undefined ,

            UserRequestStatusId: undefined ,

            RequestDate: { from: undefined , to: undefined} ,
ResponseDate: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getUserRequestActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('userRequest', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/ath/user-request/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('userRequest', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user-request/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('userRequest', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user-request/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('userRequest', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/user-request/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('userRequest', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['userRequestStatusId'], route:'/ath/user-request-status/view', displayText:'Go to UserRequestStatus'
});
}

        return links;
    }
    //---------------------------------------------------------

