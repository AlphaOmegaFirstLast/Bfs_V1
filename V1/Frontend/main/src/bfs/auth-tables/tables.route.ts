import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./role-component-system-action/role-component-system-action.route').then((mod) => mod.RoleComponentSystemAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./user/user.route').then((mod) => mod.User_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./app/app.route').then((mod) => mod.App_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./role/role.route').then((mod) => mod.Role_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./role-app/role-app.route').then((mod) => mod.RoleApp_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./role-user/role-user.route').then((mod) => mod.RoleUser_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./user-request/user-request.route').then((mod) => mod.UserRequest_ROUTES),
    },

//Template_Component_RegisterRoute
]