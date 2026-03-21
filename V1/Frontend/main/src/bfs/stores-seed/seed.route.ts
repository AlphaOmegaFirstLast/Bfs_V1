import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const Seed_ROUTES: Routes = [ 
    {
        path: '',
        loadChildren: () => import('./str-effect-type/str-effect-type.route').then((mod) => mod.StrEffectType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./str-third-party-type/str-third-party-type.route').then((mod) => mod.StrThirdPartyType_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./str-unit/str-unit.route').then((mod) => mod.StrUnit_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./str-currency/str-currency.route').then((mod) => mod.StrCurrency_ROUTES),
    },

    {
        path: '',
        loadChildren: () => import('./str-operation/str-operation.route').then((mod) => mod.StrOperation_ROUTES),
    },

//Template_Component_RegisterRoute
]