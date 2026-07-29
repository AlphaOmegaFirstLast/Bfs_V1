import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const TABLES_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('./trading-room/trading-room.route').then((mod) => mod.TradingRoom_ROUTES),
    },

//Template_Component_RegisterRoute
]