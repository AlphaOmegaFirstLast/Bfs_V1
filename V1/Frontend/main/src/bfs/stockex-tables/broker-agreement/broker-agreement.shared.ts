
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BrokerAgreementColumns = [
    { fieldName: 'agreementDate', displayName: 'Agreement Date', sortName: 'AgreementDate', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'overdraftPrcnt', displayName: 'Overdraft Percent', sortName: 'OverdraftPrcnt', width: '50px', isVisible:false },
{ fieldName: 'overdraftMx', displayName: 'Overdraft Max', sortName: 'OverdraftMx', width: '50px', isVisible:false },
{ fieldName: 'investorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible:true },
{ fieldName: 'brokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible:true },
{ fieldName: 'ssPortfolioId', displayName: 'StockShare Portfolio', sortName: 'SsPortfolio_Name', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IBrokerAgreement {
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
        agreementDate: null,
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
    agreementDate: [null],
isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
overdraftPrcnt: [0],
overdraftMx: [0],

    investorId: ['0'],
brokerId: ['0'],
ssPortfolioId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IBrokerAgreementWithLookup extends IBrokerAgreement{

    investorName?: string;
brokerName?: string;
ssPortfolioName?: string;

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

export function getBrokerAgreementActions(component: any, record: IEntity): IAction[] {
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

