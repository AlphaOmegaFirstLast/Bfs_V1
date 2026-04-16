import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ActionLocationListComponent } from './action-location.list.component';
import { ActionLocationFormComponent } from './action-location.form.component';

// Example role, api, and app
export const ActionLocation_ROUTES: Routes = [
    {
        path: 'mstr/action-location/list', 
        component: ActionLocationListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/action-location/list/:id', 
        component: ActionLocationListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/action-location/add/0', 
        component: ActionLocationFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/action-location/view/:id', 
        component: ActionLocationFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/action-location/edit/:id',
        component: ActionLocationFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/action-location/delete/:id', 
        component: ActionLocationFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

