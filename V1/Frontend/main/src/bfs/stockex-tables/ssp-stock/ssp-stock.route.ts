import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SspStockListComponent } from './ssp-stock.list.component';
import { SspStockFormComponent } from './ssp-stock.form.component';

// Example role, api, and app
export const SspStock_ROUTES: Routes = [
    {
        path: 'stkx/ssp-stock/list', 
        component: SspStockListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-stock/list/:id', 
        component: SspStockListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-stock/add/0', 
        component: SspStockFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-stock/view/:id', 
        component: SspStockFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-stock/edit/:id',
        component: SspStockFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/ssp-stock/delete/:id', 
        component: SspStockFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

