
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const BackendDataTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IBackendDataType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initBackendDataType(): IBackendDataType {
    let entity: IBackendDataType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function backendDataTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IBackendDataTypeWithLookup extends IBackendDataType{

}
//---------------------------------------------------------
export interface IBackendDataTypeRequest extends IEntityRequest<IBackendDataTypeFilter> {}

//---------------------------------------------------------
export interface IBackendDataTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initBackendDataTypeRequest(): IBackendDataTypeRequest {
    let request: IBackendDataTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: BackendDataTypeColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getBackendDataTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

