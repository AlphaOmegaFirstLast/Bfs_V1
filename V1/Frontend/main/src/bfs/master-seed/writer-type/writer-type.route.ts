import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { WriterTypeListComponent } from './writer-type.list.component';
import { WriterTypeFormComponent } from './writer-type.form.component';

// Example role, api, and app
export const WriterType_ROUTES: Routes = [
    {
        path: 'mstr/writer-type/list', 
        component: WriterTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/writer-type/list/:id', 
        component: WriterTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/writer-type/add/0', 
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/writer-type/view/:id', 
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/writer-type/edit/:id',
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/writer-type/delete/:id', 
        component: WriterTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

