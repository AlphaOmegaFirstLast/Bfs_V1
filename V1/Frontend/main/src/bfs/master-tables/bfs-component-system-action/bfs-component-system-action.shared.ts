
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BfsComponentSystemActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'bfsComponentId', displayName: 'Component Name', sortName: 'BfsComponentName', width: '50px', isVisible:true },
{ fieldName: 'systemActionId', displayName: 'System Action', sortName: 'SystemActionName', width: '50px', isVisible:true },
{ fieldName: 'actionLocationId', displayName: 'Action Location', sortName: 'ActionLocationName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBfsComponentSystemAction {
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
    isDeleted: [false],
id: ['0'],

    bfsComponentId: ['0'],
systemActionId: ['0'],
actionLocationId: [0],

    };
} 
//---------------------------------------------------------
export interface IBfsComponentSystemActionWithLookup extends IBfsComponentSystemAction{

    bfsComponentName?: string;
systemActionName?: string;
actionLocationName?: string;

}
//---------------------------------------------------------
export interface IBfsComponentSystemActionRequest extends IEntityRequest<IBfsComponentSystemActionFilter> {}

//---------------------------------------------------------
export interface IBfsComponentSystemActionFilter {
    [key: string]: any;

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

            BfsComponentId: undefined ,
SystemActionId: undefined ,
ActionLocationId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBfsComponentSystemActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

