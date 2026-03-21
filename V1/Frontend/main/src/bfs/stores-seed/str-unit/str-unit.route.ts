import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StrUnitListComponent } from './str-unit.list.component';
import { StrUnitFormComponent } from './str-unit.form.component';

// Example role, api, and app
export const StrUnit_ROUTES: Routes = [
    {
        path: 'str/str-unit/list', 
        component: StrUnitListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-unit/list/:id', 
        component: StrUnitListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-unit/add/0', 
        component: StrUnitFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-unit/view/:id', 
        component: StrUnitFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-unit/edit/:id',
        component: StrUnitFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-unit/delete/:id', 
        component: StrUnitFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]