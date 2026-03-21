import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AuthAppListComponent } from './auth-app.list.component';
import { AuthAppFormComponent } from './auth-app.form.component';

// Example role, api, and app
export const AuthApp_ROUTES: Routes = [
    {
        path: 'bfs/auth-app/list', 
        component: AuthAppListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-app/list/:id', 
        component: AuthAppListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-app/add/0', 
        component: AuthAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-app/view/:id', 
        component: AuthAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-app/edit/:id',
        component: AuthAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/auth-app/delete/:id', 
        component: AuthAppFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]