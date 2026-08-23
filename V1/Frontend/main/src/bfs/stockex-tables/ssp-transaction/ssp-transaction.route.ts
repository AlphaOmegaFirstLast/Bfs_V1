import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SspTransactionListComponent } from './ssp-transaction.list.component';
import { SspTransactionFormComponent } from './ssp-transaction.form.component';

// Example role, api, and app
export const SspTransaction_ROUTES: Routes = [
    {
        path: 'stkx/ssp-transaction/list', 
        component: SspTransactionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-transaction/list/:id', 
        component: SspTransactionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-transaction/add/0', 
        component: SspTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-transaction/view/:id', 
        component: SspTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-transaction/edit/:id',
        component: SspTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-transaction/delete/:id', 
        component: SspTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

