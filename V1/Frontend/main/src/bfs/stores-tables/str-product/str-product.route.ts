import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StrProductListComponent } from './str-product.list.component';
import { StrProductFormComponent } from './str-product.form.component';

// Example role, api, and app
export const StrProduct_ROUTES: Routes = [
    {
        path: 'str/str-product/list', 
        component: StrProductListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-product/list/:id', 
        component: StrProductListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-product/add/0', 
        component: StrProductFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-product/view/:id', 
        component: StrProductFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-product/edit/:id',
        component: StrProductFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-product/delete/:id', 
        component: StrProductFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]