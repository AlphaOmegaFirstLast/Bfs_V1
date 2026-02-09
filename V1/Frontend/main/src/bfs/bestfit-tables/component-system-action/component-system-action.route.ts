import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ComponentSystemActionListComponent } from './component-system-action.list.component';
import { ComponentSystemActionFormComponent } from './component-system-action.form.component';

// Example role, api, and app
export const ComponentSystemAction_ROUTES: Routes = [
    {
        path: 'component-system-action/list', 
        component: ComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-system-action/list/:id', 
        component: ComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-system-action/add/0', 
        component: ComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-system-action/view/:id', 
        component: ComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-system-action/edit/:id',
        component: ComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-system-action/delete/:id', 
        component: ComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]