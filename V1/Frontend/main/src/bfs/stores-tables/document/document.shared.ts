
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DocumentColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Doc No.', sortName: 'Name', width: '50px', isVisible:false },
{ fieldName: 'storeId', displayName: 'Store', sortName: 'StoreName', width: '50px', isVisible:true },
{ fieldName: 'operationId', displayName: 'Operation', sortName: 'OperationName', width: '50px', isVisible:true },
{ fieldName: 'responseDate', displayName: 'Response Date', sortName: 'ResponseDate', width: '50px', isVisible:false },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IDocument {
    isDeleted?: boolean;
id?: string;
name?: string;
responseDate?: Date | null;
notes?: string;

    storeId?: string;
operationId?: number;

}
//---------------------------------------------------------
export function initDocument(): IDocument {
    let entity: IDocument = {
        isDeleted: false,
id: '0',
name: '',
responseDate: null,
notes: '',

        storeId: '0',
operationId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function documentUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
responseDate: [null],
notes: [''],

    storeId: ['0'],
operationId: [0],

    };
} 
//---------------------------------------------------------
export interface IDocumentWithLookup extends IDocument{

    storeName?: string;
operationName?: string;

}
//---------------------------------------------------------
export interface IDocumentRequest extends IEntityRequest<IDocumentFilter> {}

//---------------------------------------------------------
export interface IDocumentFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

    StoreId?: string;
OperationId?: number;

    ResponseDate?: { from?: Date | null ; to?: Date | null} ;

}
//---------------------------------------------------------
export function initDocumentRequest(): IDocumentRequest {
    let request: IDocumentRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: DocumentColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            Name: undefined ,

            StoreId: undefined ,
OperationId: undefined ,

            ResponseDate: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getDocumentActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('document', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/document/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('document', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/document/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('document', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/document/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('document', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/document/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('document', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['storeId'], route:'/str/store/view', displayText:'Go to Store'
});
}
if (component.accessService.isActionAllowed('document', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['operationId'], route:'/str/operation/view', displayText:'Go to Operation'
});
}

        return links;
    }
    //---------------------------------------------------------

