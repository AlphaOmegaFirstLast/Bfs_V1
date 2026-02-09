import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemActionColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },

    { fieldName: 'actionTypeId', displayName: 'Action Type', sortName:'ActionType', width: '50px', isVisible:false },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function systemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    actionTypeId: [0],

    };
} 
//---------------------------------------------------------

export interface ISystemAction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    actionTypeId?: number;

}
//---------------------------------------------------------
export interface ISystemActionWithLookup extends ISystemAction{

    actionType?: string;

}
//---------------------------------------------------------

export function initSystemAction(): ISystemAction {
    let entity: ISystemAction = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        actionTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface ISystemActionRequest extends IEntityRequest<ISystemActionFilter> {}

//---------------------------------------------------------
export interface ISystemActionFilter {
    [key: string]: any;

    systemActionName?: string;

    systemActionActionTypeId?: number;

}
//---------------------------------------------------------
export function initSystemActionRequest(): ISystemActionRequest {
    let request: ISystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            systemActionName: undefined ,

            systemActionActionTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

