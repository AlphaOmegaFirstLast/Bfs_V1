import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ComponentBusinessActionListComponent } from './component-business-action.list.component';
import { ComponentBusinessActionFormComponent } from './component-business-action.form.component';

// Example role, api, and app
export const ComponentBusinessAction_ROUTES: Routes = [
    {
        path: 'component-business-action/list', 
        component: ComponentBusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-business-action/list/:id', 
        component: ComponentBusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-business-action/add/0', 
        component: ComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-business-action/view/:id', 
        component: ComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-business-action/edit/:id',
        component: ComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'component-business-action/delete/:id', 
        component: ComponentBusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]