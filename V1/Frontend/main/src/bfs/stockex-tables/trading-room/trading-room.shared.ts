
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const TradingRoomColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'broker', displayName: 'Brokers', sortName: 'Broker', width: '50px', isVisible:true },
{ fieldName: 'stockShare', displayName: 'StockShares', sortName: 'StockShare', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface ITradingRoom {
    isDeleted?: boolean;
id?: string;
name?: string;

}
//---------------------------------------------------------
export function initTradingRoom(): ITradingRoom {
    let entity: ITradingRoom = {
        isDeleted: false,
id: '0',
name: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function tradingRoomUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],

    };
} 
//---------------------------------------------------------
export interface ITradingRoomWithLookup extends ITradingRoom{

}
//---------------------------------------------------------
export interface ITradingRoomRequest extends IEntityRequest<ITradingRoomFilter> {}

//---------------------------------------------------------
export interface ITradingRoomFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initTradingRoomRequest(): ITradingRoomRequest {
    let request: ITradingRoomRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: TradingRoomColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getTradingRoomActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('tradingRoom', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/trading-room/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('tradingRoom', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/trading-room/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('tradingRoom', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/trading-room/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('tradingRoom', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/trading-room/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

