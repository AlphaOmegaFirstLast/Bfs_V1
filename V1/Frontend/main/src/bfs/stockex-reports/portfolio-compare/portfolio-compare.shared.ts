import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const PortfolioCompareColumns:IQueryColumn[] = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Name', sortName: 'SsPortfolio_Name', width: '50px', isVisible: true },
    { fieldName: 'broker_Name', displayName: 'Broker Name', sortName: 'Broker_Name', width: '50px', isVisible: true },
    { fieldName: 'investor_Name', displayName: 'Investor Name', sortName: 'Investor_Name', width: '50px', isVisible: true },
    { fieldName: 'sspTransaction_Quantity', displayName: 'Quantity', sortName: 'SspTransaction_Quantity', width: '50px', isVisible: true },
    { fieldName: 'sspTransaction_Price', displayName: 'Price', sortName: 'SspTransaction_Price', width: '50px', isVisible: true },
    { fieldName: 'sspTransaction_TransactionDate', displayName: 'Transaction Date', sortName: 'SspTransaction_TransactionDate', width: '50px', isVisible: true },
    { fieldName: 'stockShare_Name', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible: true },
    { fieldName: 'transactionType_Name', displayName: 'Transaction Type', sortName: 'TransactionType_Name', width: '50px', isVisible: true },

];

//---------------------------------------------------------

export interface IPortfolioCompare {
    ssPortfolio_Name?: string;
    broker_Name?: string;
    investor_Name?: string;
    sspTransaction_Quantity?: number;
    sspTransaction_Price?: number;
    sspTransaction_TransactionDate?: Date | null;
    stockShare_Name?: string;
    transactionType_Name?: string;

}
//---------------------------------------------------------
export interface IPortfolioCompareWithLookup extends IPortfolioCompare {

}
//---------------------------------------------------------
export interface IPortfolioCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;
    StockShare_Name?: string;

    SspTransaction_Quantity?: { from?: number; to?: number };
    SspTransaction_Price?: { from?: number; to?: number };
    SspTransaction_TransactionDate?: { from?: Date | null; to?: Date | null };

}
//---------------------------------------------------------

export interface IPortfolioCompareRequest extends IEntityRequest<IPortfolioCompareFilter> { }

//---------------------------------------------------------
export function initPortfolioCompareRequest(): IPortfolioCompareRequest {
    let request: IPortfolioCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: PortfolioCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SsPortfolio_Name: undefined,
            StockShare_Name: undefined,

            SspTransaction_Quantity: { from: undefined, to: undefined },
            SspTransaction_Price: { from: undefined, to: undefined },
            SspTransaction_TransactionDate: { from: undefined, to: undefined },

        }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getPortfolioCompareActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

