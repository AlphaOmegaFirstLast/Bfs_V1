import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";

export const Seed_ROUTES: Routes = [ 
    {
        path: '',
        loadChildren: () => import('./user-request-status/user-request-status.route').then((mod) => mod.UserRequestStatus_ROUTES),
    },

//Template_Component_RegisterRoute
]