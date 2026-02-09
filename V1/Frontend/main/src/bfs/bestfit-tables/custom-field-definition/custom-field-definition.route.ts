import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CustomFieldDefinitionListComponent } from './custom-field-definition.list.component';
import { CustomFieldDefinitionFormComponent } from './custom-field-definition.form.component';

// Example role, api, and app
export const CustomFieldDefinition_ROUTES: Routes = [
    {
        path: 'custom-field-definition/list', 
        component: CustomFieldDefinitionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-field-definition/list/:id', 
        component: CustomFieldDefinitionListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-field-definition/add/0', 
        component: CustomFieldDefinitionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-field-definition/view/:id', 
        component: CustomFieldDefinitionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-field-definition/edit/:id',
        component: CustomFieldDefinitionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'custom-field-definition/delete/:id', 
        component: CustomFieldDefinitionFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    }
]