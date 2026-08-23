import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { TransferCostTypeListComponent } from './transfer-cost-type.list.component';
import { TransferCostTypeFormComponent } from './transfer-cost-type.form.component';

// Example role, api, and app
export const TransferCostType_ROUTES: Routes = [
    {
        path: 'stkx/transfer-cost-type/list', 
        component: TransferCostTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transfer-cost-type/list/:id', 
        component: TransferCostTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transfer-cost-type/add/0', 
        component: TransferCostTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transfer-cost-type/view/:id', 
        component: TransferCostTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transfer-cost-type/edit/:id',
        component: TransferCostTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/transfer-cost-type/delete/:id', 
        component: TransferCostTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

