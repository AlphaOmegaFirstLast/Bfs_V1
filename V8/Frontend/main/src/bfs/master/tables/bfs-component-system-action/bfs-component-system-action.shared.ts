import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const BfsComponentSystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponent_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'systemActionId', displayName: 'System Action', sortName: 'SystemAction_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'actionLocationId', displayName: 'Action Location', sortName: 'ActionLocation_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBfsComponentSystemAction extends IEntity{
    isDeleted?: boolean;
id?: string;

    bfsComponentId?: string;
systemActionId?: string;
actionLocationId?: number;

}
//---------------------------------------------------------
export function initBfsComponentSystemAction(): IBfsComponentSystemAction {
    let entity: IBfsComponentSystemAction = {
        isDeleted: false,
id: '0',

        bfsComponentId: '0',
systemActionId: '0',
actionLocationId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsComponentSystemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
systemActionId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
actionLocationId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IBfsComponentSystemActionRequest extends IEntityRequest<IBfsComponentSystemActionFilter> {}

//---------------------------------------------------------
export interface IBfsComponentSystemActionFilter {
    [key: string]: any;
    Id?: string;

    BfsComponentId?: string;
SystemActionId?: string;
ActionLocationId?: number;

}
//---------------------------------------------------------
export function initBfsComponentSystemActionRequest(): IBfsComponentSystemActionRequest {
    let request: IBfsComponentSystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsComponentSystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            BfsComponentId: undefined ,
SystemActionId: undefined ,
ActionLocationId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBfsComponentSystemAction(record: IBfsComponentSystemAction, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IBfsComponentSystemAction];
        switch (column.fieldName) {
            case 'bfsComponentId':
                return record['bfsComponentName']?.toString();
case 'systemActionId':
                return record['systemActionName']?.toString();
case 'actionLocationId':
                return record['actionLocationName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getBfsComponentSystemActionActions(component: any, record: IBfsComponentSystemAction): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IBfsComponentSystemAction, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

