import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"
import * as operations from '@bfs/stockex/main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:3 },
{ fieldName: 'currency_Name', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:8 },
{ fieldName: 'ssPortfolio_InvestorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'ssPortfolio_BrokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible:false, columnOrder:2 },

    { fieldName: 'sumValue', displayName: 'Stock Share Value', sortName: 'sumValue', width: '50px', isVisible:true,  columnOrder:5 },
{ fieldName: 'sumBalance', displayName: 'Cash', sortName: 'sumBalance', width: '50px', isVisible:true,  columnOrder:6 },
{ fieldName: 'sumNav', displayName: 'Nav', sortName: 'sumNav', width: '50px', isVisible:true,  columnOrder:7 },

];
export const NavReportCompareColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface INavReportCompare {
    [key: string]: any;
    ssPortfolio_Name?: string;
currency_Name?: string;
ssPortfolio_InvestorId?: string;
ssPortfolio_BrokerId?: string;

}
//---------------------------------------------------------
export interface INavReportCompareFilter {
    [key: string]: any;

    SsPortfolio_InvestorId?: string;
SsPortfolio_BrokerId?: string;
SsPortfolio_SsPortfolioId?: string;

    sumNav?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------

export interface INavReportCompareRequest extends IEntityRequest<INavReportCompareFilter> {}

//---------------------------------------------------------
export function initNavReportCompareRequest(): INavReportCompareRequest {
    let request: INavReportCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: NavReportCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SsPortfolio_InvestorId: undefined ,
SsPortfolio_BrokerId: undefined ,
SsPortfolio_SsPortfolioId: undefined ,

            sumNav: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderNavReportCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'ssPortfolio_InvestorId':
                return record['investorName']?.toString();
case 'ssPortfolio_BrokerId':
                return record['brokerName']?.toString();
case 'ssPortfolio_SsPortfolioId':
                return record['ssPortfolioName']?.toString();

            case 'sumValue':
                return record['sumValue']?.toString();
case 'sumBalance':
                return record['sumBalance']?.toString();
case 'sumNav':
                return record['sumNav']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getNavReportCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

