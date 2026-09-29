import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const ResourceRuleColumns = [
    { fieldName: 'selectBlackList', displayName: 'Select Statement  BlackList fields', sortName: 'SelectBlackList', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'roleId', displayName: 'Role', sortName: 'Role_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'bfsComponentName', displayName: 'BfsComponent Name', sortName: 'BfsComponentName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'joinStatement', displayName: 'Join Statement', sortName: 'JoinStatement', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'whereStatement', displayName: 'Where Statement', sortName: 'WhereStatement', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'parameterName', displayName: 'Parameter Name', sortName: 'ParameterName', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'parameterValue', displayName: 'Parameter Value', sortName: 'ParameterValue', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'parameterType', displayName: 'Parameter Type', sortName: 'ParameterType', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsComponentId', displayName: 'BfsComponent', sortName: 'BfsComponent_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'roleName', displayName: 'Role Name', sortName: 'RoleName', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IResourceRule extends IEntity{
    selectBlackList?: string;
isDeleted?: boolean;
id?: string;
bfsComponentName?: string;
joinStatement?: string;
whereStatement?: string;
parameterName?: string;
parameterValue?: string;
parameterType?: string;
roleName?: string;

    roleId?: string;
bfsComponentId?: string;

}
//---------------------------------------------------------
export function initResourceRule(): IResourceRule {
    let entity: IResourceRule = {
        selectBlackList: '',
isDeleted: false,
id: '0',
bfsComponentName: '',
joinStatement: '',
whereStatement: '',
parameterName: '',
parameterValue: '',
parameterType: '',
roleName: '',

        roleId: '0',
bfsComponentId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function resourceRuleUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    selectBlackList: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
bfsComponentName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
joinStatement: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
whereStatement: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
parameterName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
parameterValue: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
parameterType: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
roleName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    roleId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IResourceRuleRequest extends IEntityRequest<IResourceRuleFilter> {}

//---------------------------------------------------------
export interface IResourceRuleFilter {
    [key: string]: any;
    Id?: string;

    SelectBlackList?: string;
BfsComponentName?: string;
JoinStatement?: string;
WhereStatement?: string;
ParameterName?: string;
ParameterValue?: string;
ParameterType?: string;
RoleName?: string;

    RoleId?: string;
BfsComponentId?: string;

}
//---------------------------------------------------------
export function initResourceRuleRequest(): IResourceRuleRequest {
    let request: IResourceRuleRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ResourceRuleColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            SelectBlackList: undefined ,
BfsComponentName: undefined ,
JoinStatement: undefined ,
WhereStatement: undefined ,
ParameterName: undefined ,
ParameterValue: undefined ,
ParameterType: undefined ,
RoleName: undefined ,

            RoleId: undefined ,
BfsComponentId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderResourceRule(record: IResourceRule, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IResourceRule];
        switch (column.fieldName) {
            case 'roleId':
                return record['roleName']?.toString();
case 'bfsComponentId':
                return record['bfsComponentName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getResourceRuleActions(component: any, record: IResourceRule): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IResourceRule, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

