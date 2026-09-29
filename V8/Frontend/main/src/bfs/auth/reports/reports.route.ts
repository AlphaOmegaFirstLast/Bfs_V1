import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { RoleRepCompareComponent } from './role-rep-compare/role-rep-compare.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'ath/report/role-rep-compare/:id',
        component: RoleRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['auth'], app: ['b.ofc'] } 
    },
//Template_Component_RegisterRoute
]