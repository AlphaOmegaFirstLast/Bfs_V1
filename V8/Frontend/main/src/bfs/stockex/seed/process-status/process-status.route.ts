import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ProcessStatusListComponent } from './process-status.list.component';
import { ProcessStatusFormComponent } from './process-status.form.component';

// Example role, api, and app
export const ProcessStatus_ROUTES: Routes = [
    {
        path: 'stkx/process-status/list', 
        component: ProcessStatusListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/process-status/list/:id', 
        component: ProcessStatusListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/process-status/add/0', 
        component: ProcessStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/process-status/view/:id', 
        component: ProcessStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/process-status/edit/:id',
        component: ProcessStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/process-status/delete/:id', 
        component: ProcessStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]