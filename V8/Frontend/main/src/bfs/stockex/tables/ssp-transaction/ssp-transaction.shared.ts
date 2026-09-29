import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const SspTransactionColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible: false, columnOrder: 1 },
    { fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible: false, columnOrder: 1 },
    { fieldName: 'sourceDate', displayName: 'Source Date', sortName: 'SourceDate', width: '50px', isVisible: false, columnOrder: 1 },
    { fieldName: 'transactionDate', displayName: 'Transaction Date', sortName: 'TransactionDate', width: '50px', isVisible: false, columnOrder: 1 },
    { fieldName: 'source', displayName: 'Source', sortName: 'Source', width: '50px', isVisible: false, columnOrder: 1 },
    { fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'transactionTypeId', displayName: 'Transaction Type', sortName: 'TransactionType_Name', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'quantity', displayName: 'Quantity', sortName: 'Quantity', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'price', displayName: 'Price', sortName: 'Price', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'stockShareId', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'toQuantity', displayName: 'To Quantity', sortName: 'ToQuantity', width: '50px', isVisible: true, columnOrder: 1 },
    { fieldName: 'toPortfolioId', displayName: 'To Portfolio', sortName: 'ToPortfolio_Name', width: '50px', isVisible: true, columnOrder: 1 },

];
//---------------------------------------------------------
export interface ISspTransaction extends IEntity {
    isDeleted?: boolean;
    id?: string;
    name?: string;
    notes?: string;
    sourceDate?: Date | null;
    transactionDate?: Date | null;
    source?: string;
    quantity?: number;
    price?: number;
    toQuantity?: number;

    ssPortfolioId?: string;
    transactionTypeId?: number;
    stockShareId?: string;
    toPortfolioId?: string;

}
//---------------------------------------------------------
export function initSspTransaction(): ISspTransaction {
    let entity: ISspTransaction = {
        isDeleted: false,
        id: '0',
        name: '',
        notes: '',
        sourceDate: new Date(0),
        transactionDate: new Date(0),
        source: '',
        quantity: 0,
        price: 0,
        toQuantity: 0,

        ssPortfolioId: '0',
        transactionTypeId: 0,
        stockShareId: '0',
        toPortfolioId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function sspTransactionUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
        isDeleted: [false, getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        id: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        name: ['', getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        notes: ['', getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        sourceDate: [new Date(0), getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        transactionDate: [new Date(0), getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        source: ['', getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        quantity: [0, getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        price: [0, getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        toQuantity: [0, getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

        ssPortfolioId: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        transactionTypeId: [0, getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        stockShareId: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
        toPortfolioId: ['0', getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

        //Template_Start_Code_DontOverwrite_2
        //Template_End_Code_DontOverwrite_2

    };
}
//---------------------------------------------------------
export interface ISspTransactionRequest extends IEntityRequest<ISspTransactionFilter> { }

//---------------------------------------------------------
export interface ISspTransactionFilter {
    [key: string]: any;
    Id?: string;
    Name?: string;

    SsPortfolioId?: string;
    TransactionTypeId?: number;
    StockShareId?: string;
    ToPortfolioId?: string;

    SourceDate?: { from?: Date | null; to?: Date | null };
    TransactionDate?: { from?: Date | null; to?: Date | null };
    Quantity?: { from?: number; to?: number };
    Price?: { from?: number; to?: number };
    ToQuantity?: { from?: number; to?: number };
}
//---------------------------------------------------------
export function initSspTransactionRequest(): ISspTransactionRequest {
    let request: ISspTransactionRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SspTransactionColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
        },
        filter: {
            Id: undefined,

            Name: undefined,

            SsPortfolioId: undefined,
            TransactionTypeId: undefined,
            StockShareId: undefined,
            ToPortfolioId: undefined,

            SourceDate: { from: undefined, to: undefined },
            TransactionDate: { from: undefined, to: undefined },
            Quantity: { from: undefined, to: undefined },
            Price: { from: undefined, to: undefined },
            ToQuantity: { from: undefined, to: undefined },

        }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderSspTransaction(record: ISspTransaction, column: IQueryColumn): any {
    const value = record[column.fieldName as keyof ISspTransaction];
    switch (column.fieldName) {
        case 'ssPortfolioId':
            return record['ssPortfolioName']?.toString();
        case 'transactionTypeId':
            return record['transactionTypeName']?.toString();
        case 'stockShareId':
            return record['stockShareName']?.toString();
        case 'toPortfolioId':
            return record['toPortfolioName']?.toString();

        default:
            return value;
    }
    return value;
}
//---------------------------------------------------------
export function getSspTransactionActions(component: any, record: ISspTransaction): IAction[] {
    let links: IAction[] = [];

    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListHeader', recordId: 0, route: '/stkx/ssp-transaction/add', displayText: 'Add New record'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/ssp-transaction/view', displayText: 'View...'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/ssp-transaction/edit', displayText: 'Edit...'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['id'], route: '/stkx/ssp-transaction/delete', displayText: 'Delete...'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['ssPortfolioId'], route: '/stkx/ss-portfolio/view', displayText: 'Go to SsPortfolio'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['transactionTypeId'], route: '/stkx/transaction-type/view', displayText: 'Go to TransactionType'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['stockShareId'], route: '/stkx/stock-share/view', displayText: 'Go to StockShare'
        });
    }
    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendLink', actionLocation: 'ListRow', recordId: record['toPortfolioId'], route: '/stkx/to-portfolio/view', displayText: 'Go to ToPortfolio'
        });
    }

    if (component.accessService.isActionAllowed('sspTransaction', '')) {
        links.push({
            actionSource: 'System', actionType: 'FrontendFunction', actionLocation: 'FormHeader', recordId: record['id'], action: operations.sspTransactionRollout, displayText: 'Save and Rollout Transaction', data: { recordId: record['id'], postUrl: '/Operations/SspTransaction/Rollout' }
        });
    }

    return links;
}
//---------------------------------------------------------
//Template_Start_Code_DontOverwrite_3
export function isVisible(entity: ISspTransaction, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    let control = validationForm.get('transactionTypeId');
    let transactionTypeId = control?.value;
    switch (fieldName.toLocaleLowerCase()) {
        case 'toportfolioid':
            return (transactionTypeId == 9 || transactionTypeId == 10);
        case 'toquantity':
            return (transactionTypeId == 15 || transactionTypeId == 16) ? true : false;
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
//Template_End_Code_DontOverwrite_3

