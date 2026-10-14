import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"
import * as operations from '@bfs/stockex/main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'overdraftPortfolio_Id', displayName: 'ID', sortName: 'OverdraftPortfolio_Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_OverdraftMax', displayName: 'Overdraft Max', sortName: 'OverdraftPortfolio_OverdraftMax', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'brokerAgreement_SsPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_BrokerAgreementId', displayName: 'Broker Agreement', sortName: 'BrokerAgreement_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'brokerAgreement_BrokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'brokerAgreement_InvestorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'overdraftPortfolio_Overdraft', displayName: 'Overdraft ', sortName: 'OverdraftPortfolio_Overdraft', width: '50px', isVisible:true, columnOrder:1 },

    { fieldName: 'sumTotalCashDeposit', displayName: 'Total Cash Deposit', sortName: 'sumTotalCashDeposit', width: '50px', isVisible:true,  columnOrder:1 },
{ fieldName: 'sumAvailableCredit', displayName: 'Available Credit', sortName: 'sumAvailableCredit', width: '50px', isVisible:true,  columnOrder:1 },

];
export const OverdraftRepCompareColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface IOverdraftRepCompare {
    [key: string]: any;
    overdraftPortfolio_Id?: string;
overdraftPortfolio_OverdraftMax?: number;
brokerAgreement_SsPortfolioId?: string;
overdraftPortfolio_BrokerAgreementId?: string;
brokerAgreement_BrokerId?: string;
brokerAgreement_InvestorId?: string;
overdraftPortfolio_Overdraft?: number;

}
//---------------------------------------------------------
export interface IOverdraftRepCompareFilter {
    [key: string]: any;
    OverdraftPortfolio_Id?: string;

    BrokerAgreement_SsPortfolioId?: string;
OverdraftPortfolio_BrokerAgreementId?: string;
BrokerAgreement_BrokerId?: string;
BrokerAgreement_InvestorId?: string;

    OverdraftPortfolio_OverdraftMax?: { from?: number ; to?: number} ;
OverdraftPortfolio_Overdraft?: { from?: number ; to?: number} ;

    sumTotalCashDeposit?: { from?: number ; to?: number} ;
sumAvailableCredit?: { from?: number ; to?: number} ;

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

            BrokerAgreement_SsPortfolioId: undefined ,
OverdraftPortfolio_BrokerAgreementId: undefined ,
BrokerAgreement_BrokerId: undefined ,
BrokerAgreement_InvestorId: undefined ,

            OverdraftPortfolio_OverdraftMax: { from: undefined , to: undefined} ,
OverdraftPortfolio_Overdraft: { from: undefined , to: undefined} ,

            sumTotalCashDeposit: { from: undefined , to: undefined} ,
sumAvailableCredit: { from: undefined , to: undefined} ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderOverdraftRepCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            case 'brokerAgreement_SsPortfolioId':
                return record['ssPortfolioName']?.toString();
case 'overdraftPortfolio_BrokerAgreementId':
                return record['brokerAgreementName']?.toString();
case 'brokerAgreement_BrokerId':
                return record['brokerName']?.toString();
case 'brokerAgreement_InvestorId':
                return record['investorName']?.toString();

            case 'sumTotalCashDeposit':
                return record['sumTotalCashDeposit']?.toString();
case 'sumAvailableCredit':
                return record['sumAvailableCredit']?.toString();

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

