import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AuthRoleListComponent } from './auth-role.list.component';
import { AuthRoleFormComponent } from './auth-role.form.component';

// Example role, api, and app
export const AuthRole_ROUTES: Routes = [
    {
        path: 'bfs/auth-role/list', 
        component: AuthRoleListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role/list/:id', 
        component: AuthRoleListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role/add/0', 
        component: AuthRoleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role/view/:id', 
        component: AuthRoleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role/edit/:id',
        component: AuthRoleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role/delete/:id', 
        component: AuthRoleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]