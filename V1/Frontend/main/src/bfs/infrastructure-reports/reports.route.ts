import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StructureReportCompareComponent } from './structure-report/structure-report.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'bfs-report/structure-report/:id',
        component: StructureReportCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['infrastructure'], app: ['stkex.b.ofc'] } 
    },
//Template_Component_RegisterRoute
]