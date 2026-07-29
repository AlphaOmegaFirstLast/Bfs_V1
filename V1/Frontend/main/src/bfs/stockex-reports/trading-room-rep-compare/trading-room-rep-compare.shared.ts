import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const TradingRoomRepCompareColumns = [
    { fieldName: 'stkxTradingRoom_Name', displayName: 'Name', sortName: 'stkxTradingRoom_Name', width: '50px', isVisible:true },

];

//---------------------------------------------------------

export interface ITradingRoomRepCompare {
    stkxTradingRoom_Name?: string;

}
//---------------------------------------------------------
export interface ITradingRoomRepCompareWithLookup extends ITradingRoomRepCompare{

}
//---------------------------------------------------------
export interface ITradingRoomRepCompareFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------

export interface ITradingRoomRepCompareRequest extends IEntityRequest<ITradingRoomRepCompareFilter> {}

//---------------------------------------------------------
export function initTradingRoomRepCompareRequest(): ITradingRoomRepCompareRequest {
    let request: ITradingRoomRepCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: TradingRoomRepCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {

            Name: undefined ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getTradingRoomRepCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

