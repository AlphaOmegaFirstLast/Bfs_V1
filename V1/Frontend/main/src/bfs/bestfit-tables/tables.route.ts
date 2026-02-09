import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./custom-field-definition/custom-field-definition.route').then((mod) => mod.CustomFieldDefinition_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./component/component.route').then((mod) => mod.Component_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./table-field/table-field.route').then((mod) => mod.TableField_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./system-info/system-info.route').then((mod) => mod.SystemInfo_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./client/client.route').then((mod) => mod.Client_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./custom-reports/custom-reports.route').then((mod) => mod.CustomReports_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./business-action/business-action.route').then((mod) => mod.BusinessAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./component-system-action/component-system-action.route').then((mod) => mod.ComponentSystemAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./component-business-action/component-business-action.route').then((mod) => mod.ComponentBusinessAction_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./deployment-local/deployment-local.route').then((mod) => mod.DeploymentLocal_ROUTES),
    },

//Template_Component_RegisterRoute
]