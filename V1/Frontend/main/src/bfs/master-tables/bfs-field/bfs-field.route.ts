import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsFieldListComponent } from './bfs-field.list.component';
import { BfsFieldFormComponent } from './bfs-field.form.component';

// Example role, api, and app
export const BfsField_ROUTES: Routes = [
    {
        path: 'mstr/bfs-field/list', 
        component: BfsFieldListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-field/list/:id', 
        component: BfsFieldListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-field/add/0', 
        component: BfsFieldFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-field/view/:id', 
        component: BfsFieldFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-field/edit/:id',
        component: BfsFieldFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-field/delete/:id', 
        component: BfsFieldFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

