import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const SsPortfolioBalanceColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'ssPortfolioId', displayName: ' Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'balance', displayName: 'Balance', sortName: 'Balance', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'currencyId', displayName: 'Currency', sortName: 'Currency_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ISsPortfolioBalance extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
balance?: number;

    ssPortfolioId?: string;
currencyId?: string;

}
//---------------------------------------------------------
export function initSsPortfolioBalance(): ISsPortfolioBalance {
    let entity: ISsPortfolioBalance = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
balance: 0,

        ssPortfolioId: '0',
currencyId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function ssPortfolioBalanceUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
balance: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
currencyId: ['0',getFormControlValidation('{"IsRequired":true,"MinLength":null,"MaxLength":null,"MinValue":"1","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ISsPortfolioBalanceRequest extends IEntityRequest<ISsPortfolioBalanceFilter> {}

//---------------------------------------------------------
export interface ISsPortfolioBalanceFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;
CurrencyId?: string;

    Balance?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initSsPortfolioBalanceRequest(): ISsPortfolioBalanceRequest {
    let request: ISsPortfolioBalanceRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SsPortfolioBalanceColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,
CurrencyId: undefined ,

            Balance: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderSsPortfolioBalance(record: ISsPortfolioBalance, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ISsPortfolioBalance];
        switch (column.fieldName) {
            case 'ssPortfolioId':
                return record['ssPortfolioName']?.toString();
case 'currencyId':
                return record['currencyName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getSsPortfolioBalanceActions(component: any, record: ISsPortfolioBalance): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/ss-portfolio-balance/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio-balance/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio-balance/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio-balance/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}
if (component.accessService.isActionAllowed('ssPortfolioBalance', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['currencyId'], route:'/stkx/currency/view', displayText:'Go to Currency'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ISsPortfolioBalance, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

