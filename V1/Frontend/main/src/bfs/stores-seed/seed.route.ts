import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const Seed_ROUTES: Routes = [ 
    {
        path: '',
        loadChildren: () => import('./effect-type/effect-type.route').then((mod) => mod.EffectType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./third-party-type/third-party-type.route').then((mod) => mod.ThirdPartyType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./unit/unit.route').then((mod) => mod.Unit_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./currency/currency.route').then((mod) => mod.Currency_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./operation/operation.route').then((mod) => mod.Operation_ROUTES),
    },

//Template_Component_RegisterRoute
]