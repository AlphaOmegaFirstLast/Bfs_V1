import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex/main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const PortfolioAggregateCompareColumns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Name', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'broker_Name', displayName: 'Broker Name', sortName: 'Broker_Name', width: '50px', isVisible:true },
{ fieldName: 'investor_Name', displayName: 'Investor Name', sortName: 'Investor_Name', width: '50px', isVisible:true },

    { fieldName: 'sumQuantity', displayName: 'Quantity', sortName: 'sumQuantity', width: '50px', isVisible:true },
{ fieldName: 'sumPrice', displayName: 'Price', sortName: 'sumPrice', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IPortfolioAggregateCompare {
    [key: string]: any;
    ssPortfolio_Name?: string;
broker_Name?: string;
investor_Name?: string;

}
//---------------------------------------------------------
export interface IPortfolioAggregateCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;

    sumQuantity?: { from?: number ; to?: number} ;
sumPrice?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------

export interface IPortfolioAggregateCompareRequest extends IEntityRequest<IPortfolioAggregateCompareFilter> {}

//---------------------------------------------------------
export function initPortfolioAggregateCompareRequest(): IPortfolioAggregateCompareRequest {
    let request: IPortfolioAggregateCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: PortfolioAggregateCompareColumns.map(column => ({ ...column })),
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
export function renderPortfolioAggregateCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            case 'sumQuantity':
                return record['sumQuantity']?.toString();
case 'sumPrice':
                return record['sumPrice']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getPortfolioAggregateCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

