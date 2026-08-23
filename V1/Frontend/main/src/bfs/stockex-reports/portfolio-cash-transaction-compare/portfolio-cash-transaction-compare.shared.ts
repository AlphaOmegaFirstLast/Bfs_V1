import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const PortfolioCashTransactionCompareColumns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Name', sortName: 'SsPortfolio_Name', width: '50px', isVisible: true },
    { fieldName: 'transactionType_Name', displayName: 'Transaction Type', sortName: 'TransactionType_Name', width: '50px', isVisible: true },

    { fieldName: 'broker_Name', displayName: 'Broker Name', sortName: 'Broker_Name', width: '50px', isVisible: true },
    { fieldName: 'investor_Name', displayName: 'Investor Name', sortName: 'Investor_Name', width: '50px', isVisible: true },
    { fieldName: 'cashTransaction_Value', displayName: 'Value', sortName: 'CashTransaction_Value', width: '50px', isVisible: true },
    { fieldName: 'cashTransaction_TransactionDate', displayName: 'Transaction Date', sortName: 'CashTransaction_TransactionDate', width: '50px', isVisible: true },
];

//---------------------------------------------------------

export interface IPortfolioCashTransactionCompare {
    ssPortfolio_Name?: string;
    broker_Name?: string;
    investor_Name?: string;
    cashTransaction_Value?: number;
    cashTransaction_TransactionDate?: Date | null;
    transactionType_Name?: string;

}
//---------------------------------------------------------
export interface IPortfolioCashTransactionCompareWithLookup extends IPortfolioCashTransactionCompare {

}
//---------------------------------------------------------
export interface IPortfolioCashTransactionCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;

    CashTransaction_Value?: { from?: number; to?: number };
    CashTransaction_TransactionDate?: { from?: Date | null; to?: Date | null };

}
//---------------------------------------------------------

export interface IPortfolioCashTransactionCompareRequest extends IEntityRequest<IPortfolioCashTransactionCompareFilter> { }

//---------------------------------------------------------
export function initPortfolioCashTransactionCompareRequest(): IPortfolioCashTransactionCompareRequest {
    let request: IPortfolioCashTransactionCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: PortfolioCashTransactionCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SsPortfolio_Name: undefined,

            CashTransaction_Value: { from: undefined, to: undefined },
            CashTransaction_TransactionDate: { from: undefined, to: undefined },

        }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getPortfolioCashTransactionCompareActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

