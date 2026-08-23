import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SsPortfolioBalanceListComponent } from './ss-portfolio-balance.list.component';
import { SsPortfolioBalanceFormComponent } from './ss-portfolio-balance.form.component';

// Example role, api, and app
export const SsPortfolioBalance_ROUTES: Routes = [
    {
        path: 'stkx/ss-portfolio-balance/list', 
        component: SsPortfolioBalanceListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio-balance/list/:id', 
        component: SsPortfolioBalanceListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio-balance/add/0', 
        component: SsPortfolioBalanceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio-balance/view/:id', 
        component: SsPortfolioBalanceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio-balance/edit/:id',
        component: SsPortfolioBalanceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio-balance/delete/:id', 
        component: SsPortfolioBalanceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

