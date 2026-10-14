import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"
import * as operations from '@bfs/stockex/main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'overdraftPortfolio_Id', displayName: 'ID', sortName: 'OverdraftPortfolio_Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_Notes', displayName: 'Notes', sortName: 'OverdraftPortfolio_Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_OverdraftPercent', displayName: 'Overdraft Percent', sortName: 'OverdraftPortfolio_OverdraftPercent', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_OverdraftMax', displayName: 'Overdraft Max', sortName: 'OverdraftPortfolio_OverdraftMax', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_SsPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_BrokerAgreementId', displayName: 'Broker Agreement', sortName: 'BrokerAgreement_Name', width: '50px', isVisible:true, columnOrder:1 },

];
export const OverdraftRepCompareColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface IOverdraftRepCompare {
    [key: string]: any;
    overdraftPortfolio_Id?: string;
overdraftPortfolio_Notes?: string;
overdraftPortfolio_OverdraftPercent?: number;
overdraftPortfolio_OverdraftMax?: number;
overdraftPortfolio_SsPortfolioId?: string;
overdraftPortfolio_BrokerAgreementId?: string;

}
//---------------------------------------------------------
export interface IOverdraftRepCompareFilter {
    [key: string]: any;
    OverdraftPortfolio_Id?: string;

    OverdraftPortfolio_Name?: string;

    OverdraftPortfolio_SsPortfolioId?: string;
OverdraftPortfolio_BrokerAgreementId?: string;

    OverdraftPortfolio_OverdraftPercent?: { from?: number ; to?: number} ;
OverdraftPortfolio_OverdraftMax?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------

export interface IOverdraftRepCompareRequest extends IEntityRequest<IOverdraftRepCompareFilter> {}

//---------------------------------------------------------
export function initOverdraftRepCompareRequest(): IOverdraftRepCompareRequest {
    let request: IOverdraftRepCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: OverdraftRepCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {
            OverdraftPortfolio_Id: undefined ,

            OverdraftPortfolio_Name: undefined ,

            OverdraftPortfolio_SsPortfolioId: undefined ,
OverdraftPortfolio_BrokerAgreementId: undefined ,

            OverdraftPortfolio_OverdraftPercent: { from: undefined , to: undefined} ,
OverdraftPortfolio_OverdraftMax: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderOverdraftRepCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'overdraftPortfolio_SsPortfolioId':
                return record['ssPortfolioName']?.toString();
case 'overdraftPortfolio_BrokerAgreementId':
                return record['brokerAgreementName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getOverdraftRepCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

