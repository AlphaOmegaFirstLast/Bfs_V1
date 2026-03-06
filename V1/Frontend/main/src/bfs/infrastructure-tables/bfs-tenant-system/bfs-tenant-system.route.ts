import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsTenantSystemListComponent } from './bfs-tenant-system.list.component';
import { BfsTenantSystemFormComponent } from './bfs-tenant-system.form.component';

// Example role, api, and app
export const BfsTenantSystem_ROUTES: Routes = [
    {
        path: 'bfs/bfs-tenant-system/list', 
        component: BfsTenantSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant-system/list/:id', 
        component: BfsTenantSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant-system/add/0', 
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant-system/view/:id', 
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant-system/edit/:id',
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant-system/delete/:id', 
        component: BfsTenantSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    }
]
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

