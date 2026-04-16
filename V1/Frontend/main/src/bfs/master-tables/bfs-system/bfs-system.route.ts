import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsSystemListComponent } from './bfs-system.list.component';
import { BfsSystemFormComponent } from './bfs-system.form.component';

// Example role, api, and app
export const BfsSystem_ROUTES: Routes = [
    {
        path: 'mstr/bfs-system/list', 
        component: BfsSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-system/list/:id', 
        component: BfsSystemListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-system/add/0', 
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-system/view/:id', 
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-system/edit/:id',
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-system/delete/:id', 
        component: BfsSystemFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

