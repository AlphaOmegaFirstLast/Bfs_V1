import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const BrokerAgreementColumns = [
    { fieldName: 'agreementDate', displayName: 'Agreement Date', sortName: 'AgreementDate', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftPrcnt', displayName: 'Overdraft Percent', sortName: 'OverdraftPrcnt', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'overdraftMx', displayName: 'Overdraft Max', sortName: 'OverdraftMx', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'investorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'brokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IBrokerAgreement extends IEntity{
    agreementDate?: Date | null;
isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
overdraftPrcnt?: number;
overdraftMx?: number;

    investorId?: string;
brokerId?: string;
ssPortfolioId?: string;

}
//---------------------------------------------------------
export function initBrokerAgreement(): IBrokerAgreement {
    let entity: IBrokerAgreement = {
        agreementDate: new Date(0),
isDeleted: false,
id: '0',
name: '',
notes: '',
overdraftPrcnt: 0,
overdraftMx: 0,

        investorId: '0',
brokerId: '0',
ssPortfolioId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function brokerAgreementUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    agreementDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
overdraftPrcnt: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
overdraftMx: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    investorId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
brokerId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
ssPortfolioId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IBrokerAgreementRequest extends IEntityRequest<IBrokerAgreementFilter> {}

//---------------------------------------------------------
export interface IBrokerAgreementFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    InvestorId?: string;
BrokerId?: string;
SsPortfolioId?: string;

    AgreementDate?: { from?: Date | null ; to?: Date | null} ;
OverdraftPrcnt?: { from?: number ; to?: number} ;
OverdraftMx?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initBrokerAgreementRequest(): IBrokerAgreementRequest {
    let request: IBrokerAgreementRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BrokerAgreementColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            InvestorId: undefined ,
BrokerId: undefined ,
SsPortfolioId: undefined ,

            AgreementDate: { from: undefined , to: undefined} ,
OverdraftPrcnt: { from: undefined , to: undefined} ,
OverdraftMx: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderBrokerAgreement(record: IBrokerAgreement, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IBrokerAgreement];
        switch (column.fieldName) {
            case 'investorId':
                return record['investorName']?.toString();
case 'brokerId':
                return record['brokerName']?.toString();
case 'ssPortfolioId':
                return record['ssPortfolioName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getBrokerAgreementActions(component: any, record: IBrokerAgreement): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/broker-agreement/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/broker-agreement/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/broker-agreement/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/broker-agreement/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['investorId'], route:'/stkx/investor/view', displayText:'Go to Investor'
});
}
if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['brokerId'], route:'/stkx/broker/view', displayText:'Go to Broker'
});
}
if (component.accessService.isActionAllowed('brokerAgreement', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['ssPortfolioId'], route:'/stkx/ss-portfolio/view', displayText:'Go to SsPortfolio'
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IBrokerAgreement, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

