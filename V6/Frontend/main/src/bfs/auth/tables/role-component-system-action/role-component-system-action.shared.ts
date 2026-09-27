import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/auth/main/auth.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const RoleComponentSystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponent_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'systemActionId', displayName: 'System Action', sortName: 'SystemAction_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'roleId', displayName: 'Role', sortName: 'Role_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IRoleComponentSystemAction extends IEntity{
    isDeleted?: boolean;
id?: string;

    bfsComponentId?: string;
systemActionId?: string;
roleId?: string;

}
//---------------------------------------------------------
export function initRoleComponentSystemAction(): IRoleComponentSystemAction {
    let entity: IRoleComponentSystemAction = {
        isDeleted: false,
id: '0',

        bfsComponentId: '0',
systemActionId: '0',
roleId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function roleComponentSystemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
systemActionId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
roleId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IRoleComponentSystemActionRequest extends IEntityRequest<IRoleComponentSystemActionFilter> {}

//---------------------------------------------------------
export interface IRoleComponentSystemActionFilter {
    [key: string]: any;
    Id?: string;

    BfsComponentId?: string;
SystemActionId?: string;
RoleId?: string;

}
//---------------------------------------------------------
export function initRoleComponentSystemActionRequest(): IRoleComponentSystemActionRequest {
    let request: IRoleComponentSystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleComponentSystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            BfsComponentId: undefined ,
SystemActionId: undefined ,
RoleId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderRoleComponentSystemAction(record: IRoleComponentSystemAction, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IRoleComponentSystemAction];
        switch (column.fieldName) {
            case 'bfsComponentId':
                return record['bfsComponentName']?.toString();
case 'systemActionId':
                return record['systemActionName']?.toString();
case 'roleId':
                return record['roleName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getRoleComponentSystemActionActions(component: any, record: IRoleComponentSystemAction): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role-component-system-action/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role-component-system-action/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/ath/role-component-system-action/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsComponentId'], route:'/ath/bfs-component/view', displayText:'Go to BfsComponent'
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['systemActionId'], route:'/ath/system-action/view', displayText:'Go to SystemAction'
});
}
if (component.accessService.isActionAllowed('roleComponentSystemAction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['roleId'], route:'/ath/role/view', displayText:'Go to Role'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IRoleComponentSystemAction, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

