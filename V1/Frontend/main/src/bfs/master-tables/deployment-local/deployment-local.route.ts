import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { DeploymentLocalListComponent } from './deployment-local.list.component';
import { DeploymentLocalFormComponent } from './deployment-local.form.component';

// Example role, api, and app
export const DeploymentLocal_ROUTES: Routes = [
    {
        path: 'mstr/deployment-local/list', 
        component: DeploymentLocalListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/deployment-local/list/:id', 
        component: DeploymentLocalListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/deployment-local/add/0', 
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/deployment-local/view/:id', 
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/deployment-local/edit/:id',
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/deployment-local/delete/:id', 
        component: DeploymentLocalFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

