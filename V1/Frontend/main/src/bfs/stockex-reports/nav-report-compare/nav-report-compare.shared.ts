import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const NavReportCompareColumns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible: true },
    { fieldName: 'currency_Name', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible: true },

    { fieldName: 'stockValue', displayName: 'Stock Share Value', sortName: 'stockValue', width: '50px', isVisible: true },
    { fieldName: 'cash', displayName: 'Cash', sortName: 'cash', width: '50px', isVisible: true },
    { fieldName: 'nav', displayName: 'NAV', sortName: 'nav', width: '50px', isVisible: true },
];

//---------------------------------------------------------

export interface INavReportCompare {
    ssPortfolio_Name?: string;
    currency_Name?: string;

}
//---------------------------------------------------------
export interface INavReportCompareWithLookup extends INavReportCompare {

    stockValue?: number;
    cash?: number;
    nav?: number;

}
//---------------------------------------------------------
export interface INavReportCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;

    stockValue?: { from?: number; to?: number };
    cash?: { from?: string; to?: string };

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

            stockValue: { from: undefined, to: undefined },
            cash: { from: undefined, to: undefined },

        }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getNavReportCompareActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

