
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StockShareColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'tradingRoomId', displayName: 'Trading Room', sortName: 'TradingRoom_Name', width: '50px', isVisible:true },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IStockShare {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    tradingRoomId?: string;
currencyId?: string;

}
//---------------------------------------------------------
export function initStockShare(): IStockShare {
    let entity: IStockShare = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        tradingRoomId: '0',
currencyId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function stockShareUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    tradingRoomId: ['0'],
currencyId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IStockShareWithLookup extends IStockShare{

    tradingRoomName?: string;
currencyName?: string;

}
//---------------------------------------------------------
export interface IStockShareRequest extends IEntityRequest<IStockShareFilter> {}

//---------------------------------------------------------
export interface IStockShareFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    TradingRoomId?: string;
CurrencyId?: string;

}
//---------------------------------------------------------
export function initStockShareRequest(): IStockShareRequest {
    let request: IStockShareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StockShareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            TradingRoomId: undefined ,
CurrencyId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getStockShareActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/stock-share/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-share/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-share/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/stock-share/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['tradingRoomId'], route:'/stkx/trading-room/view', displayText:'Go to TradingRoom'
});
}
if (component.accessService.isActionAllowed('stockShare', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['currencyId'], route:'/stkx/currency/view', displayText:'Go to Currency'
});
}

        return links;
    }
    //---------------------------------------------------------

