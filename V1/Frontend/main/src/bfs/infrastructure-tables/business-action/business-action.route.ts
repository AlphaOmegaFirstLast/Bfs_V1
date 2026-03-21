import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BusinessActionListComponent } from './business-action.list.component';
import { BusinessActionFormComponent } from './business-action.form.component';

// Example role, api, and app
export const BusinessAction_ROUTES: Routes = [
    {
        path: 'bfs/business-action/list', 
        component: BusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/business-action/list/:id', 
        component: BusinessActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/business-action/add/0', 
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/business-action/view/:id', 
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/business-action/edit/:id',
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/business-action/delete/:id', 
        component: BusinessActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    }
]
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

