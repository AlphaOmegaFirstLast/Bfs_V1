import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { SystemTemplateListComponent } from './system-template.list.component';
import { SystemTemplateFormComponent } from './system-template.form.component';

// Example role, api, and app
export const SystemTemplate_ROUTES: Routes = [
    {
        path: 'mstr/system-template/list', 
        component: SystemTemplateListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/system-template/list/:id', 
        component: SystemTemplateListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/system-template/add/0', 
        component: SystemTemplateFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/system-template/view/:id', 
        component: SystemTemplateFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/system-template/edit/:id',
        component: SystemTemplateFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/system-template/delete/:id', 
        component: SystemTemplateFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]

