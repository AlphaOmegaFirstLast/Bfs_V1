import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CustomReportsListComponent } from './custom-reports.list.component';
import { CustomReportsFormComponent } from './custom-reports.form.component';

// Example role, api, and app
export const CustomReports_ROUTES: Routes = [
    {
        path: 'mstr/custom-reports/list', 
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/custom-reports/list/:id', 
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/custom-reports/add/0', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/custom-reports/view/:id', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/custom-reports/edit/:id',
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/custom-reports/delete/:id', 
        component: CustomReportsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

