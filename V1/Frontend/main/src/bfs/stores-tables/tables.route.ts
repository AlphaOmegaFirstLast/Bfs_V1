import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./str-store/str-store.route').then((mod) => mod.StrStore_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./str-product/str-product.route').then((mod) => mod.StrProduct_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./str-transaction/str-transaction.route').then((mod) => mod.StrTransaction_ROUTES),
    },

//Template_Component_RegisterRoute
]