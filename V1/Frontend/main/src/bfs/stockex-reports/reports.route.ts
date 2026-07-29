import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { TradingRoomRepCompareComponent } from './trading-room-rep-compare/trading-room-rep-compare.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'stkx/report/trading-room-rep-compare/:id',
        component: TradingRoomRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
//Template_Component_RegisterRoute
]