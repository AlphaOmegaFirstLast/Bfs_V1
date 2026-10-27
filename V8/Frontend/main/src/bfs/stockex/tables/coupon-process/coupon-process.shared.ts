import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';
import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { reOrderColumns } from "@bfs/_shared/helpers/misc.helper"

import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stockex/main/stockex.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

export const fields = {
        isDeleted:  { visible: true, enabled: true },
id:  { visible: true, enabled: true },
notes:  { visible: true, enabled: true },
processDate:  { visible: true, enabled: true },

        couponId:  { visible: true, enabled: true },
processStatusId:  { visible: true, enabled: true },

};

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'couponId', displayName: 'Coupon', sortName: 'CouponName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'processDate', displayName: 'Process Date', sortName: 'ProcessDate', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'processStatusId', displayName: 'Process Status', sortName: 'ProcessStatusName', width: '50px', isVisible:true, columnOrder:1 },

];
export const CouponProcessColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface ICouponProcess extends IEntity{
    isDeleted?: boolean;
id?: string;
notes?: string;
processDate?: Date | null;

    couponId?: string;
processStatusId?: string;

}
//---------------------------------------------------------
export function initCouponProcess(): ICouponProcess {
    let entity: ICouponProcess = {
        isDeleted: false,
id: '0',
notes: '',
processDate: new Date(),

        couponId: '0',
processStatusId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function couponProcessUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"0","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
processDate: [new Date(),getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    couponId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
processStatusId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":null,"MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface ICouponProcessRequest extends IEntityRequest<ICouponProcessFilter> {}

//---------------------------------------------------------
export interface ICouponProcessFilter {
    [key: string]: any;
    Id?: string;

    CouponId?: string;
ProcessStatusId?: string;

    ProcessDate?: { from?: Date | null ; to?: Date | null} ;

}
//---------------------------------------------------------
export function initCouponProcessRequest(): ICouponProcessRequest {
    let request: ICouponProcessRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CouponProcessColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            CouponId: undefined ,
ProcessStatusId: undefined ,

            ProcessDate: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderCouponProcess(record: ICouponProcess, column: IQueryColumn, datePipe?: DatePipe, sanitizer?: DomSanitizer): SafeHtml {
        const value = record[column.fieldName as keyof ICouponProcess];
        switch (column.fieldName) {
            case 'couponId':
                return record['couponName']?.toString();
case 'processStatusId':
                return record['processStatusName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getCouponProcessActions(component: any, record: ICouponProcess): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('couponProcess', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListHeader',recordId:0, action: operations.couponProcess, displayText: 'Process', data: { recordId:0,  postUrl: '/Operations/Coupon/Process'}
});
}

        return links;
    }
//---------------------------------------------------------

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

