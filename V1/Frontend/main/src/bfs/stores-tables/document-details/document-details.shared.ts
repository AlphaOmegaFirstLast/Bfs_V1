
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DocumentDetailsColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'quantity', displayName: 'Quantity', sortName: 'Quantity', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'productId', displayName: 'Product', sortName: 'ProductName', width: '50px', isVisible:true },
{ fieldName: 'unitId', displayName: 'Unit', sortName: 'UnitName', width: '50px', isVisible:true },
{ fieldName: 'documentId', displayName: 'Document', sortName: 'DocumentName', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IDocumentDetails {
    isDeleted?: boolean;
id?: string;
quantity?: number;
notes?: string;

    productId?: string;
unitId?: number;
documentId?: string;

}
//---------------------------------------------------------
export function initDocumentDetails(): IDocumentDetails {
    let entity: IDocumentDetails = {
        isDeleted: false,
id: '0',
quantity: 0,
notes: '',

        productId: '0',
unitId: 0,
documentId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function documentDetailsUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
quantity: [0],
notes: [''],

    productId: ['0'],
unitId: [0],
documentId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IDocumentDetailsWithLookup extends IDocumentDetails{

    productName?: string;
unitName?: string;
documentName?: string;

}
//---------------------------------------------------------
export interface IDocumentDetailsRequest extends IEntityRequest<IDocumentDetailsFilter> {}

//---------------------------------------------------------
export interface IDocumentDetailsFilter {
    [key: string]: any;
    Id?: string;

    ProductId?: string;
UnitId?: number;
DocumentId?: string;

    Quantity?: { from?: number ; to?: number} ;

}
//---------------------------------------------------------
export function initDocumentDetailsRequest(): IDocumentDetailsRequest {
    let request: IDocumentDetailsRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: DocumentDetailsColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            ProductId: undefined ,
UnitId: undefined ,
DocumentId: undefined ,

            Quantity: { from: undefined , to: undefined} ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getDocumentDetailsActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/document-details/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/document-details/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/document-details/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/document-details/delete', displayText: 'Delete...' 
});
}
if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['productId'], route:'/str/product/view', displayText:'Go to Product'
});
}
if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['unitId'], route:'/str/unit/view', displayText:'Go to Unit'
});
}
if (component.accessService.isActionAllowed('documentDetails', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['documentId'], route:'/str/document/view', displayText:'Go to Document'
});
}

        return links;
    }
    //---------------------------------------------------------

