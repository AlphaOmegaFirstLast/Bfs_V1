import { IEntityRequest } from "@bfs/_shared/interfaces";
import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const CustomReportsColumns = [
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName:'Name', width: '50px', isVisible:true },
{ fieldName: 'createdBy', displayName: 'CreatedBy', sortName:'CreatedBy', width: '50px', isVisible:true },
{ fieldName: 'isPrivate', displayName: 'Is Private', sortName:'IsPrivate', width: '50px', isVisible:true },
];
//---------------------------------------------------------
// Fields of an Entity [used in Entity form]
export const customReportsUntypedFormGroup = {
    isDeleted: [false],
id: [],
name: [],
request: [],
createdBy: [],
isPrivate: [],
baseReport: [],
url: [],

} as any as UntypedFormGroup;

//---------------------------------------------------------

export interface ICustomReports {
    isDeleted?: boolean;
id?: string;
name?: string;
request?: string;
createdBy?: string;
isPrivate?: boolean;
baseReport?: string;
url?: string;    
}
//---------------------------------------------------------
export interface ICustomReportsWithLookup {
    isDeleted?: boolean;
id?: string;
name?: string;
request?: string;
createdBy?: string;
isPrivate?: boolean;
baseReport?: string;
url?: string;
    
}
//---------------------------------------------------------
export interface ICustomReportsFilter {
    [key: string]: any;
    id?: string;
name?: string;
createdBy?: string;
baseReport?: string;
url?: string;

    

    
   
    

    
}
//---------------------------------------------------------

export interface ICustomReportsRequest extends IEntityRequest<ICustomReportsFilter> {}

//---------------------------------------------------------
export function initCustomReports(): ICustomReports {
    let entity: ICustomReports = {
        isDeleted: false,
id: '',
name: '',
request: '',
createdBy: '',
isPrivate: false,
baseReport: '',
url: '',
    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export function initCustomReportsRequest(): ICustomReportsRequest {
    let request: ICustomReportsRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: CustomReportsColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            id: undefined ,
name: undefined ,
createdBy: undefined ,
baseReport: undefined ,
url: undefined ,
          
            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------



