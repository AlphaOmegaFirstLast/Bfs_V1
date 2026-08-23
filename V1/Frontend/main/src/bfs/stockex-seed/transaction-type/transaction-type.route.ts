import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { TransactionTypeListComponent } from './transaction-type.list.component';
import { TransactionTypeFormComponent } from './transaction-type.form.component';

// Example role, api, and app
export const TransactionType_ROUTES: Routes = [
    {
        path: 'stkx/transaction-type/list', 
        component: TransactionTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transaction-type/list/:id', 
        component: TransactionTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transaction-type/add/0', 
        component: TransactionTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transaction-type/view/:id', 
        component: TransactionTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transaction-type/edit/:id',
        component: TransactionTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transaction-type/delete/:id', 
        component: TransactionTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

