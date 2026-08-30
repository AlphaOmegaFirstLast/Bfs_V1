
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CashTransactionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'sspTransactionId', displayName: 'StocksShare Transaction', sortName: 'SspTransaction_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'source', displayName: 'Source', sortName: 'Source', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'sourceDate', displayName: 'Source Date', sortName: 'SourceDate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'transactionDate', displayName: 'Transaction Date', sortName: 'TransactionDate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'value', displayName: 'Value', sortName: 'Value', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'transactionTypeId', displayName: 'Transaction Type', sortName: 'TransactionType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'expensesTypeId', displayName: 'Expenses Type', sortName: 'ExpensesType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ICashTransaction {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
source?: string;
sourceDate?: Date | null;
transactionDate?: Date | null;
value?: number;

    sspTransactionId?: string;
ssPortfolioId?: string;
transactionTypeId?: number;
expensesTypeId?: string;
currencyId?: string;

}
//---------------------------------------------------------
export function initCashTransaction(): ICashTransaction {
    let entity: ICashTransaction = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
source: '',
sourceDate: new Date(0),
transactionDate: new Date(0),
value: 0,

        sspTransactionId: '0',
ssPortfolioId: '0',
transactionTypeId: 0,
expensesTypeId: '0',
currencyId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function cashTransactionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
source: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
sourceDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
transactionDate: [new Date(0),getFormControlValidation('{"IsRequired":true,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
value: [0,getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"9999999999999.99","RegexPattern":"","AllowedValues":""}')],

    sspTransactionId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
transactionTypeId: [0,getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
expensesTypeId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
currencyId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    };
} 
//---------------------------------------------------------
export interface ICashTransactionWithLookup extends ICashTransaction{

    sspTransactionName?: string;
ssPortfolioName?: string;
transactionTypeName?: string;
expensesTypeName?: string;
currencyName?: string;

}
//---------------------------------------------------------
export interface ICashTransactionRequest extends IEntityRequest<ICashTransactionFilter> {}

//---------------------------------------------------------
export interface ICashTransactionFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SspTransactionId?: string;
SsPortfolioId?: string;
TransactionTypeId?: number;
ExpensesTypeId?: string;
CurrencyId?: string;

    SourceDate?: { from?: Date | null ; to?: Date | null} ;
TransactionDate?: { from?: Date | null ; to?: Date | null} ;
Value?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initCashTransactionRequest(): ICashTransactionRequest {
    let request: ICashTransactionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CashTransactionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SspTransactionId: undefined ,
SsPortfolioId: undefined ,
TransactionTypeId: undefined ,
ExpensesTypeId: undefined ,
CurrencyId: undefined ,

            SourceDate: { from: undefined , to: undefined} ,
TransactionDate: { from: undefined , to: undefined} ,
Value: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getCashTransactionActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/cash-transaction/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/cash-transaction/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/cash-transaction/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/cash-transaction/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['sspTransactionId'], route:'/stkx/ssp-transaction/view', displayText:'Go to SspTransaction'
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['transactionTypeId'], route:'/stkx/transaction-type/view', displayText:'Go to TransactionType'
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['expensesTypeId'], route:'/stkx/expenses-type/view', displayText:'Go to ExpensesType'
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['currencyId'], route:'/stkx/currency/view', displayText:'Go to Currency'
});
}

if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.cashTransactionRollout, displayText: 'Save and Rollout Transaction', data: { recordId: record['id'],  postUrl: '/Operations/CashTransaction/Rollout'}
});
}

        return links;
    }
    //---------------------------------------------------------

