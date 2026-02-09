import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SystemInfoListComponent } from './system-info.list.component';
import { SystemInfoFormComponent } from './system-info.form.component';

// Example role, api, and app
export const SystemInfo_ROUTES: Routes = [
    {
        path: 'system-info/list', 
        component: SystemInfoListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'system-info/list/:id', 
        component: SystemInfoListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'system-info/add/0', 
        component: SystemInfoFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'system-info/view/:id', 
        component: SystemInfoFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'system-info/edit/:id',
        component: SystemInfoFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'system-info/delete/:id', 
        component: SystemInfoFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]