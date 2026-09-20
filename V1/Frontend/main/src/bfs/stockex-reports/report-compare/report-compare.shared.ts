import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const ReportCompareColumns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'currency_Name', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true },

    { fieldName: 'sumQuantity', displayName: 'Quantity', sortName: 'sumQuantity', width: '50px', isVisible:true },
{ fieldName: 'sumPrice', displayName: 'Price', sortName: 'sumPrice', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IReportCompare {
    ssPortfolio_Name?: string;
currency_Name?: string;

}
//---------------------------------------------------------
export interface IReportCompareWithLookup extends IReportCompare{

    sumQuantity?:number;
sumPrice?:number;

}
//---------------------------------------------------------
export interface IReportCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;

    sumQuantity?: { from?: number ; to?: number} ;
sumPrice?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------

export interface IReportCompareRequest extends IEntityRequest<IReportCompareFilter> {}

//---------------------------------------------------------
export function initReportCompareRequest(): IReportCompareRequest {
    let request: IReportCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ReportCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SsPortfolio_Name: undefined ,

            sumQuantity: { from: undefined , to: undefined} ,
sumPrice: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getReportCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

