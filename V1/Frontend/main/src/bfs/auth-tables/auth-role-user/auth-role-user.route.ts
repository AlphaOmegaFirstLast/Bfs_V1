import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AuthRoleUserListComponent } from './auth-role-user.list.component';
import { AuthRoleUserFormComponent } from './auth-role-user.form.component';

// Example role, api, and app
export const AuthRoleUser_ROUTES: Routes = [
    {
        path: 'bfs/auth-role-user/list', 
        component: AuthRoleUserListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-user/list/:id', 
        component: AuthRoleUserListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-user/add/0', 
        component: AuthRoleUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-user/view/:id', 
        component: AuthRoleUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-user/edit/:id',
        component: AuthRoleUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-role-user/delete/:id', 
        component: AuthRoleUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]