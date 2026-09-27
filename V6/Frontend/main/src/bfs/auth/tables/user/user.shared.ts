import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const UserColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'aspNetUserId', displayName: 'AspNetUserId', sortName: 'AspNetUserId', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'email', displayName: 'Email', sortName: 'Email', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IUser extends IEntity{
    isDeleted?: boolean;
id?: string;
aspNetUserId?: string;
notes?: string;
name?: string;
email?: string;

}
//---------------------------------------------------------
export function initUser(): IUser {
    let entity: IUser = {
        isDeleted: false,
id: '0',
aspNetUserId: '',
notes: '',
name: '',
email: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function userUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
aspNetUserId: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
email: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IUserRequest extends IEntityRequest<IUserFilter> {}

//---------------------------------------------------------
export interface IUserFilter {
    [key: string]: any;
    Id?: string;

    AspNetUserId?: string;
Name?: string;
Email?: string;

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
            Id: undefined ,

            AspNetUserId: undefined ,
Name: undefined ,
Email: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderUser(record: IUser, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IUser];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getUserActions(component: any, record: IUser): IAction[] {
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
export function isVisible(entity: IUser, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

