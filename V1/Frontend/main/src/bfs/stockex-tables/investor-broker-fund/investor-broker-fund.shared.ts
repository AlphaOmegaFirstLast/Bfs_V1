
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const InvestorBrokerFundColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'brokerId', displayName: 'Broker', sortName: 'Broker_Name', width: '50px', isVisible:true },
{ fieldName: 'investorId', displayName: 'Investor', sortName: 'Investor_Name', width: '50px', isVisible:true },
{ fieldName: 'fund', displayName: 'Fund', sortName: 'Fund', width: '50px', isVisible:false },
{ fieldName: 'fundDate', displayName: 'Fund Date', sortName: 'FundDate', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IInvestorBrokerFund {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
fund?: number;
fundDate?: Date | null;

    brokerId?: string;
investorId?: string;

}
//---------------------------------------------------------
export function initInvestorBrokerFund(): IInvestorBrokerFund {
    let entity: IInvestorBrokerFund = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
fund: 0,
fundDate: null,

        brokerId: '0',
investorId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function investorBrokerFundUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
fund: [0],
fundDate: [null],

    brokerId: ['0'],
investorId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IInvestorBrokerFundWithLookup extends IInvestorBrokerFund{

    brokerName?: string;
investorName?: string;

}
//---------------------------------------------------------
export interface IInvestorBrokerFundRequest extends IEntityRequest<IInvestorBrokerFundFilter> {}

//---------------------------------------------------------
export interface IInvestorBrokerFundFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    BrokerId?: string;
InvestorId?: string;

    Fund?: { from?: number ; to?: number} ;
FundDate?: { from?: Date | null ; to?: Date | null} ;

}
//---------------------------------------------------------
export function initInvestorBrokerFundRequest(): IInvestorBrokerFundRequest {
    let request: IInvestorBrokerFundRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: InvestorBrokerFundColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            BrokerId: undefined ,
InvestorId: undefined ,

            Fund: { from: undefined , to: undefined} ,
FundDate: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getInvestorBrokerFundActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('investorBrokerFund', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/investor-broker-fund/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('investorBrokerFund', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/investor-broker-fund/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('investorBrokerFund', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/investor-broker-fund/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('investorBrokerFund', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/investor-broker-fund/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('investorBrokerFund', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['brokerId'], route:'/stkx/broker/view', displayText:'Go to Broker'
});
}
if (component.accessService.isActionAllowed('investorBrokerFund', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['investorId'], route:'/stkx/investor/view', displayText:'Go to Investor'
});
}

        return links;
    }
    //---------------------------------------------------------

