import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const SsPortfolioColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'brokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'investorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface ISsPortfolio extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    brokerId?: string;
    brokerName?: string;
investorId?: string;
    investorName?: string;

}
//---------------------------------------------------------
export function initSsPortfolio(): ISsPortfolio {
    let entity: ISsPortfolio = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        brokerId: '0',
        brokerName: '',
investorId: '0',
        investorName: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function ssPortfolioUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    brokerId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
    brokerName: [''],
investorId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
    investorName: [''],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ISsPortfolioRequest extends IEntityRequest<ISsPortfolioFilter> {}

//---------------------------------------------------------
export interface ISsPortfolioFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    BrokerId?: string;
    BrokerName?: string,
InvestorId?: string;
    InvestorName?: string,

}
//---------------------------------------------------------
export function initSsPortfolioRequest(): ISsPortfolioRequest {
    let request: ISsPortfolioRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SsPortfolioColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            BrokerId: undefined ,
            BrokerName: undefined ,
InvestorId: undefined ,
            InvestorName: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderSsPortfolio(record: ISsPortfolio, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ISsPortfolio];
        switch (column.fieldName) {

            case 'brokerId':
                return record['brokerName']?.toString();
case 'investorId':
                return record['investorName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getSsPortfolioActions(component: any, record: ISsPortfolio): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('ssPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/ss-portfolio/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('ssPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('ssPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('ssPortfolio', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/ss-portfolio/delete', displayText: 'Delete...' 
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: ISsPortfolio, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

