
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CouponColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'stockShareId', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible:true },
{ fieldName: 'couponTypeId', displayName: 'Coupon Type', sortName: 'CouponType_Name', width: '50px', isVisible:true },
{ fieldName: 'value', displayName: 'Value', sortName: 'Value', width: '50px', isVisible:false },
{ fieldName: 'announceDate', displayName: 'Announce Date', sortName: 'AnnounceDate', width: '50px', isVisible:true },
{ fieldName: 'valueDate', displayName: 'Value Date', sortName: 'ValueDate', width: '50px', isVisible:true },
{ fieldName: 'dueDate', displayName: 'Due Date', sortName: 'DueDate', width: '50px', isVisible:true },
{ fieldName: 'couponPercent', displayName: 'Percent', sortName: 'CouponPercent', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface ICoupon {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
value?: number;
announceDate?: Date | null;
valueDate?: Date | null;
dueDate?: Date | null;
couponPercent?: number;

    tradingRoomId?: string;
stockShareId?: string;
couponTypeId?: string;
couponStatusId?: string;

}
//---------------------------------------------------------
export function initCoupon(): ICoupon {
    let entity: ICoupon = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
value: 0,
announceDate: null,
valueDate: null,
dueDate: null,
couponPercent: 0,

        tradingRoomId: '0',
stockShareId: '0',
couponTypeId: '0',
couponStatusId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function couponUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
value: [0],
announceDate: [null],
valueDate: [null],
dueDate: [null],
couponPercent: [0],

    tradingRoomId: ['0'],
stockShareId: ['0'],
couponTypeId: ['0'],
couponStatusId: ['0'],

    };
} 
//---------------------------------------------------------
export interface ICouponWithLookup extends ICoupon{

    tradingRoomName?: string;
stockShareName?: string;
couponTypeName?: string;
couponStatusName?: string;

}
//---------------------------------------------------------
export interface ICouponRequest extends IEntityRequest<ICouponFilter> {}

//---------------------------------------------------------
export interface ICouponFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    TradingRoomId?: string;
StockShareId?: string;
CouponTypeId?: string;
CouponStatusId?: string;

    Value?: { from?: number ; to?: number} ;
AnnounceDate?: { from?: Date | null ; to?: Date | null} ;
ValueDate?: { from?: Date | null ; to?: Date | null} ;
DueDate?: { from?: Date | null ; to?: Date | null} ;
CouponPercent?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initCouponRequest(): ICouponRequest {
    let request: ICouponRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CouponColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            TradingRoomId: undefined ,
StockShareId: undefined ,
CouponTypeId: undefined ,
CouponStatusId: undefined ,

            Value: { from: undefined , to: undefined} ,
AnnounceDate: { from: undefined , to: undefined} ,
ValueDate: { from: undefined , to: undefined} ,
DueDate: { from: undefined , to: undefined} ,
CouponPercent: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getCouponActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/coupon/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/coupon/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/coupon/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/coupon/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['tradingRoomId'], route:'/stkx/trading-room/view', displayText:'Go to TradingRoom'
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['stockShareId'], route:'/stkx/stock-share/view', displayText:'Go to StockShare'
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['couponTypeId'], route:'/stkx/coupon-type/view', displayText:'Go to CouponType'
});
}
if (component.accessService.isActionAllowed('coupon', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['couponStatusId'], route:'/stkx/coupon-status/view', displayText:'Go to CouponStatus'
});
}

        return links;
    }
    //---------------------------------------------------------

