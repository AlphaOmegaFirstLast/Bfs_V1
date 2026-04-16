import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BusinessActionListComponent } from './business-action.list.component';
import { BusinessActionFormComponent } from './business-action.form.component';

// Example role, api, and app
export const BusinessAction_ROUTES: Routes = [
    {
        path: 'mstr/business-action/list', 
        component: BusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/business-action/list/:id', 
        component: BusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/business-action/add/0', 
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/business-action/view/:id', 
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/business-action/edit/:id',
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/business-action/delete/:id', 
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

