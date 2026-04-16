
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const ThirdPartyTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IThirdPartyType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initThirdPartyType(): IThirdPartyType {
    let entity: IThirdPartyType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function thirdPartyTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IThirdPartyTypeWithLookup extends IThirdPartyType{

}
//---------------------------------------------------------
export interface IThirdPartyTypeRequest extends IEntityRequest<IThirdPartyTypeFilter> {}

//---------------------------------------------------------
export interface IThirdPartyTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initThirdPartyTypeRequest(): IThirdPartyTypeRequest {
    let request: IThirdPartyTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: ThirdPartyTypeColumns.map(column => ({ ...column })),
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

export function getThirdPartyTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('thirdPartyType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/third-party-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('thirdPartyType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/third-party-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('thirdPartyType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/third-party-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('thirdPartyType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/third-party-type/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

