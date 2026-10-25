import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"
import * as operations from '@bfs/stockex/main/stockex.operations';
import { setArrowData } from '@bfs/_shared/helpers/html.helper';

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'sspStock_SsPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolioName', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'sspStock_Quantity', displayName: 'Quantity', sortName: 'SspStock_Quantity', width: '50px', isVisible: true, columnOrder: 3 },
    { fieldName: 'sspStock_StockShareId', displayName: 'StockShare ', sortName: 'StockShareName', width: '50px', isVisible: true, columnOrder: 2 },
    { fieldName: 'sspStock_AverageCost', displayName: 'Average Cost', sortName: 'SspStock_AverageCost', width: '50px', isVisible: true, columnOrder: 4 },
    { fieldName: 'sspStock_CurrencyId', displayName: 'Currency', sortName: 'CurrencyName', width: '50px', isVisible: true, columnOrder: 7 },
    { fieldName: 'currentPrice_Price', displayName: 'Current Price', sortName: 'CurrentPrice_Price', width: '50px', isVisible: true, columnOrder: 5 },
    { fieldName: 'currentPrice_Profit', displayName: 'Profit', sortName: 'CurrentPrice_Profit', width: '50px', isVisible: true, columnOrder: 6 },

];
export const PortfolioProfitRepCompareColumns = reOrderColumns(columns);
//---------------------------------------------------------
export interface IPortfolioProfitRepCompare {
    [key: string]: any;
    sspStock_SsPortfolioId?: string;
    sspStock_Quantity?: number;
    sspStock_StockShareId?: string;
    sspStock_AverageCost?: number;
    sspStock_CurrencyId?: string;
    currentPrice_Price?: number;
    currentPrice_Profit?: number;

}
//---------------------------------------------------------
export interface IPortfolioProfitRepCompareFilter {
    [key: string]: any;

    SspStock_SsPortfolioId?: string;
    SspStock_StockShareId?: string;
    SspStock_CurrencyId?: string;

    CurrentPrice_Profit?: { from?: number; to?: number };

}
//---------------------------------------------------------

export interface IPortfolioProfitRepCompareRequest extends IEntityRequest<IPortfolioProfitRepCompareFilter> { }

//---------------------------------------------------------
export function initPortfolioProfitRepCompareRequest(): IPortfolioProfitRepCompareRequest {
    let request: IPortfolioProfitRepCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: PortfolioProfitRepCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SspStock_SsPortfolioId: undefined,
            SspStock_StockShareId: undefined,
            SspStock_CurrencyId: undefined,

            CurrentPrice_Profit: { from: undefined, to: undefined },

        }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderPortfolioProfitRepCompare(record: IEntity, column: IQueryColumn, sanitizer: DomSanitizer): SafeHtml {
    const value = record[column.fieldName as keyof IEntity];
    switch (column.fieldName) {
        case 'sspStock_SsPortfolioId':
            return record['ssPortfolioName']?.toString();
        case 'sspStock_StockShareId':
            return record['stockShareName']?.toString();
        case 'sspStock_CurrencyId':
            return record['currencyName']?.toString();
        case 'currentPrice_Profit':
            let rawHtml = "";
            if (record['currentPrice_Profit'] >= 0) {
                rawHtml = setArrowData(sanitizer,'green', 'up' ,record['currentPrice_Profit']);
            }
            else {
                rawHtml = setArrowData(sanitizer,'red' , 'down' , record['currentPrice_Profit']);
            }
            return rawHtml ;
      //      return sanitizer ? sanitizer.bypassSecurityTrustHtml(rawHtml) : record['currentPrice_Profit']?.toString();
        default:
            return value;
    }
    return value;
}
//---------------------------------------------------------
export function getPortfolioProfitRepCompareActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

