import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ComponentSystemActionColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName: 'IsDeleted', width: '50px', isVisible: false },
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible: false },

    { fieldName: 'componentId', displayName: 'Component Name', sortName: 'Component', width: '50px', isVisible: true },
    { fieldName: 'systemActionId', displayName: 'Menu Action', sortName: 'SystemAction', width: '50px', isVisible: true },
    { fieldName: 'actionLocationId', displayName: 'Menu Action', sortName: 'ActionLocation', width: '50px', isVisible: true },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function componentSystemActionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
        isDeleted: [false],
        id: ['0'],

        componentId: ['0'],
        systemActionId: [0],
        actionLocationId: [0],

    };
}
//---------------------------------------------------------

export interface IComponentSystemAction {
    isDeleted?: boolean;
    id?: string;

    componentId?: string;
    systemActionId?: number;
    actionLocationId?: number;

}
//---------------------------------------------------------
export interface IComponentSystemActionWithLookup extends IComponentSystemAction {

    component?: string;
    systemAction?: string;
    actionLocation?: string;

}
//---------------------------------------------------------

export function initComponentSystemAction(): IComponentSystemAction {
    let entity: IComponentSystemAction = {
        isDeleted: false,
        id: '0',

        componentId: '0',
        systemActionId: 0,
        actionLocationId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IComponentSystemActionRequest extends IEntityRequest<IComponentSystemActionFilter> { }

//---------------------------------------------------------
export interface IComponentSystemActionFilter {
    [key: string]: any;

    ComponentId?: string;
    SystemActionId?: number;
    ActionLocationId?: number;

}
//---------------------------------------------------------
export function initComponentSystemActionRequest(): IComponentSystemActionRequest {
    let request: IComponentSystemActionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ComponentSystemActionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
        },
        filter: {

            ComponentId: undefined,
            SystemActionId: undefined,
            ActionLocationId: undefined,

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

