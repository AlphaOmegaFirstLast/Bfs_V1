import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CustomReportsListComponent } from './custom-reports.list.component';
import { CustomReportsFormComponent } from './custom-reports.form.component';

// Example role, api, and app
export const CustomReports_ROUTES: Routes = [
    {
        path: 'custom-reports/list', 
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-reports/list/:id', 
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-reports/add/0', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-reports/view/:id', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-reports/edit/:id',
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-reports/delete/:id', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]