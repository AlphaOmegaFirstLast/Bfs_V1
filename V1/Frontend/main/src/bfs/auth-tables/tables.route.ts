import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./auth-role-component-system-action/auth-role-component-system-action.route').then((mod) => mod.AuthRoleComponentSystemAction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./auth-user/auth-user.route').then((mod) => mod.AuthUser_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./auth-app/auth-app.route').then((mod) => mod.AuthApp_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./auth-role/auth-role.route').then((mod) => mod.AuthRole_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./auth-role-app/auth-role-app.route').then((mod) => mod.AuthRoleApp_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./auth-role-user/auth-role-user.route').then((mod) => mod.AuthRoleUser_ROUTES),
    },

//Template_Component_RegisterRoute
]