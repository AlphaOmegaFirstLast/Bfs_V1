import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { PaymentModeListComponent } from './payment-mode.list.component';
import { PaymentModeFormComponent } from './payment-mode.form.component';

// Example role, api, and app
export const PaymentMode_ROUTES: Routes = [
    {
        path: 'stkx/payment-mode/list', 
        component: PaymentModeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/payment-mode/list/:id', 
        component: PaymentModeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/payment-mode/add/0', 
        component: PaymentModeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/payment-mode/view/:id', 
        component: PaymentModeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/payment-mode/edit/:id',
        component: PaymentModeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/payment-mode/delete/:id', 
        component: PaymentModeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]