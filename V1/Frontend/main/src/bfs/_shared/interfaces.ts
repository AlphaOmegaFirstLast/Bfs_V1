import { Router } from '@angular/router';
import { ICustomField } from '@bfs/_shared/customFields';

type Primitive = string | number | boolean | Date;
type Range = { from?: Primitive; to?: Primitive };
type FilterObject = Record<string, Primitive | Range | undefined>;
//------------------------------------------------
export interface TokenParsed {
  userId: string;
  exp: number; // e.g. offsetSeconds from Date.now()/1000
  roleId: string[];
  app: string[];
  method: string[];
}
//------------------------------------------------
export interface TokenModel {
  token: string | null;
  tokenParsed: TokenParsed | null;
}
//------------------------------------------------
export interface IUserInterface {
    isLoading?: boolean;
    apiService?: any;
    router: Router;
    messages: IUIMessage[];

    [key: string]: any; //index signature technique to go around compiler errors. calling any method in the underlying object by methodName as string. like this["getReport"]()
}
//------------------------------------------------
export interface IUIMessage {
    text?: string;
    msgType: 'info' | 'warning' | 'danger';
}
//------------------------------------------------
export interface IAction {
    recordId?: string | number;
    displayText: string;
    route?: string;
    action?: any;
    data?: any;
    actionType: string;
    actionLocation: string;
    actionSource: string;
}
//------------------------------------------------
export interface ViewLink {
    recordId?: string | number;
    displayText: string;
    route: string;
}
//------------------------------------------------
export interface ActionLink {
    recordId?: string | number;
    displayText: string;
    action: any;
    data?: any;
}
//------------------------------------------------
export interface IColumns {
    fieldName: string;
    displayName: string;
    sortName: string;
    width: string;
    isVisible: boolean;
}
//------------------------------------------------
export interface IQueryColumn {
    [key: string]: any; // Allow additional properties
    fieldName: string;
    displayName: string;
    sortName: string;
    width: string;
    isVisible: boolean;
}
//------------------------------------------------
export interface ISort {
    sortBy?: string;
    direction?: 'asc' | 'desc';
}
//------------------------------------------------

export interface IEntityRequest<TFilter> {
    pageIndex: number;
    pageSize: number;
    filter?: TFilter;
    sortOption?: ISort;
    group: string;
    columns: IColumns[];
}

//------------------------------------------------
export interface IQueryResponse {
    items: any[];
    totalItems: number;
    totalPages: number;
}
//------------------------------------------------
export interface ILookup {
    id: number;
    name: string;
}
//------------------------------------------------
export interface IIdentifiable {
    id: number;
}
//------------------------------------------------
export interface IEntity {
    [key: string]: any; // Allow additional properties
    id?: string;
    customFields?: ICustomField[];
}
//------------------------------------------------

export interface ICustomReports {
    id?: string;
name?: string;
request?: string;
baseReport?: string;
isPrivate?: boolean;
isDeleted?: boolean;
createdBy?: string;
url?: string;

}
//------------------------------------------------

export interface ICustomFieldDefinitionRecord
{
    id?: string;
    name?: string;
    displayName?: string;
    bfsComponentId?: string;
    fieldValidation?: string;
    bfsComponentName?: string;
}
//------------------------------------------------
export function formatFilter(filter?: FilterObject): string[] {
    const result: string[] = [];
    if (filter) {
        for (const [key, value] of Object.entries(filter)) {
            if (value == null) continue;

            if (typeof value === 'object' && ('from' in value || 'to' in value)) {
                const from = value.from != null ? formatPrimitive(value.from) : null;
                const to = value.to != null ? formatPrimitive(value.to) : null;

                if (from || to) {
                    result.push(`${toReadableLabel(key)}: ${[from, to].filter(Boolean).join(' - ')}`);
                }
            } else {
                result.push(`${toReadableLabel(key)}: ${formatPrimitive(value as Primitive)}`);
            }
        }
    }
    return result;
}
//------------------------------------------------
function formatPrimitive(value: Primitive): string {
    return value instanceof Date ? value.toLocaleDateString() : String(value);
}
//------------------------------------------------
function toReadableLabel(key: string): string {
    return key
        .replace(/([A-Z])/g, ' $1') // Add space before capital letters
        .replace(/Id$/, ' ID')       // Expand common suffix
        .replace(/\b\w/g, char => char.toUpperCase()); // Capitalize words
}
//------------------------------------------------