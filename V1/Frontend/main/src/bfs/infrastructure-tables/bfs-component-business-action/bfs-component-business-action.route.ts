import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsComponentBusinessActionListComponent } from './bfs-component-business-action.list.component';
import { BfsComponentBusinessActionFormComponent } from './bfs-component-business-action.form.component';

// Example role, api, and app
export const BfsComponentBusinessAction_ROUTES: Routes = [
    {
        path: 'bfs/bfs-component-business-action/list', 
        component: BfsComponentBusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-component-business-action/list/:id', 
        component: BfsComponentBusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-component-business-action/add/0', 
        component: BfsComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-component-business-action/view/:id', 
        component: BfsComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-component-business-action/edit/:id',
        component: BfsComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/bfs-component-business-action/delete/:id', 
        component: BfsComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    }
]