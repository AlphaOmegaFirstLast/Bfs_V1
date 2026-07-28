import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ProductTransactionCompareComponent } from './product-transaction-compare/product-transaction-compare.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'str/report/product-transaction-compare/:id',
        component: ProductTransactionCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
//Template_Component_RegisterRoute
]

