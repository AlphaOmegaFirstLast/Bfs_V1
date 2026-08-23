import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StockFieldTypeListComponent } from './stock-field-type.list.component';
import { StockFieldTypeFormComponent } from './stock-field-type.form.component';

// Example role, api, and app
export const StockFieldType_ROUTES: Routes = [
    {
        path: 'stkx/stock-field-type/list', 
        component: StockFieldTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-field-type/list/:id', 
        component: StockFieldTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-field-type/add/0', 
        component: StockFieldTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-field-type/view/:id', 
        component: StockFieldTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-field-type/edit/:id',
        component: StockFieldTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/stock-field-type/delete/:id', 
        component: StockFieldTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

