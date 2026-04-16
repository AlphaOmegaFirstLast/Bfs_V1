
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/stores-main/stores.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const EffectTypeColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'name', displayName: 'Name', sortName: 'NameName', width: '50px', isVisible:true },
{ fieldName: 'notes', displayName: 'Notes', sortName: 'NotesName', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IEffectType {
    isDeleted?: boolean;
id?: string;
name?: string;
notes?: string;

}
//---------------------------------------------------------
export function initEffectType(): IEffectType {
    let entity: IEffectType = {
        isDeleted: false,
id: '0',
name: '',
notes: '',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function effectTypeUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
name: [''],
notes: [''],

    };
} 
//---------------------------------------------------------
export interface IEffectTypeWithLookup extends IEffectType{

}
//---------------------------------------------------------
export interface IEffectTypeRequest extends IEntityRequest<IEffectTypeFilter> {}

//---------------------------------------------------------
export interface IEffectTypeFilter {
    [key: string]: any;

    Name?: string;

}
//---------------------------------------------------------
export function initEffectTypeRequest(): IEffectTypeRequest {
    let request: IEffectTypeRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: EffectTypeColumns.map(column => ({ ...column })),
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

export function getEffectTypeActions(component: any, record: IEntity): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('effectType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/str/effect-type/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('effectType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/effect-type/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('effectType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/effect-type/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('effectType', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/str/effect-type/delete', displayText: 'Delete...' 
});
}

        return links;
    }
    //---------------------------------------------------------

