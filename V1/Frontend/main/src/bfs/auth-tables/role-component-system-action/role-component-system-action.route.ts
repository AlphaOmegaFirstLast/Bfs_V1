import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { RoleComponentSystemActionListComponent } from './role-component-system-action.list.component';
import { RoleComponentSystemActionFormComponent } from './role-component-system-action.form.component';

// Example role, api, and app
export const RoleComponentSystemAction_ROUTES: Routes = [
    {
        path: 'ath/role-component-system-action/list', 
        component: RoleComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/role-component-system-action/list/:id', 
        component: RoleComponentSystemActionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/role-component-system-action/add/0', 
        component: RoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/role-component-system-action/view/:id', 
        component: RoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/role-component-system-action/edit/:id',
        component: RoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/role-component-system-action/delete/:id', 
        component: RoleComponentSystemActionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]

