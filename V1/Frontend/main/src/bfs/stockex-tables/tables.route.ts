import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./trading-room/trading-room.route').then((mod) => mod.TradingRoom_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./broker/broker.route').then((mod) => mod.Broker_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./investor/investor.route').then((mod) => mod.Investor_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./broker-agreement/broker-agreement.route').then((mod) => mod.BrokerAgreement_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./cash-transaction/cash-transaction.route').then((mod) => mod.CashTransaction_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./coupon/coupon.route').then((mod) => mod.Coupon_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./currency/currency.route').then((mod) => mod.Currency_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./current-price/current-price.route').then((mod) => mod.CurrentPrice_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./expenses-type/expenses-type.route').then((mod) => mod.ExpensesType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./investor-broker-fund/investor-broker-fund.route').then((mod) => mod.InvestorBrokerFund_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./overdraft-portfolio/overdraft-portfolio.route').then((mod) => mod.OverdraftPortfolio_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./ss-portfolio/ss-portfolio.route').then((mod) => mod.SsPortfolio_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./ss-portfolio-balance/ss-portfolio-balance.route').then((mod) => mod.SsPortfolioBalance_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./ssp-stock/ssp-stock.route').then((mod) => mod.SspStock_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./ssp-transaction/ssp-transaction.route').then((mod) => mod.SspTransaction_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./stock-share/stock-share.route').then((mod) => mod.StockShare_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./custom-reports/custom-reports.route').then((mod) => mod.CustomReports_ROUTES),
    },

//Template_Component_RegisterRoute
]