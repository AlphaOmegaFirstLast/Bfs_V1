import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./store/store.route').then((mod) => mod.Store_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./product/product.route').then((mod) => mod.Product_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./transaction/transaction.route').then((mod) => mod.Transaction_ROUTES),
    },

//Template_Component_RegisterRoute
]