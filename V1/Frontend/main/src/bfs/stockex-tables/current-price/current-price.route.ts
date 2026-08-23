import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CurrentPriceListComponent } from './current-price.list.component';
import { CurrentPriceFormComponent } from './current-price.form.component';

// Example role, api, and app
export const CurrentPrice_ROUTES: Routes = [
    {
        path: 'stkx/current-price/list', 
        component: CurrentPriceListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/current-price/list/:id', 
        component: CurrentPriceListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/current-price/add/0', 
        component: CurrentPriceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/current-price/view/:id', 
        component: CurrentPriceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/current-price/edit/:id',
        component: CurrentPriceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/current-price/delete/:id', 
        component: CurrentPriceFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

