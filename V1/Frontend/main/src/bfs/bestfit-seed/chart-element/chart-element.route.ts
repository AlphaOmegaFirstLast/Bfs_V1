import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ChartElementListComponent } from './chart-element.list.component';
import { ChartElementFormComponent } from './chart-element.form.component';

// Example role, api, and app
export const ChartElement_ROUTES: Routes = [
    {
        path: 'chart-element/list', 
        component: ChartElementListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'chart-element/list/:id', 
        component: ChartElementListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'chart-element/add/0', 
        component: ChartElementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'chart-element/view/:id', 
        component: ChartElementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'chart-element/edit/:id',
        component: ChartElementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'chart-element/delete/:id', 
        component: ChartElementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]