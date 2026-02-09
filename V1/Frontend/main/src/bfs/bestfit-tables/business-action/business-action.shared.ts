import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BusinessActionColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },

    { fieldName: 'actionTypeId', displayName: 'Action Type', sortName:'ActionType', width: '50px', isVisible:false },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function businessActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    actionTypeId: [0],

    };
} 
//---------------------------------------------------------

export interface IBusinessAction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    actionTypeId?: number;

}
//---------------------------------------------------------
export interface IBusinessActionWithLookup extends IBusinessAction{

    actionType?: string;

}
//---------------------------------------------------------

export function initBusinessAction(): IBusinessAction {
    let entity: IBusinessAction = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        actionTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IBusinessActionRequest extends IEntityRequest<IBusinessActionFilter> {}

//---------------------------------------------------------
export interface IBusinessActionFilter {
    [key: string]: any;

    Name?: string;

    ActionTypeId?: number;

}
//---------------------------------------------------------
export function initBusinessActionRequest(): IBusinessActionRequest {
    let request: IBusinessActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BusinessActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            ActionTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

