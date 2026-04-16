
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsComponentBusinessActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponentName', width: '50px', isVisible:true },
{ fieldName: 'businessActionId', displayName: 'Business Action', sortName: 'BusinessActionName', width: '50px', isVisible:true },
{ fieldName: 'actionLocationId', displayName: 'Action Location', sortName: 'ActionLocationName', width: '50px', isVisible:true },

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
    isDeleted: [false],
id: ['0'],

    bfsComponentId: ['0'],
businessActionId: ['0'],
actionLocationId: [0],

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

            BfsComponentId: undefined ,
BusinessActionId: undefined ,
ActionLocationId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsComponentBusinessActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

