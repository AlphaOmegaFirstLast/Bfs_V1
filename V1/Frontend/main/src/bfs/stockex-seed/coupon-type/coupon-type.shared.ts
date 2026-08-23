
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex-main/stockex.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CouponTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface ICouponType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initCouponType(): ICouponType {
    let entity: ICouponType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function couponTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface ICouponTypeWithLookup extends ICouponType{

}
//---------------------------------------------------------
export interface ICouponTypeRequest extends IEntityRequest<ICouponTypeFilter> {}

//---------------------------------------------------------
export interface ICouponTypeFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initCouponTypeRequest(): ICouponTypeRequest {
    let request: ICouponTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CouponTypeColumns.map(column => ({ ...column })),
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

export function getCouponTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('couponType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/stkx/coupon-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('couponType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/coupon-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('couponType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/coupon-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('couponType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/stkx/coupon-type/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

