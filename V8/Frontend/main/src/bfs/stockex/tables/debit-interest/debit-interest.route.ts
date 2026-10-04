import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { DebitInterestListComponent } from './debit-interest.list.component';
import { DebitInterestFormComponent } from './debit-interest.form.component';

// Example role, api, and app
export const DebitInterest_ROUTES: Routes = [
    {
        path: 'stkx/debit-interest/list', 
        component: DebitInterestListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/debit-interest/list/:id', 
        component: DebitInterestListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/debit-interest/add/0', 
        component: DebitInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/debit-interest/view/:id', 
        component: DebitInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/debit-interest/edit/:id',
        component: DebitInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/debit-interest/delete/:id', 
        component: DebitInterestFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]