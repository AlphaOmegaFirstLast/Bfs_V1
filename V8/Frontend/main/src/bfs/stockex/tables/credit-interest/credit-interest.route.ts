import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CreditInterestListComponent } from './credit-interest.list.component';
import { CreditInterestFormComponent } from './credit-interest.form.component';

// Example role, api, and app
export const CreditInterest_ROUTES: Routes = [
    {
        path: 'stkx/credit-interest/list', 
        component: CreditInterestListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/credit-interest/list/:id', 
        component: CreditInterestListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/credit-interest/add/0', 
        component: CreditInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/credit-interest/view/:id', 
        component: CreditInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/credit-interest/edit/:id',
        component: CreditInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/credit-interest/delete/:id', 
        component: CreditInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]