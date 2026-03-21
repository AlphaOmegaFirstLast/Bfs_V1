import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StrTransactionListComponent } from './str-transaction.list.component';
import { StrTransactionFormComponent } from './str-transaction.form.component';

// Example role, api, and app
export const StrTransaction_ROUTES: Routes = [
    {
        path: 'str/str-transaction/list', 
        component: StrTransactionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-transaction/list/:id', 
        component: StrTransactionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-transaction/add/0', 
        component: StrTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-transaction/view/:id', 
        component: StrTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-transaction/edit/:id',
        component: StrTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-transaction/delete/:id', 
        component: StrTransactionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]