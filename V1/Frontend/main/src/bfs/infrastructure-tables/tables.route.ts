import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./bfs-component/bfs-component.route').then((mod) => mod.BfsComponent_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-field/bfs-field.route').then((mod) => mod.BfsField_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-system/bfs-system.route').then((mod) => mod.BfsSystem_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-client/bfs-client.route').then((mod) => mod.BfsClient_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./custom-reports/custom-reports.route').then((mod) => mod.CustomReports_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./custom-field-definition/custom-field-definition.route').then((mod) => mod.CustomFieldDefinition_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./business-action/business-action.route').then((mod) => mod.BusinessAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-component-system-action/bfs-component-system-action.route').then((mod) => mod.BfsComponentSystemAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-component-business-action/bfs-component-business-action.route').then((mod) => mod.BfsComponentBusinessAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./deployment-local/deployment-local.route').then((mod) => mod.DeploymentLocal_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./system-action/system-action.route').then((mod) => mod.SystemAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./deployment-azure/deployment-azure.route').then((mod) => mod.DeploymentAzure_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-client-system/bfs-client-system.route').then((mod) => mod.BfsClientSystem_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-tenant/bfs-tenant.route').then((mod) => mod.BfsTenant_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./bfs-tenant-system/bfs-tenant-system.route').then((mod) => mod.BfsTenantSystem_ROUTES),
    },

//Template_Component_RegisterRoute
]