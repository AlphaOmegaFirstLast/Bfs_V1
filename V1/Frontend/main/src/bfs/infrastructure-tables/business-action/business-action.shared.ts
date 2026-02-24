
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BusinessActionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'actionTypeId', displayName: 'Action Type', sortName: 'ActionType', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBusinessAction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    actionTypeId?: number;

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
export interface IBusinessActionWithLookup extends IBusinessAction{

    actionTypeName?: string;

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

export function getBusinessActionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

