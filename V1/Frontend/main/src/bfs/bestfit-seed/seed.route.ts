import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const Seed_ROUTES: Routes = [ 
    {
        path: '',
        loadChildren: () => import('./action-type/action-type.route').then((mod) => mod.ActionType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./data-type/data-type.route').then((mod) => mod.DataType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./system-template/system-template.route').then((mod) => mod.SystemTemplate_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./filter-type/filter-type.route').then((mod) => mod.FilterType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./form-control-type/form-control-type.route').then((mod) => mod.FormControlType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./component-type/component-type.route').then((mod) => mod.ComponentType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./backend-data-type/backend-data-type.route').then((mod) => mod.BackendDataType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./aggregate-type/aggregate-type.route').then((mod) => mod.AggregateType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./chart-element/chart-element.route').then((mod) => mod.ChartElement_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./action-location/action-location.route').then((mod) => mod.ActionLocation_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./system-action/system-action.route').then((mod) => mod.SystemAction_ROUTES),
    },

//Template_Component_RegisterRoute
]