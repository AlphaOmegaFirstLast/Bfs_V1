import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AuthRoleComponentSystemActionListComponent } from './auth-role-component-system-action.list.component';
import { AuthRoleComponentSystemActionFormComponent } from './auth-role-component-system-action.form.component';

// Example role, api, and app
export const AuthRoleComponentSystemAction_ROUTES: Routes = [
    {
        path: 'bfs/auth-role-component-system-action/list', 
        component: AuthRoleComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-component-system-action/list/:id', 
        component: AuthRoleComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-component-system-action/add/0', 
        component: AuthRoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-component-system-action/view/:id', 
        component: AuthRoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-component-system-action/edit/:id',
        component: AuthRoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-component-system-action/delete/:id', 
        component: AuthRoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]