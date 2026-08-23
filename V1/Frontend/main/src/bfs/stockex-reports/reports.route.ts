import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { TradingRoomRepCompareComponent } from './trading-room-rep-compare/trading-room-rep-compare.report.component';
import { PortfolioCompareComponent } from './portfolio-compare/portfolio-compare.report.component';
import { PortfolioAggregateCompareComponent } from './portfolio-aggregate-compare/portfolio-aggregate-compare.report.component';
import { PortfolioCashTransactionCompareComponent } from './portfolio-cash-transaction-compare/portfolio-cash-transaction-compare.report.component';
import { PortfolioCashTransactionAggregateCompareComponent } from './portfolio-cash-transaction-aggregate-compare/portfolio-cash-transaction-aggregate-compare.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'stkx/report/trading-room-rep-compare/:id',
        component: TradingRoomRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/portfolio-compare/:id',
        component: PortfolioCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/portfolio-aggregate-compare/:id',
        component: PortfolioAggregateCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/portfolio-cash-transaction-compare/:id',
        component: PortfolioCashTransactionCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/portfolio-cash-transaction-aggregate-compare/:id',
        component: PortfolioCashTransactionAggregateCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
//Template_Component_RegisterRoute
]