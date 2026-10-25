import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"
import * as operations from '@bfs/stockex/main/stockex.operations';
import { setArrowData } from "@bfs/_shared/helpers/html.helper";

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'overdraftPortfolio_Id', displayName: 'ID', sortName: 'OverdraftPortfolio_Id', width: '50px', isVisible: false, columnOrder: 0 },
    { fieldName: 'overdraftPortfolio_SsPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'brokerAgreement_AgreementDate', displayName: 'Agreement Date', sortName: 'BrokerAgreement_AgreementDate', width: '50px', isVisible: true, columnOrder: 2 },
    { fieldName: 'brokerAgreement_InvestorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible: true, columnOrder: 3 },
    { fieldName: 'brokerAgreement_BrokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible: true, columnOrder: 4 },
    { fieldName: 'ssPortfolioBalance_CurrencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible: true, columnOrder: 5 },
    { fieldName: 'ssPortfolioBalance_Balance', displayName: 'Balance', sortName: 'SsPortfolioBalance_Balance', width: '50px', isVisible: true, columnOrder: 6 },
    { fieldName: 'overdraftPortfolio_Overdraft', displayName: 'Overdraft ', sortName: 'OverdraftPortfolio_Overdraft', width: '50px', isVisible: true, columnOrder: 21 },
    { fieldName: 'overdraftPortfolio_OverdraftMax', displayName: 'Overdraft Max', sortName: 'OverdraftPortfolio_OverdraftMax', width: '50px', isVisible: false, columnOrder: 19 },
    { fieldName: 'overdraftPortfolio_OverdraftPercent', displayName: 'Overdraft Percent', sortName: 'OverdraftPortfolio_OverdraftPercent', width: '50px', isVisible: false, columnOrder: 20 },

    { fieldName: 'sumTotalCashDeposit', displayName: 'Total Cash Deposit', sortName: 'sumTotalCashDeposit', width: '50px', isVisible: true, columnOrder: 15 },
    { fieldName: 'sumAvailableCredit', displayName: 'Available Credit', sortName: 'sumAvailableCredit', width: '50px', isVisible: true, columnOrder: 18 },

];
export const OverdraftRepCompareColumns = reOrderColumns(columns);
//---------------------------------------------------------
export interface IOverdraftRepCompare {
    [key: string]: any;
    overdraftPortfolio_Id?: string;
    overdraftPortfolio_SsPortfolioId?: string;
    brokerAgreement_AgreementDate?: Date | null;
    brokerAgreement_InvestorId?: string;
    brokerAgreement_BrokerId?: string;
    ssPortfolioBalance_CurrencyId?: string;
    ssPortfolioBalance_Balance?: number;
    overdraftPortfolio_Overdraft?: number;
    overdraftPortfolio_OverdraftMax?: number;
    overdraftPortfolio_OverdraftPercent?: number;

}
//---------------------------------------------------------
export interface IOverdraftRepCompareFilter {
    [key: string]: any;
    OverdraftPortfolio_Id?: string;

    OverdraftPortfolio_BrokerAgreementId?: string;
    OverdraftPortfolio_SsPortfolioId?: string;
    BrokerAgreement_InvestorId?: string;
    BrokerAgreement_BrokerId?: string;
    SsPortfolioBalance_CurrencyId?: string;

    BrokerAgreement_AgreementDate?: { from?: Date | null; to?: Date | null };
    OverdraftPortfolio_Overdraft?: { from?: number; to?: number };
    OverdraftPortfolio_OverdraftMax?: { from?: number; to?: number };
    OverdraftPortfolio_OverdraftPercent?: { from?: number; to?: number };

    sumTotalCashDeposit?: { from?: number; to?: number };
    sumAvailableCredit?: { from?: number; to?: number };

}
//---------------------------------------------------------

export interface IOverdraftRepCompareRequest extends IEntityRequest<IOverdraftRepCompareFilter> { }

//---------------------------------------------------------
export function initOverdraftRepCompareRequest(): IOverdraftRepCompareRequest {
    let request: IOverdraftRepCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: OverdraftRepCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {
            OverdraftPortfolio_Id: undefined,

            OverdraftPortfolio_BrokerAgreementId: undefined,
            OverdraftPortfolio_SsPortfolioId: undefined,
            BrokerAgreement_InvestorId: undefined,
            BrokerAgreement_BrokerId: undefined,
            SsPortfolioBalance_CurrencyId: undefined,

            BrokerAgreement_AgreementDate: { from: undefined, to: undefined },
            OverdraftPortfolio_Overdraft: { from: undefined, to: undefined },
            OverdraftPortfolio_OverdraftMax: { from: undefined, to: undefined },
            OverdraftPortfolio_OverdraftPercent: { from: undefined, to: undefined },

            sumTotalCashDeposit: { from: undefined, to: undefined },
            sumAvailableCredit: { from: undefined, to: undefined },

        }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderOverdraftRepCompare(record: IEntity, column: IQueryColumn, sanitizer: DomSanitizer): SafeHtml {
    const value = record[column.fieldName as keyof IEntity];
    switch (column.fieldName) {
        case 'overdraftPortfolio_BrokerAgreementId':
            return record['brokerAgreementName']?.toString();
        case 'overdraftPortfolio_SsPortfolioId':
            return record['ssPortfolioName']?.toString();
        case 'brokerAgreement_InvestorId':
            return record['investorName']?.toString();
        case 'brokerAgreement_BrokerId':
            return record['brokerName']?.toString();
        case 'ssPortfolioBalance_CurrencyId':
            return record['currencyName']?.toString();

        case 'overdraftPortfolio_Overdraft':
            return setArrowData(sanitizer,'blue', 'none' ,record['overdraftPortfolio_Overdraft']);

        case 'sumTotalCashDeposit':
            return  record['sumTotalCashDeposit']?.toString();
        case 'sumAvailableCredit':
            return record['sumAvailableCredit']?.toString();

        default:
            return value;
    }
    return value;
}
//---------------------------------------------------------
export function getOverdraftRepCompareActions(component: any, record: IEntity): IAction[] {
    let links: IAction[] = [];

    return links;
}
//---------------------------------------------------------

