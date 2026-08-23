import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const PortfolioCashTransactionAggregateCompareColumns = [
    { fieldName: 'ssPortfolio_Name', displayName: 'Name', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },
{ fieldName: 'broker_Name', displayName: 'Broker Name', sortName: 'Broker_Name', width: '50px', isVisible:true },
{ fieldName: 'investor_Name', displayName: 'Investor Name', sortName: 'Investor_Name', width: '50px', isVisible:true },

    { fieldName: 'sumValue', displayName: 'Value', sortName: 'sumValue', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface IPortfolioCashTransactionAggregateCompare {
    ssPortfolio_Name?: string;
broker_Name?: string;
investor_Name?: string;

}
//---------------------------------------------------------
export interface IPortfolioCashTransactionAggregateCompareWithLookup extends IPortfolioCashTransactionAggregateCompare{

    sumValue?:number;

}
//---------------------------------------------------------
export interface IPortfolioCashTransactionAggregateCompareFilter {
    [key: string]: any;

    SsPortfolio_Name?: string;

    sumValue?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------

export interface IPortfolioCashTransactionAggregateCompareRequest extends IEntityRequest<IPortfolioCashTransactionAggregateCompareFilter> {}

//---------------------------------------------------------
export function initPortfolioCashTransactionAggregateCompareRequest(): IPortfolioCashTransactionAggregateCompareRequest {
    let request: IPortfolioCashTransactionAggregateCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: PortfolioCashTransactionAggregateCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            SsPortfolio_Name: undefined ,

            sumValue: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getPortfolioCashTransactionAggregateCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

