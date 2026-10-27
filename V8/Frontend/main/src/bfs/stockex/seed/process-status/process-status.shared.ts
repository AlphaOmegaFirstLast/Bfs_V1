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
name:  { visible: true, enabled: true },
notes:  { visible: true, enabled: true },

};

// Output Columns of a Query  [used in entity Query]
export const columns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false, columnOrder:1 },

];
export const ProcessStatusColumns  = reOrderColumns(columns);
//---------------------------------------------------------
export interface IProcessStatus extends IEntity{
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initProcessStatus(): IProcessStatus {
    let entity: IProcessStatus = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function processStatusUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
name: ['',getFormControlValidation('{"IsRequired":true,"MinLength":"3","MaxLength":"50","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
notes: ['',getFormControlValidation('{"IsRequired":false,"MinLength":"","MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IProcessStatusRequest extends IEntityRequest<IProcessStatusFilter> {}

//---------------------------------------------------------
export interface IProcessStatusFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------
export function initProcessStatusRequest(): IProcessStatusRequest {
    let request: IProcessStatusRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ProcessStatusColumns.map(column => ({ ...column })),
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
export function renderProcessStatus(record: IProcessStatus, column: IQueryColumn, datePipe?: DatePipe, sanitizer?: DomSanitizer): SafeHtml {
        const value = record[column.fieldName as keyof IProcessStatus];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getProcessStatusActions(component: any, record: IProcessStatus): IAction[] {
        let links: IAction[] = [];

        return links;
    }
//---------------------------------------------------------

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

