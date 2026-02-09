import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ComponentColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'isSoftDelete', displayName: 'Is Soft Delete', sortName:'IsSoftDelete', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'displayName', displayName: 'DisplayName', sortName:'DisplayName', width: '50px', isVisible:false },
{ fieldName: 'menuName', displayName: 'MenuName', sortName:'MenuName', width: '50px', isVisible:true },
{ fieldName: 'menuPlaceHolder', displayName: 'MenuPlaceHolder', sortName:'MenuPlaceHolder', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName:'Notes', width: '50px', isVisible:false },
{ fieldName: 'queryBaseTable', displayName: 'QueryBaseTable', sortName:'QueryBaseTable', width: '50px', isVisible:true },

    { fieldName: 'systemInfoId', displayName: 'System Info', sortName:'SystemInfo', width: '50px', isVisible:false },
{ fieldName: 'dataTypeId', displayName: 'Data Type', sortName:'DataType', width: '50px', isVisible:false },

];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export function componentUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
isSoftDelete: [false],
name: [''],
displayName: [''],
menuName: [''],
menuPlaceHolder: [''],
notes: [''],
queryBaseTable: [''],

    systemInfoId: ['0'],
dataTypeId: [0],

    };
} 
//---------------------------------------------------------

export interface IComponent {
    isDeleted?: boolean;
id?: string;
isSoftDelete?: boolean;
name?: string;
displayName?: string;
menuName?: string;
menuPlaceHolder?: string;
notes?: string;
queryBaseTable?: string;

    systemInfoId?: string;
dataTypeId?: number;

}
//---------------------------------------------------------
export interface IComponentWithLookup extends IComponent{

    systemInfo?: string;
dataType?: string;

}
//---------------------------------------------------------

export function initComponent(): IComponent {
    let entity: IComponent = {
        isDeleted: false,
id: '0',
isSoftDelete: false,
name: '',
displayName: '',
menuName: '',
menuPlaceHolder: '',
notes: '',
queryBaseTable: '',

        systemInfoId: '0',
dataTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IComponentRequest extends IEntityRequest<IComponentFilter> {}

//---------------------------------------------------------
export interface IComponentFilter {
    [key: string]: any;

    Name?: string;

    SystemInfoId?: string;
DataTypeId?: number;

}
//---------------------------------------------------------
export function initComponentRequest(): IComponentRequest {
    let request: IComponentRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ComponentColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            SystemInfoId: undefined ,
DataTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

