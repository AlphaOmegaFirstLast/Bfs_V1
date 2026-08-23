
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SspTransactionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'sourceDate', displayName: 'Source Date', sortName: 'SourceDate', width: '50px', isVisible:false },
{ fieldName: 'transactionDate', displayName: 'Transaction Date', sortName: 'TransactionDate', width: '50px', isVisible:false },
{ fieldName: 'source', displayName: 'Source', sortName: 'Source', width: '50px', isVisible:false },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'transactionTypeId', displayName: 'Transaction Type', sortName: 'TransactionType_Name', width: '50px', isVisible:true },
{ fieldName: 'quantity', displayName: 'Quantity', sortName: 'Quantity', width: '50px', isVisible:true },
{ fieldName: 'price', displayName: 'Price', sortName: 'Price', width: '50px', isVisible:true },
{ fieldName: 'stockShareId', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible:true },
{ fieldName: 'toQuantity', displayName: 'To Quantity', sortName: 'ToQuantity', width: '50px', isVisible:true },
{ fieldName: 'toPortfolioId', displayName: 'To Portfolio', sortName: 'ToPortfolio_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ISspTransaction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
sourceDate?: Date | null;
transactionDate?: Date | null;
source?: string;
quantity?: number;
price?: number;
toQuantity?: number;

    ssPortfolioId?: string;
transactionTypeId?: number;
stockShareId?: string;
toPortfolioId?: string;

}
//---------------------------------------------------------
export function initSspTransaction(): ISspTransaction {
    let entity: ISspTransaction = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
sourceDate: null,
transactionDate: null,
source: '',
quantity: 0,
price: 0,
toQuantity: 0,

        ssPortfolioId: '0',
transactionTypeId: 0,
stockShareId: '0',
toPortfolioId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function sspTransactionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
sourceDate: [null],
transactionDate: [null],
source: [''],
quantity: [0],
price: [0],
toQuantity: [0],

    ssPortfolioId: ['0'],
transactionTypeId: [0],
stockShareId: ['0'],
toPortfolioId: ['0'],

    };
} 
//---------------------------------------------------------
export interface ISspTransactionWithLookup extends ISspTransaction{

    ssPortfolioName?: string;
transactionTypeName?: string;
stockShareName?: string;
toPortfolioName?: string;

}
//---------------------------------------------------------
export interface ISspTransactionRequest extends IEntityRequest<ISspTransactionFilter> {}

//---------------------------------------------------------
export interface ISspTransactionFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;
TransactionTypeId?: number;
StockShareId?: string;
ToPortfolioId?: string;

    SourceDate?: { from?: Date | null ; to?: Date | null} ;
TransactionDate?: { from?: Date | null ; to?: Date | null} ;
Quantity?: { from?: number ; to?: number} ;
Price?: { from?: number ; to?: number} ;
ToQuantity?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initSspTransactionRequest(): ISspTransactionRequest {
    let request: ISspTransactionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SspTransactionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,
TransactionTypeId: undefined ,
StockShareId: undefined ,
ToPortfolioId: undefined ,

            SourceDate: { from: undefined , to: undefined} ,
TransactionDate: { from: undefined , to: undefined} ,
Quantity: { from: undefined , to: undefined} ,
Price: { from: undefined , to: undefined} ,
ToQuantity: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getSspTransactionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/ssp-transaction/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-transaction/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-transaction/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ssp-transaction/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['transactionTypeId'], route:'/stkx/transaction-type/view', displayText:'Go to TransactionType'
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockShareId'], route:'/stkx/stock-share/view', displayText:'Go to StockShare'
});
}
if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['toPortfolioId'], route:'/stkx/to-portfolio/view', displayText:'Go to ToPortfolio'
});
}

if (component.accessService.isActionAllowed('sspTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.sspTransactionRollout, displayText: 'Save and Rollout Transaction', data: { recordId: record['id'],  postUrl: '/Operations/SspTransaction/Rollout'}
});
}

        return links;
    }
    //---------------------------------------------------------

