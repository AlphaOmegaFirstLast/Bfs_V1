import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsTenantListComponent } from './bfs-tenant.list.component';
import { BfsTenantFormComponent } from './bfs-tenant.form.component';

// Example role, api, and app
export const BfsTenant_ROUTES: Routes = [
    {
        path: 'bfs/bfs-tenant/list', 
        component: BfsTenantListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant/list/:id', 
        component: BfsTenantListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant/add/0', 
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant/view/:id', 
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant/edit/:id',
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/bfs-tenant/delete/:id', 
        component: BfsTenantFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    }
]
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2
//Template_Start_Code_DontOverwrite_3

//Template_End_Code_DontOverwrite_3

