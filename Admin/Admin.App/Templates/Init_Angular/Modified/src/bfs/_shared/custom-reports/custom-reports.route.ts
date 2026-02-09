import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CustomReportsListComponent } from './custom-reports-list.component';

// Example role, api, and app
export const CustomReportsList_ROUTES: Routes = [
    {
        path: 'custom-reports-list',
        component: CustomReportsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['stkex.b.ofc'] } 
    }
]