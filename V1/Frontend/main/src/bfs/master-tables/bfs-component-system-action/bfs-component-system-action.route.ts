import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsComponentSystemActionListComponent } from './bfs-component-system-action.list.component';
import { BfsComponentSystemActionFormComponent } from './bfs-component-system-action.form.component';

// Example role, api, and app
export const BfsComponentSystemAction_ROUTES: Routes = [
    {
        path: 'mstr/bfs-component-system-action/list', 
        component: BfsComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component-system-action/list/:id', 
        component: BfsComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component-system-action/add/0', 
        component: BfsComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component-system-action/view/:id', 
        component: BfsComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component-system-action/edit/:id',
        component: BfsComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-component-system-action/delete/:id', 
        component: BfsComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

