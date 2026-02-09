import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsSystemListComponent } from './bfs-system.list.component';
import { BfsSystemFormComponent } from './bfs-system.form.component';

// Example role, api, and app
export const BfsSystem_ROUTES: Routes = [
    {
        path: 'bfs/bfs-system/list', 
        component: BfsSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-system/list/:id', 
        component: BfsSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-system/add/0', 
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-system/view/:id', 
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-system/edit/:id',
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-system/delete/:id', 
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    }
]