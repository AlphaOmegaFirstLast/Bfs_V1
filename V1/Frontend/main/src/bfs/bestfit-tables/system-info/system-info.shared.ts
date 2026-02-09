import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const SystemInfoColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:true },
{ fieldName: 'basePortNumber', displayName: 'Base Port Number', sortName:'BasePortNumber', width: '50px', isVisible:true },

    { fieldName: 'clientId', displayName: 'Client', sortName:'Client', width: '50px', isVisible:false },
{ fieldName: 'systemTemplateId', displayName: 'Template', sortName:'SystemTemplate', width: '50px', isVisible:false },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function systemInfoUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],
basePortNumber: [''],

    clientId: ['0'],
systemTemplateId: [0],

    };
} 
//---------------------------------------------------------

export interface ISystemInfo {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;
basePortNumber?: string;

    clientId?: string;
systemTemplateId?: number;

}
//---------------------------------------------------------
export interface ISystemInfoWithLookup extends ISystemInfo{

    client?: string;
systemTemplate?: string;

}
//---------------------------------------------------------

export function initSystemInfo(): ISystemInfo {
    let entity: ISystemInfo = {
        isDeleted: false,
id: '0',
name: '',
notes: '',
basePortNumber: '',

        clientId: '0',
systemTemplateId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface ISystemInfoRequest extends IEntityRequest<ISystemInfoFilter> {}

//---------------------------------------------------------
export interface ISystemInfoFilter {
    [key: string]: any;

    Name?: string;

    ClientId?: string;
SystemTemplateId?: number;

}
//---------------------------------------------------------
export function initSystemInfoRequest(): ISystemInfoRequest {
    let request: ISystemInfoRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: SystemInfoColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            ClientId: undefined ,
SystemTemplateId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

