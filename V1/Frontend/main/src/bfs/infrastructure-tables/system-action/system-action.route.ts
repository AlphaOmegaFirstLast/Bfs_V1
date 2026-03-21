import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SystemActionListComponent } from './system-action.list.component';
import { SystemActionFormComponent } from './system-action.form.component';

// Example role, api, and app
export const SystemAction_ROUTES: Routes = [
    {
        path: 'bfs/system-action/list', 
        component: SystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/system-action/list/:id', 
        component: SystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/system-action/add/0', 
        component: SystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/system-action/view/:id', 
        component: SystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/system-action/edit/:id',
        component: SystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    },
    {
        path: 'bfs/system-action/delete/:id', 
        component: SystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['b.ofc'] } 
    }
]
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

