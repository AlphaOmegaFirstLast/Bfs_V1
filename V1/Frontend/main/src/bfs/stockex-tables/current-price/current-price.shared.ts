
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CurrentPriceColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'stockShareId', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible:true },
{ fieldName: 'transactionDate', displayName: 'Transaction Date', sortName: 'TransactionDate', width: '50px', isVisible:false },
{ fieldName: 'price', displayName: 'Price', sortName: 'Price', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface ICurrentPrice {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
transactionDate?: Date | null;
price?: number;

    stockShareId?: string;

}
//---------------------------------------------------------
export function initCurrentPrice(): ICurrentPrice {
    let entity: ICurrentPrice = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
transactionDate: null,
price: 0,

        stockShareId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function currentPriceUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
transactionDate: [null],
price: [0],

    stockShareId: ['0'],

    };
} 
//---------------------------------------------------------
export interface ICurrentPriceWithLookup extends ICurrentPrice{

    stockShareName?: string;

}
//---------------------------------------------------------
export interface ICurrentPriceRequest extends IEntityRequest<ICurrentPriceFilter> {}

//---------------------------------------------------------
export interface ICurrentPriceFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    StockShareId?: string;

    TransactionDate?: { from?: Date | null ; to?: Date | null} ;
Price?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initCurrentPriceRequest(): ICurrentPriceRequest {
    let request: ICurrentPriceRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CurrentPriceColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            StockShareId: undefined ,

            TransactionDate: { from: undefined , to: undefined} ,
Price: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getCurrentPriceActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/current-price/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/current-price/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/current-price/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/current-price/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('currentPrice', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockShareId'], route:'/stkx/stock-share/view', displayText:'Go to StockShare'
});
}

        return links;
    }
    //---------------------------------------------------------

