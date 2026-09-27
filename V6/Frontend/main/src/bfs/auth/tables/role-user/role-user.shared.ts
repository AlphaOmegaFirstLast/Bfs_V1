import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const RoleUserColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'roleId', displayName: 'Role', sortName: 'Role_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'userId', displayName: 'User', sortName: 'User_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IRoleUser extends IEntity{
    isDeleted?: boolean;
id?: string;

    roleId?: string;
userId?: string;

}
//---------------------------------------------------------
export function initRoleUser(): IRoleUser {
    let entity: IRoleUser = {
        isDeleted: false,
id: '0',

        roleId: '0',
userId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleUserUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    roleId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
userId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IRoleUserRequest extends IEntityRequest<IRoleUserFilter> {}

//---------------------------------------------------------
export interface IRoleUserFilter {
    [key: string]: any;
    Id?: string;

    RoleId?: string;
UserId?: string;

}
//---------------------------------------------------------
export function initRoleUserRequest(): IRoleUserRequest {
    let request: IRoleUserRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleUserColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            RoleId: undefined ,
UserId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderRoleUser(record: IRoleUser, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IRoleUser];
        switch (column.fieldName) {
            case 'roleId':
                return record['roleName']?.toString();
case 'userId':
                return record['userName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getRoleUserActions(component: any, record: IRoleUser): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IRoleUser, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

