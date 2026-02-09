import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { DataTypeListComponent } from './data-type.list.component';
import { DataTypeFormComponent } from './data-type.form.component';

// Example role, api, and app
export const DataType_ROUTES: Routes = [
    {
        path: 'data-type/list', 
        component: DataTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'data-type/list/:id', 
        component: DataTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'data-type/add/0', 
        component: DataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'data-type/view/:id', 
        component: DataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'data-type/edit/:id',
        component: DataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'data-type/delete/:id', 
        component: DataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]