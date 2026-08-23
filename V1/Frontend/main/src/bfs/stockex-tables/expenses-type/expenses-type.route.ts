import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ExpensesTypeListComponent } from './expenses-type.list.component';
import { ExpensesTypeFormComponent } from './expenses-type.form.component';

// Example role, api, and app
export const ExpensesType_ROUTES: Routes = [
    {
        path: 'stkx/expenses-type/list', 
        component: ExpensesTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/expenses-type/list/:id', 
        component: ExpensesTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/expenses-type/add/0', 
        component: ExpensesTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/expenses-type/view/:id', 
        component: ExpensesTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/expenses-type/edit/:id',
        component: ExpensesTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/expenses-type/delete/:id', 
        component: ExpensesTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

