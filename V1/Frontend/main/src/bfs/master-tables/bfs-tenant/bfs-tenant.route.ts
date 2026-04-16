import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsTenantListComponent } from './bfs-tenant.list.component';
import { BfsTenantFormComponent } from './bfs-tenant.form.component';

// Example role, api, and app
export const BfsTenant_ROUTES: Routes = [
    {
        path: 'mstr/bfs-tenant/list', 
        component: BfsTenantListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant/list/:id', 
        component: BfsTenantListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant/add/0', 
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant/view/:id', 
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant/edit/:id',
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant/delete/:id', 
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

