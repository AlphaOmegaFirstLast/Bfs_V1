
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";
import { TokenService } from "@bfs/_shared/security/token.service";

// Output Columns of a Query  [used in entity Query]
export const StrProductColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible: true },
    { fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible: true },
    { fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible: false },

];
//---------------------------------------------------------
export interface IStrProduct {
    isDeleted?: boolean;
    id?: string;
    name?: string;
    notes?: string;

}
//---------------------------------------------------------
export function initStrProduct(): IStrProduct {
    let entity: IStrProduct = {
        isDeleted: false,
        id: '0',
        name: '',
        notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function strProductUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
        isDeleted: [false],
        id: ['0'],
        name: [''],
        notes: [''],

    };
}
//---------------------------------------------------------
export interface IStrProductWithLookup extends IStrProduct {

}
//---------------------------------------------------------
export interface IStrProductRequest extends IEntityRequest<IStrProductFilter> { }

//---------------------------------------------------------
export interface IStrProductFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initStrProductRequest(): IStrProductRequest {
    let request: IStrProductRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StrProductColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
        },
        filter: {

            Name: undefined,

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getStrProductActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];
    if (component.accessService.isActionAllowed('StrProduct', 'Add')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListHeader', recordId: 0, route: '/str/str-product/add', displayText: 'Add New record'
        });
    }
    if (component.accessService.isActionAllowed('StrProduct', 'View')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/str/str-product/view', displayText: 'View...'
        });
    }
    if (component.accessService.isActionAllowed('StrProduct', 'Edit')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/str/str-product/edit', displayText: 'Edit...'
        });
    }
    if (component.accessService.isActionAllowed('StrProduct', 'Delete')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/str/str-product/delete', displayText: 'Delete...'
        });
    }
    return links;
}
//---------------------------------------------------------

