import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { UserRequestStatusListComponent } from './user-request-status.list.component';
import { UserRequestStatusFormComponent } from './user-request-status.form.component';

// Example role, api, and app
export const UserRequestStatus_ROUTES: Routes = [
    {
        path: 'ath/user-request-status/list', 
        component: UserRequestStatusListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/user-request-status/list/:id', 
        component: UserRequestStatusListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/user-request-status/add/0', 
        component: UserRequestStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/user-request-status/view/:id', 
        component: UserRequestStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/user-request-status/edit/:id',
        component: UserRequestStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/user-request-status/delete/:id', 
        component: UserRequestStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]

