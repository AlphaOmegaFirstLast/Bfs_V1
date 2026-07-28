import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ResourceRuleListComponent } from './resource-rule.list.component';
import { ResourceRuleFormComponent } from './resource-rule.form.component';

// Example role, api, and app
export const ResourceRule_ROUTES: Routes = [
    {
        path: 'ath/resource-rule/list', 
        component: ResourceRuleListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/resource-rule/list/:id', 
        component: ResourceRuleListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/resource-rule/add/0', 
        component: ResourceRuleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/resource-rule/view/:id', 
        component: ResourceRuleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/resource-rule/edit/:id',
        component: ResourceRuleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
    {
        path: 'ath/resource-rule/delete/:id', 
        component: ResourceRuleFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    }
]

