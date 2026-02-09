import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BackendDataTypeListComponent } from './backend-data-type.list.component';
import { BackendDataTypeFormComponent } from './backend-data-type.form.component';

// Example role, api, and app
export const BackendDataType_ROUTES: Routes = [
    {
        path: 'backend-data-type/list', 
        component: BackendDataTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'backend-data-type/list/:id', 
        component: BackendDataTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'backend-data-type/add/0', 
        component: BackendDataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'backend-data-type/view/:id', 
        component: BackendDataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'backend-data-type/edit/:id',
        component: BackendDataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'backend-data-type/delete/:id', 
        component: BackendDataTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]