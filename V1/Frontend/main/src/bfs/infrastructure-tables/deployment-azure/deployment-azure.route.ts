import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { DeploymentAzureListComponent } from './deployment-azure.list.component';
import { DeploymentAzureFormComponent } from './deployment-azure.form.component';

// Example role, api, and app
export const DeploymentAzure_ROUTES: Routes = [
    {
        path: 'bfs/deployment-azure/list', 
        component: DeploymentAzureListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/deployment-azure/list/:id', 
        component: DeploymentAzureListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/deployment-azure/add/0', 
        component: DeploymentAzureFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/deployment-azure/view/:id', 
        component: DeploymentAzureFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/deployment-azure/edit/:id',
        component: DeploymentAzureFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'bfs/deployment-azure/delete/:id', 
        component: DeploymentAzureFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    }
]