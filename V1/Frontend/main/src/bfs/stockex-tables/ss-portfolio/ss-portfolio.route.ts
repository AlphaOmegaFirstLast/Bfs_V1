import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SsPortfolioListComponent } from './ss-portfolio.list.component';
import { SsPortfolioFormComponent } from './ss-portfolio.form.component';

// Example role, api, and app
export const SsPortfolio_ROUTES: Routes = [
    {
        path: 'stkx/ss-portfolio/list', 
        component: SsPortfolioListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio/list/:id', 
        component: SsPortfolioListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio/add/0', 
        component: SsPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio/view/:id', 
        component: SsPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio/edit/:id',
        component: SsPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ss-portfolio/delete/:id', 
        component: SsPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

