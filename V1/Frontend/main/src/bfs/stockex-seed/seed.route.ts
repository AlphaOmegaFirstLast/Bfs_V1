import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const Seed_ROUTES: Routes = [ 
    {
        path: '',
        loadChildren: () => import('./coupon-type/coupon-type.route').then((mod) => mod.CouponType_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./coupon-status/coupon-status.route').then((mod) => mod.CouponStatus_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./coupon-type/coupon-type.route').then((mod) => mod.CouponType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./transaction-type/transaction-type.route').then((mod) => mod.TransactionType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./transfer-cost-type/transfer-cost-type.route').then((mod) => mod.TransferCostType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./effect-type/effect-type.route').then((mod) => mod.EffectType_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./calculation-method/calculation-method.route').then((mod) => mod.CalculationMethod_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./source-type/source-type.route').then((mod) => mod.SourceType_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./stock-entity-type/stock-entity-type.route').then((mod) => mod.StockEntityType_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('./stock-field-type/stock-field-type.route').then((mod) => mod.StockFieldType_ROUTES),
    },

//Template_Component_RegisterRoute
]