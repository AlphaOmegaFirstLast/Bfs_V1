import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CashTransactionListComponent } from './cash-transaction.list.component';
import { CashTransactionFormComponent } from './cash-transaction.form.component';

// Example role, api, and app
export const CashTransaction_ROUTES: Routes = [
    {
        path: 'stkx/cash-transaction/list', 
        component: CashTransactionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/cash-transaction/list/:id', 
        component: CashTransactionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/cash-transaction/add/0', 
        component: CashTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/cash-transaction/view/:id', 
        component: CashTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/cash-transaction/edit/:id',
        component: CashTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/cash-transaction/delete/:id', 
        component: CashTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

