import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { WriterTypeListComponent } from './writer-type.list.component';
import { WriterTypeFormComponent } from './writer-type.form.component';

// Example role, api, and app
export const WriterType_ROUTES: Routes = [
    {
        path: 'bfs/writer-type/list', 
        component: WriterTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/writer-type/list/:id', 
        component: WriterTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/writer-type/add/0', 
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/writer-type/view/:id', 
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/writer-type/edit/:id',
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/writer-type/delete/:id', 
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    }
]