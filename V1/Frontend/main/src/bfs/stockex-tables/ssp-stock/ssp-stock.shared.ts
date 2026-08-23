
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SspStockColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'quantity', displayName: 'Quantity', sortName: 'Quantity', width: '50px', isVisible:true },
{ fieldName: 'stockShareId', displayName: 'StockShare ', sortName: 'StockShare_Name', width: '50px', isVisible:true },
{ fieldName: 'averageCost', displayName: 'Average Cost', sortName: 'AverageCost', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ISspStock {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
quantity?: number;
averageCost?: number;

    ssPortfolioId?: string;
stockShareId?: string;

}
//---------------------------------------------------------
export function initSspStock(): ISspStock {
    let entity: ISspStock = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
quantity: 0,
averageCost: 0,

        ssPortfolioId: '0',
stockShareId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function sspStockUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
quantity: [0],
averageCost: [0],

    ssPortfolioId: ['0'],
stockShareId: ['0'],

    };
} 
//---------------------------------------------------------
export interface ISspStockWithLookup extends ISspStock{

    ssPortfolioName?: string;
stockShareName?: string;

}
//---------------------------------------------------------
export interface ISspStockRequest extends IEntityRequest<ISspStockFilter> {}

//---------------------------------------------------------
export interface ISspStockFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;
StockShareId?: string;

    Quantity?: { from?: number ; to?: number} ;
AverageCost?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initSspStockRequest(): ISspStockRequest {
    let request: ISspStockRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SspStockColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,
StockShareId: undefined ,

            Quantity: { from: undefined , to: undefined} ,
AverageCost: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getSspStockActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/ssp-stock/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-stock/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-stock/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-stock/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}
if (component.accessService.isActionAllowed('sspStock', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockShareId'], route:'/stkx/stock-share/view', displayText:'Go to StockShare'
});
}

        return links;
    }
    //---------------------------------------------------------

