import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { OverdraftPortfolioListComponent } from './overdraft-portfolio.list.component';
import { OverdraftPortfolioFormComponent } from './overdraft-portfolio.form.component';

// Example role, api, and app
export const OverdraftPortfolio_ROUTES: Routes = [
    {
        path: 'stkx/overdraft-portfolio/list', 
        component: OverdraftPortfolioListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/overdraft-portfolio/list/:id', 
        component: OverdraftPortfolioListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/overdraft-portfolio/add/0', 
        component: OverdraftPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/overdraft-portfolio/view/:id', 
        component: OverdraftPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/overdraft-portfolio/edit/:id',
        component: OverdraftPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/overdraft-portfolio/delete/:id', 
        component: OverdraftPortfolioFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

