
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StockEntityTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IStockEntityType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initStockEntityType(): IStockEntityType {
    let entity: IStockEntityType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function stockEntityTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IStockEntityTypeWithLookup extends IStockEntityType{

}
//---------------------------------------------------------
export interface IStockEntityTypeRequest extends IEntityRequest<IStockEntityTypeFilter> {}

//---------------------------------------------------------
export interface IStockEntityTypeFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initStockEntityTypeRequest(): IStockEntityTypeRequest {
    let request: IStockEntityTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StockEntityTypeColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getStockEntityTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('stockEntityType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/stock-entity-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('stockEntityType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-entity-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('stockEntityType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-entity-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('stockEntityType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-entity-type/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

