import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"

import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:10 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:10 },
{ fieldName: 'sspTransactionId', displayName: 'StocksShare Transaction', sortName: 'SspTransaction_Name', width: '50px', isVisible:false, columnOrder:9 },
{ fieldName: 'source', displayName: 'Source', sortName: 'Source', width: '50px', isVisible:false, columnOrder:10 },
{ fieldName: 'sourceDate', displayName: 'Source Date', sortName: 'SourceDate', width: '50px', isVisible:false, columnOrder:10 },
{ fieldName: 'transactionDate', displayName: 'Transaction Date', sortName: 'TransactionDate', width: '50px', isVisible:false, columnOrder:2 },
{ fieldName: 'value', displayName: 'Value', sortName: 'Value', width: '50px', isVisible:true, columnOrder:5 },
{ fieldName: 'transactionTypeId', displayName: 'Transaction Type', sortName: 'TransactionType_Name', width: '50px', isVisible:true, columnOrder:3 },
{ fieldName: 'expensesTypeId', displayName: 'Expenses Type', sortName: 'ExpensesType_Name', width: '50px', isVisible:true, columnOrder:4 },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:6 },
{ fieldName: 'ssPortfolioId', displayName: 'Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },

];
export const CashTransactionColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface ICashTransaction extends IEntity{
    isDeleted?: boolean;
id?: string;
notes?: string;
source?: string;
sourceDate?: Date | null;
transactionDate?: Date | null;
value?: number;

    sspTransactionId?: string;
transactionTypeId?: number;
expensesTypeId?: string;
currencyId?: string;

    ssPortfolioId?: string;
    ssPortfolioName?: string;

}
//---------------------------------------------------------
export function initCashTransaction(): ICashTransaction {
    let entity: ICashTransaction = {
        isDeleted: false,
id: '0',
notes: '',
source: '',
sourceDate: new Date(),
transactionDate: new Date(),
value: 0,

        sspTransactionId: '0',
transactionTypeId: 0,
expensesTypeId: '0',
currencyId: '0',

        ssPortfolioId: '0',
        ssPortfolioName: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function cashTransactionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
source: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
sourceDate: [new Date(),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
transactionDate: [new Date(),getFormControlValidation('{"IsRequired":true,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
value: [0,getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"9999999999999.99","RegexPattern":"","AllowedValues":""}')],

    sspTransactionId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
transactionTypeId: [0,getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
expensesTypeId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
currencyId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
    ssPortfolioName: [''],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ICashTransactionRequest extends IEntityRequest<ICashTransactionFilter> {}

//---------------------------------------------------------
export interface ICashTransactionFilter {
    [key: string]: any;
    Id?: string;

    SspTransactionId?: string;
TransactionTypeId?: number;
ExpensesTypeId?: string;
CurrencyId?: string;

    SsPortfolioId?: string;
    SsPortfolioName?: string,

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

            SspTransactionId: undefined ,
TransactionTypeId: undefined ,
ExpensesTypeId: undefined ,
CurrencyId: undefined ,

            SsPortfolioId: undefined ,
            SsPortfolioName: undefined ,

            TransactionDate: { from: undefined , to: undefined} ,
Value: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderCashTransaction(record: ICashTransaction, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ICashTransaction];
        switch (column.fieldName) {
            case 'sspTransactionId':
                return record['sspTransactionName']?.toString();
case 'transactionTypeId':
                return record['transactionTypeName']?.toString();
case 'expensesTypeId':
                return record['expensesTypeName']?.toString();
case 'currencyId':
                return record['currencyName']?.toString();

            case 'ssPortfolioId':
                return record['ssPortfolioName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getCashTransactionActions(component: any, record: ICashTransaction): IAction[] {
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
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.cashTransactionRollback, displayText: 'Delete and Rollback Transaction', data: { recordId: record['id'],  deleteUrl: '/Operations/CashTransaction/Rollback'}
});
}
if (component.accessService.isActionAllowed('cashTransaction', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.cashTransactionChange, displayText: 'Update and Rollout Changes', data: { recordId: record['id'],  putUrl: '/Operations/CashTransaction/Change'}
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ICashTransaction, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//---------------------------------------------------------
export function isEnabled(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

