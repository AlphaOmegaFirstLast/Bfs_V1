import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { FormControlTypeListComponent } from './form-control-type.list.component';
import { FormControlTypeFormComponent } from './form-control-type.form.component';

// Example role, api, and app
export const FormControlType_ROUTES: Routes = [
    {
        path: 'bfs/form-control-type/list', 
        component: FormControlTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/form-control-type/list/:id', 
        component: FormControlTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/form-control-type/add/0', 
        component: FormControlTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/form-control-type/view/:id', 
        component: FormControlTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/form-control-type/edit/:id',
        component: FormControlTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/form-control-type/delete/:id', 
        component: FormControlTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    }
]