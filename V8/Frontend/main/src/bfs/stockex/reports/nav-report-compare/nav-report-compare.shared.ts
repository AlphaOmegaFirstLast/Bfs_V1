import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex/main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const NavReportCompareColumns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible: true },
    { fieldName: 'currency_Name', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible: true },

    { fieldName: 'StockValue', displayName: 'Stock Value', sortName: 'sumPrice', width: '50px', isVisible: true },
    { fieldName: 'Cash', displayName: 'Cash', sortName: 'Cash', width: '50px', isVisible: true },
    { fieldName: 'Nav', displayName: 'Nav', sortName: 'Nav', width: '50px', isVisible: true },

];

//---------------------------------------------------------

export interface INavReportCompare {
    [key: string]: any;
    ssPortfolio_Name?: string;
    currency_Name?: string;

}
//---------------------------------------------------------
export interface INavReportCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;
    Currency_Name?: string;
    StockValue?: { from?: number; to?: number };
    Cash?: { from?: number; to?: number };
    Nav?: { from?: number; to?: number };

}
//---------------------------------------------------------

export interface INavReportCompareRequest extends IEntityRequest<INavReportCompareFilter> { }

//---------------------------------------------------------
export function initNavReportCompareRequest(): INavReportCompareRequest {
    let request: INavReportCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: NavReportCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SsPortfolio_Name: undefined,

            StockValue: { from: undefined, to: undefined },
            Cash: { from: undefined, to: undefined },
            Nav: { from: undefined, to: undefined },

        }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderNavReportCompare(record: IEntity, column: IQueryColumn): any {
    const value = record[column.fieldName as keyof IEntity];
    switch (column.fieldName.toLowerCase()) {

        case 'stockvalue':
            return record['stockValue']?.toString();
        case 'cash':
            return record['cash']?.toString();
        case 'nav':
            return record['nav']?.toString();

        default:
            return value;
    }
    return value;
}
//---------------------------------------------------------
export function getNavReportCompareActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

