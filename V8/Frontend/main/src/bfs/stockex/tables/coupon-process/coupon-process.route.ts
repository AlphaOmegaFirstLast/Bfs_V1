import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CouponProcessListComponent } from './coupon-process.list.component';
import { CouponProcessFormComponent } from './coupon-process.form.component';

// Example role, api, and app
export const CouponProcess_ROUTES: Routes = [
    {
        path: 'stkx/coupon-process/list', 
        component: CouponProcessListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-process/list/:id', 
        component: CouponProcessListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-process/add/0', 
        component: CouponProcessFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-process/view/:id', 
        component: CouponProcessFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-process/edit/:id',
        component: CouponProcessFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-process/delete/:id', 
        component: CouponProcessFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]