import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const OverdraftPortfolioColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'overdraftValue', displayName: 'Overdraft Value', sortName: 'OverdraftValue', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface IOverdraftPortfolio extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
overdraftValue?: number;

    ssPortfolioId?: string;

}
//---------------------------------------------------------
export function initOverdraftPortfolio(): IOverdraftPortfolio {
    let entity: IOverdraftPortfolio = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
overdraftValue: 0,

        ssPortfolioId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function overdraftPortfolioUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
overdraftValue: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IOverdraftPortfolioRequest extends IEntityRequest<IOverdraftPortfolioFilter> {}

//---------------------------------------------------------
export interface IOverdraftPortfolioFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    SsPortfolioId?: string;

    OverdraftValue?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initOverdraftPortfolioRequest(): IOverdraftPortfolioRequest {
    let request: IOverdraftPortfolioRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: OverdraftPortfolioColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            SsPortfolioId: undefined ,

            OverdraftValue: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderOverdraftPortfolio(record: IOverdraftPortfolio, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IOverdraftPortfolio];
        switch (column.fieldName) {
            case 'ssPortfolioId':
                return record['ssPortfolioName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getOverdraftPortfolioActions(component: any, record: IOverdraftPortfolio): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/overdraft-portfolio/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/overdraft-portfolio/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/overdraft-portfolio/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/overdraft-portfolio/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('overdraftPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IOverdraftPortfolio, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

