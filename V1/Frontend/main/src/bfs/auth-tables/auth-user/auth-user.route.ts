import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AuthUserListComponent } from './auth-user.list.component';
import { AuthUserFormComponent } from './auth-user.form.component';

// Example role, api, and app
export const AuthUser_ROUTES: Routes = [
    {
        path: 'bfs/auth-user/list', 
        component: AuthUserListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-user/list/:id', 
        component: AuthUserListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-user/add/0', 
        component: AuthUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-user/view/:id', 
        component: AuthUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-user/edit/:id',
        component: AuthUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-user/delete/:id', 
        component: AuthUserFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

