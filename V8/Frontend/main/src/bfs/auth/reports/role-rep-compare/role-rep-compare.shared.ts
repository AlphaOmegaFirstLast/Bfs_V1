import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import * as operations from '@bfs/auth/main/auth.operations';

// Output Columns of a Query  [used in entity Query]
export const RoleRepCompareColumns = [
    { fieldName: 'role_Id', displayName: 'ID', sortName: 'Role_Id', width: '50px', isVisible:false },
{ fieldName: 'role_Name', displayName: 'Name', sortName: 'Role_Name', width: '50px', isVisible:true },
{ fieldName: 'role_Notes', displayName: 'Notes', sortName: 'Role_Notes', width: '50px', isVisible:false },

];

//---------------------------------------------------------

export interface IRoleRepCompare {
    [key: string]: any;
    role_Id?: string;
role_Name?: string;
role_Notes?: string;

}
//---------------------------------------------------------
export interface IRoleRepCompareFilter {
    [key: string]: any;
    Role_Id?: string;

    Role_Name?: string;

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
            Role_Id: undefined ,

            Role_Name: undefined ,

            }
    };
    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderRoleRepCompare(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getRoleRepCompareActions(component:any, record: IEntity): IAction[] {
        let links: IAction[] = [];

        return links;
    }
    //---------------------------------------------------------

