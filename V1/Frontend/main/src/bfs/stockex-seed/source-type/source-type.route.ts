import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SourceTypeListComponent } from './source-type.list.component';
import { SourceTypeFormComponent } from './source-type.form.component';

// Example role, api, and app
export const SourceType_ROUTES: Routes = [
    {
        path: 'stkx/source-type/list', 
        component: SourceTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/source-type/list/:id', 
        component: SourceTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/source-type/add/0', 
        component: SourceTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/source-type/view/:id', 
        component: SourceTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/source-type/edit/:id',
        component: SourceTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/source-type/delete/:id', 
        component: SourceTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

