import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { DocumentDetailsListComponent } from './document-details.list.component';
import { DocumentDetailsFormComponent } from './document-details.form.component';

// Example role, api, and app
export const DocumentDetails_ROUTES: Routes = [
    {
        path: 'str/document-details/list', 
        component: DocumentDetailsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/document-details/list/:id', 
        component: DocumentDetailsListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/document-details/add/0', 
        component: DocumentDetailsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/document-details/view/:id', 
        component: DocumentDetailsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/document-details/edit/:id',
        component: DocumentDetailsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/document-details/delete/:id', 
        component: DocumentDetailsFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]

