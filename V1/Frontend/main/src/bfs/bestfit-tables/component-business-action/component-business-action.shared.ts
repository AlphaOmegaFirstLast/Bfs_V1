import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ComponentBusinessActionColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },

    { fieldName: 'componentId', displayName: 'Component Name', sortName:'Component', width: '50px', isVisible:true },
{ fieldName: 'businessActionId', displayName: 'Business Action', sortName:'BusinessAction', width: '50px', isVisible:true },
{ fieldName: 'actionLocationId', displayName: 'Menu Action', sortName:'ActionLocation', width: '50px', isVisible:true },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function componentBusinessActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],

    componentId: ['0'],
businessActionId: ['0'],
actionLocationId: [0],

    };
} 
//---------------------------------------------------------

export interface IComponentBusinessAction {
    isDeleted?: boolean;
id?: string;

    componentId?: string;
businessActionId?: string;
actionLocationId?: number;

}
//---------------------------------------------------------
export interface IComponentBusinessActionWithLookup extends IComponentBusinessAction{

    component?: string;
businessAction?: string;
actionLocation?: string;

}
//---------------------------------------------------------

export function initComponentBusinessAction(): IComponentBusinessAction {
    let entity: IComponentBusinessAction = {
        isDeleted: false,
id: '0',

        componentId: '0',
businessActionId: '0',
actionLocationId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IComponentBusinessActionRequest extends IEntityRequest<IComponentBusinessActionFilter> {}

//---------------------------------------------------------
export interface IComponentBusinessActionFilter {
    [key: string]: any;

    ComponentId?: string;
BusinessActionId?: string;
ActionLocationId?: number;

}
//---------------------------------------------------------
export function initComponentBusinessActionRequest(): IComponentBusinessActionRequest {
    let request: IComponentBusinessActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ComponentBusinessActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            ComponentId: undefined ,
BusinessActionId: undefined ,
ActionLocationId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

