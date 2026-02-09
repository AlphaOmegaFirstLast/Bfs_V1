import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsClientListComponent } from './bfs-client.list.component';
import { BfsClientFormComponent } from './bfs-client.form.component';

// Example role, api, and app
export const BfsClient_ROUTES: Routes = [
    {
        path: 'bfs/bfs-client/list', 
        component: BfsClientListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-client/list/:id', 
        component: BfsClientListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-client/add/0', 
        component: BfsClientFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-client/view/:id', 
        component: BfsClientFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-client/edit/:id',
        component: BfsClientFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-client/delete/:id', 
        component: BfsClientFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    }
]