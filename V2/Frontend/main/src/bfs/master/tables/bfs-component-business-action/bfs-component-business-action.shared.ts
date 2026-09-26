import { FormBuilder } from "@angular/forms";
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const BfsComponentBusinessActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponent_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'businessActionId', displayName: 'Business Action', sortName: 'BusinessAction_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'actionLocationId', displayName: 'Action Location', sortName: 'ActionLocation_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBfsComponentBusinessAction {
    isDeleted?: boolean;
id?: string;

    bfsComponentId?: string;
businessActionId?: string;
actionLocationId?: number;

}
//---------------------------------------------------------
export function initBfsComponentBusinessAction(): IBfsComponentBusinessAction {
    let entity: IBfsComponentBusinessAction = {
        isDeleted: false,
id: '0',

        bfsComponentId: '0',
businessActionId: '0',
actionLocationId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function bfsComponentBusinessActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsComponentId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
businessActionId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
actionLocationId: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IBfsComponentBusinessActionWithLookup extends IBfsComponentBusinessAction{

    bfsComponentName?: string;
businessActionName?: string;
actionLocationName?: string;

}
//---------------------------------------------------------
export interface IBfsComponentBusinessActionRequest extends IEntityRequest<IBfsComponentBusinessActionFilter> {}

//---------------------------------------------------------
export interface IBfsComponentBusinessActionFilter {
    [key: string]: any;
    Id?: string;

    BfsComponentId?: string;
BusinessActionId?: string;
ActionLocationId?: number;

}
//---------------------------------------------------------
export function initBfsComponentBusinessActionRequest(): IBfsComponentBusinessActionRequest {
    let request: IBfsComponentBusinessActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BfsComponentBusinessActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            BfsComponentId: undefined ,
BusinessActionId: undefined ,
ActionLocationId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBfsComponentBusinessAction(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'bfsComponentId':
                return record['bfsComponentName']?.toString();
case 'businessActionId':
                return record['businessActionName']?.toString();
case 'actionLocationId':
                return record['actionLocationName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getBfsComponentBusinessActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

