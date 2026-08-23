import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CouponStatusListComponent } from './coupon-status.list.component';
import { CouponStatusFormComponent } from './coupon-status.form.component';

// Example role, api, and app
export const CouponStatus_ROUTES: Routes = [
    {
        path: 'stkx/coupon-status/list', 
        component: CouponStatusListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-status/list/:id', 
        component: CouponStatusListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-status/add/0', 
        component: CouponStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-status/view/:id', 
        component: CouponStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-status/edit/:id',
        component: CouponStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/coupon-status/delete/:id', 
        component: CouponStatusFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

