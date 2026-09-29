import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const CouponColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'stockShareId', displayName: 'Stock Share', sortName: 'StockShare_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'couponTypeId', displayName: 'Coupon Type', sortName: 'CouponType_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'value', displayName: 'Value', sortName: 'Value', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'announceDate', displayName: 'Announce Date', sortName: 'AnnounceDate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'valueDate', displayName: 'Value Date', sortName: 'ValueDate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'dueDate', displayName: 'Due Date', sortName: 'DueDate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'couponPercent', displayName: 'Percent', sortName: 'CouponPercent', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface ICoupon extends IEntity{
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
announceDate: new Date(0),
valueDate: new Date(0),
dueDate: new Date(0),
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
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"0","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
value: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
announceDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
valueDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
dueDate: [new Date(0),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
couponPercent: [0,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    tradingRoomId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
stockShareId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
couponTypeId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
couponStatusId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
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
export function renderCoupon(record: ICoupon, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof ICoupon];
        switch (column.fieldName) {
            case 'tradingRoomId':
                return record['tradingRoomName']?.toString();
case 'stockShareId':
                return record['stockShareName']?.toString();
case 'couponTypeId':
                return record['couponTypeName']?.toString();
case 'couponStatusId':
                return record['couponStatusName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getCouponActions(component: any, record: ICoupon): IAction[] {
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
export function isVisible(entity: ICoupon, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

