
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const StrOperationColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'name', displayName: 'Name', sortName: 'Name', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'Notes', width: '50px', isVisible:false },
{ fieldName: 'strEffectTypeId', displayName: 'Effect Type', sortName: 'StrEffectType', width: '50px', isVisible:true },
{ fieldName: 'strThirdPartyTypeId', displayName: 'Third Party Type', sortName: 'StrThirdPartyType', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IStrOperation {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

    strEffectTypeId?: number;
strThirdPartyTypeId?: number;

}
//---------------------------------------------------------
export function initStrOperation(): IStrOperation {
    let entity: IStrOperation = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

        strEffectTypeId: 0,
strThirdPartyTypeId: 0,

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function strOperationUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    strEffectTypeId: [0],
strThirdPartyTypeId: [0],

    };
} 
//---------------------------------------------------------
export interface IStrOperationWithLookup extends IStrOperation{

    strEffectTypeName?: string;
strThirdPartyTypeName?: string;

}
//---------------------------------------------------------
export interface IStrOperationRequest extends IEntityRequest<IStrOperationFilter> {}

//---------------------------------------------------------
export interface IStrOperationFilter {
    [key: string]: any;

    Name?: string;

    StrEffectTypeId?: number;
StrThirdPartyTypeId?: number;

}
//---------------------------------------------------------
export function initStrOperationRequest(): IStrOperationRequest {
    let request: IStrOperationRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: StrOperationColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {

            Name: undefined ,

            StrEffectTypeId: undefined ,
StrThirdPartyTypeId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getStrOperationActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/str-operation/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-operation/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-operation/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/str-operation/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['strEffectTypeId'], route:'/str/str-effect-type/view', displayText:'Go to StrEffectType'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['strThirdPartyTypeId'], route:'/str/str-third-party-type/view', displayText:'Go to StrThirdPartyType'
});

        return links;
    }
    //---------------------------------------------------------

