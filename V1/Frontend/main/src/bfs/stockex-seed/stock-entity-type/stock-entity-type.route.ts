import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StockEntityTypeListComponent } from './stock-entity-type.list.component';
import { StockEntityTypeFormComponent } from './stock-entity-type.form.component';

// Example role, api, and app
export const StockEntityType_ROUTES: Routes = [
    {
        path: 'stkx/stock-entity-type/list', 
        component: StockEntityTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-entity-type/list/:id', 
        component: StockEntityTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-entity-type/add/0', 
        component: StockEntityTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-entity-type/view/:id', 
        component: StockEntityTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-entity-type/edit/:id',
        component: StockEntityTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-entity-type/delete/:id', 
        component: StockEntityTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

