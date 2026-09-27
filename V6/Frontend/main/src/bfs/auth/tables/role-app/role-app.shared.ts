import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const RoleAppColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'roleId', displayName: 'Role', sortName: 'Role_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'appId', displayName: 'System Application', sortName: 'App_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IRoleApp extends IEntity{
    isDeleted?: boolean;
id?: string;

    roleId?: string;
appId?: string;

}
//---------------------------------------------------------
export function initRoleApp(): IRoleApp {
    let entity: IRoleApp = {
        isDeleted: false,
id: '0',

        roleId: '0',
appId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleAppUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    roleId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
appId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IRoleAppRequest extends IEntityRequest<IRoleAppFilter> {}

//---------------------------------------------------------
export interface IRoleAppFilter {
    [key: string]: any;
    Id?: string;

    RoleId?: string;
AppId?: string;

}
//---------------------------------------------------------
export function initRoleAppRequest(): IRoleAppRequest {
    let request: IRoleAppRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleAppColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            RoleId: undefined ,
AppId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderRoleApp(record: IRoleApp, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IRoleApp];
        switch (column.fieldName) {
            case 'roleId':
                return record['roleName']?.toString();
case 'appId':
                return record['appName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getRoleAppActions(component: any, record: IRoleApp): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IRoleApp, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

