import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { TradingRoomRepCompareComponent } from './trading-room-rep-compare/trading-room-rep-compare.report.component';
import { PortfolioStockShareTransactionCompareComponent } from './portfolio-stock-share-transaction-compare/portfolio-stock-share-transaction-compare.report.component';
import { PortfolioAggregateCompareComponent } from './portfolio-aggregate-compare/portfolio-aggregate-compare.report.component';
import { PortfolioCashTransactionCompareComponent } from './portfolio-cash-transaction-compare/portfolio-cash-transaction-compare.report.component';
import { PortfolioCashTransactionAggregateCompareComponent } from './portfolio-cash-transaction-aggregate-compare/portfolio-cash-transaction-aggregate-compare.report.component';
import { NavReportCompareComponent } from './nav-report-compare/nav-report-compare.report.component';
import { OverdraftRepCompareComponent } from './overdraft-rep-compare/overdraft-rep-compare.report.component';
import { StockshareBrokerRepCompareComponent } from './stockshare-broker-rep-compare/stockshare-broker-rep-compare.report.component';
import { PortfolioProfitRepCompareComponent } from './portfolio-profit-rep-compare/portfolio-profit-rep-compare.report.component';
//Template_Component_AddDeclareEntry

export const REPORTS_ROUTES: Routes = [
    {
        path: 'stkx/report/trading-room-rep-compare/:id',
        component: TradingRoomRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/portfolio-stock-share-transaction-compare/:id',
        component: PortfolioStockShareTransactionCompareComponent,
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
    {
        path: 'stkx/report/nav-report-compare/:id',
        component: NavReportCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/overdraft-rep-compare/:id',
        component: OverdraftRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/stockshare-broker-rep-compare/:id',
        component: StockshareBrokerRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/report/portfolio-profit-rep-compare/:id',
        component: PortfolioProfitRepCompareComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
//Template_Component_RegisterRoute
]