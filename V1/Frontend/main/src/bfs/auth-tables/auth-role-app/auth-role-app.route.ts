import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AuthRoleAppListComponent } from './auth-role-app.list.component';
import { AuthRoleAppFormComponent } from './auth-role-app.form.component';

// Example role, api, and app
export const AuthRoleApp_ROUTES: Routes = [
    {
        path: 'bfs/auth-role-app/list', 
        component: AuthRoleAppListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-app/list/:id', 
        component: AuthRoleAppListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-app/add/0', 
        component: AuthRoleAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-app/view/:id', 
        component: AuthRoleAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-app/edit/:id',
        component: AuthRoleAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-app/delete/:id', 
        component: AuthRoleAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]