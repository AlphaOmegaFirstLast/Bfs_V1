import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { AggregateTypeListComponent } from './aggregate-type.list.component';
import { AggregateTypeFormComponent } from './aggregate-type.form.component';

// Example role, api, and app
export const AggregateType_ROUTES: Routes = [
    {
        path: 'mstr/aggregate-type/list', 
        component: AggregateTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/aggregate-type/list/:id', 
        component: AggregateTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/aggregate-type/add/0', 
        component: AggregateTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/aggregate-type/view/:id', 
        component: AggregateTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/aggregate-type/edit/:id',
        component: AggregateTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/aggregate-type/delete/:id', 
        component: AggregateTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]