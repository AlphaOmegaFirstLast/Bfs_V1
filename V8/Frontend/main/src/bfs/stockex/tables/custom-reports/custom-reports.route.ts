import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CustomReportsListComponent } from './custom-reports.list.component';
import { CustomReportsFormComponent } from './custom-reports.form.component';

// Example role, api, and app
export const CustomReports_ROUTES: Routes = [
    {
        path: 'stkx/custom-reports/list', 
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/custom-reports/list/:id', 
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/custom-reports/add/0', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/custom-reports/view/:id', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/custom-reports/edit/:id',
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/custom-reports/delete/:id', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]