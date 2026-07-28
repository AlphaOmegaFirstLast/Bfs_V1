
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth-main/auth.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ResourceRuleColumns = [
    { fieldName: 'resourceRule_SelectBlackList', displayName: 'Select Statement  BlackList fields', sortName: 'ResourceRule_SelectBlackList', width: '50px', isVisible: false },
    { fieldName: 'resourceRule_Id', displayName: 'ID', sortName: 'ResourceRule_Id', width: '50px', isVisible: false },
    { fieldName: 'resourceRule_RoleId', displayName: 'Role', sortName: 'Role_Name', width: '50px', isVisible: true },
    { fieldName: 'resourceRule_BfsComponentName', displayName: 'BfsComponent Name', sortName: 'ResourceRule_BfsComponentName', width: '50px', isVisible: true },
    { fieldName: 'resourceRule_JoinStatement', displayName: 'Join Statement', sortName: 'ResourceRule_JoinStatement', width: '50px', isVisible: false },
    { fieldName: 'resourceRule_WhereStatement', displayName: 'Where Statement', sortName: 'ResourceRule_WhereStatement', width: '50px', isVisible: false },
    { fieldName: 'resourceRule_ParameterName', displayName: 'Parameter Name', sortName: 'ResourceRule_ParameterName', width: '50px', isVisible: false },
    { fieldName: 'resourceRule_ParameterValue', displayName: 'Parameter Value', sortName: 'ResourceRule_ParameterValue', width: '50px', isVisible: true },
    { fieldName: 'resourceRule_ParameterType', displayName: 'Parameter Type', sortName: 'ResourceRule_ParameterType', width: '50px', isVisible: false },
    { fieldName: 'resourceRule_BfsComponentId', displayName: 'BfsComponent', sortName: 'BfsComponent_Name', width: '50px', isVisible: true },
    { fieldName: 'resourceRule_RoleName', displayName: 'Role Name', sortName: 'ResourceRule_RoleName', width: '50px', isVisible: true },

];
//---------------------------------------------------------
export interface IResourceRule {
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
        selectBlackList: [''],
        isDeleted: [false],
        id: ['0'],
        bfsComponentName: [''],
        joinStatement: [''],
        whereStatement: [''],
        parameterName: [''],
        parameterValue: [''],
        parameterType: [''],
        roleName: [''],

        roleId: ['0'],
        bfsComponentId: ['0'],
    };
}
//---------------------------------------------------------
export interface IResourceRuleWithLookup extends IResourceRule {

    roleName?: string;
    bfsComponentName?: string;

}
//---------------------------------------------------------
export interface IResourceRuleRequest extends IEntityRequest<IResourceRuleFilter> { }

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
            Id: undefined,

            SelectBlackList: undefined,
            BfsComponentName: undefined,
            JoinStatement: undefined,
            WhereStatement: undefined,
            ParameterName: undefined,
            ParameterValue: undefined,
            ParameterType: undefined,
            RoleName: undefined,

            RoleId: undefined,
            BfsComponentId: undefined,

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getResourceRuleActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

