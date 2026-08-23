import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/stockex-main/stockex.operations';

// Output Columns of a Query  [used in entity Query]
export const TradingRoomRepCompareColumns = [
    { fieldName: 'tradingRoom_Id', displayName: 'ID', sortName: 'TradingRoom_Id', width: '50px', isVisible:false },
{ fieldName: 'tradingRoom_Name', displayName: 'Name', sortName: 'TradingRoom_Name', width: '50px', isVisible:true },
{ fieldName: 'tradingRoom_Notes', displayName: 'Notes', sortName: 'TradingRoom_Notes', width: '50px', isVisible:false },

];

//---------------------------------------------------------

export interface ITradingRoomRepCompare {
    tradingRoom_Id?: string;
tradingRoom_Name?: string;
tradingRoom_Notes?: string;

}
//---------------------------------------------------------
export interface ITradingRoomRepCompareWithLookup extends ITradingRoomRepCompare{

}
//---------------------------------------------------------
export interface ITradingRoomRepCompareFilter {
    [key: string]: any;
    TradingRoom_Id?: string;

    TradingRoom_Name?: string;

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
            TradingRoom_Id: undefined ,

            TradingRoom_Name: undefined ,

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

