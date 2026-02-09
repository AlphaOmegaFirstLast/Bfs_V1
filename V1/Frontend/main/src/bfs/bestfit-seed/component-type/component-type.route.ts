import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ComponentTypeListComponent } from './component-type.list.component';
import { ComponentTypeFormComponent } from './component-type.form.component';

// Example role, api, and app
export const ComponentType_ROUTES: Routes = [
    {
        path: 'component-type/list', 
        component: ComponentTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-type/list/:id', 
        component: ComponentTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-type/add/0', 
        component: ComponentTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-type/view/:id', 
        component: ComponentTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-type/edit/:id',
        component: ComponentTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-type/delete/:id', 
        component: ComponentTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]