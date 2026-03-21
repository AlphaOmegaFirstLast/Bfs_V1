import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StrCurrencyListComponent } from './str-currency.list.component';
import { StrCurrencyFormComponent } from './str-currency.form.component';

// Example role, api, and app
export const StrCurrency_ROUTES: Routes = [
    {
        path: 'str/str-currency/list', 
        component: StrCurrencyListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-currency/list/:id', 
        component: StrCurrencyListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-currency/add/0', 
        component: StrCurrencyFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-currency/view/:id', 
        component: StrCurrencyFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-currency/edit/:id',
        component: StrCurrencyFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-currency/delete/:id', 
        component: StrCurrencyFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]