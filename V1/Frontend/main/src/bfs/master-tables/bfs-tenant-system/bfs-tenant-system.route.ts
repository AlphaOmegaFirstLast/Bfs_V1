import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsTenantSystemListComponent } from './bfs-tenant-system.list.component';
import { BfsTenantSystemFormComponent } from './bfs-tenant-system.form.component';

// Example role, api, and app
export const BfsTenantSystem_ROUTES: Routes = [
    {
        path: 'mstr/bfs-tenant-system/list', 
        component: BfsTenantSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant-system/list/:id', 
        component: BfsTenantSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant-system/add/0', 
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant-system/view/:id', 
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant-system/edit/:id',
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-tenant-system/delete/:id', 
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

