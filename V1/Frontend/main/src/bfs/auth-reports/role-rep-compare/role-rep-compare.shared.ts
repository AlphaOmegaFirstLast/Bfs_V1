import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/auth-main/auth.operations';

// Output Columns of a Query  [used in entity Query]
export const RoleRepCompareColumns = [
    { fieldName: 'authRole_Id', displayName: 'ID', sortName: 'AuthRole_Id', width: '50px', isVisible:false },
{ fieldName: 'authRole_Name', displayName: 'Name', sortName: 'AuthRole_Name', width: '50px', isVisible:true },
{ fieldName: 'authRole_Notes', displayName: 'Notes', sortName: 'AuthRole_Notes', width: '50px', isVisible:false },

];

//---------------------------------------------------------

export interface IRoleRepCompare {
    authRole_Id?: string;
authRole_Name?: string;
authRole_Notes?: string;

}
//---------------------------------------------------------
export interface IRoleRepCompareWithLookup extends IRoleRepCompare{

}
//---------------------------------------------------------
export interface IRoleRepCompareFilter {
    [key: string]: any;
    Id?: string;

    Name?: string;

}
//---------------------------------------------------------

export interface IRoleRepCompareRequest extends IEntityRequest<IRoleRepCompareFilter> {}

//---------------------------------------------------------
export function initRoleRepCompareRequest(): IRoleRepCompareRequest {
    let request: IRoleRepCompareRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: RoleRepCompareColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {},
        filter: {
            Id: undefined ,

            Name: undefined ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function getRoleRepCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

