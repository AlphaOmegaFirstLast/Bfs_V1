import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StructureReportReportComponent } from './structure-report/structure-report.report.component';
import { DataType1ReportComponent } from './data-type1/data-type1.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'report/structure-report/:id',
        component: StructureReportReportComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
    {
        path: 'report/data-type1/:id',
        component: DataType1ReportComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['bestfit'], app: ['stkex.b.ofc'] } 
    },
//Template_Component_RegisterRoute
]