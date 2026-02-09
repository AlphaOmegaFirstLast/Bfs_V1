import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { DeploymentLocalListComponent } from './deployment-local.list.component';
import { DeploymentLocalFormComponent } from './deployment-local.form.component';

// Example role, api, and app
export const DeploymentLocal_ROUTES: Routes = [
    {
        path: 'deployment-local/list', 
        component: DeploymentLocalListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'deployment-local/list/:id', 
        component: DeploymentLocalListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'deployment-local/add/0', 
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'deployment-local/view/:id', 
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'deployment-local/edit/:id',
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'deployment-local/delete/:id', 
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]