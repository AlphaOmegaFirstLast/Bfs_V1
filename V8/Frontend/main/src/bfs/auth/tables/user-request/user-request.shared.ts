import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const UserRequestColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'email', displayName: 'Email', sortName: 'Email', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'userId', displayName: 'User ID', sortName: 'UserId', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'requestDate', displayName: 'Request Date', sortName: 'RequestDate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'responseDate', displayName: 'Response Date', sortName: 'ResponseDate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'userRequestStatusId', displayName: 'User Request Status', sortName: 'UserRequestStatus_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IUserRequest extends IEntity{
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
requestDate: new Date(0),
responseDate: new Date(0),

        userRequestStatusId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function userRequestUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
aspNetUserId: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
email: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
userId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
requestDate: [new Date(0),getFormControlValidation('{"IsRequired":true,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
responseDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    userRequestStatusId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
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
export function renderUserRequest(record: IUserRequest, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IUserRequest];
        switch (column.fieldName) {
            case 'userRequestStatusId':
                return record['userRequestStatusName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getUserRequestActions(component: any, record: IUserRequest): IAction[] {
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
export function isVisible(entity: IUserRequest, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//---------------------------------------------------------
export function isEnabled(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

