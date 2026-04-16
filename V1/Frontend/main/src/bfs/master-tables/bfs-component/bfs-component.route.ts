import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsComponentListComponent } from './bfs-component.list.component';
import { BfsComponentFormComponent } from './bfs-component.form.component';

// Example role, api, and app
export const BfsComponent_ROUTES: Routes = [
    {
        path: 'mstr/bfs-component/list', 
        component: BfsComponentListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component/list/:id', 
        component: BfsComponentListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component/add/0', 
        component: BfsComponentFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component/view/:id', 
        component: BfsComponentFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component/edit/:id',
        component: BfsComponentFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component/delete/:id', 
        component: BfsComponentFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

