import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { ThirdPartyTypeListComponent } from './third-party-type.list.component';
import { ThirdPartyTypeFormComponent } from './third-party-type.form.component';

// Example role, api, and app
export const ThirdPartyType_ROUTES: Routes = [
    {
        path: 'str/third-party-type/list', 
        component: ThirdPartyTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/third-party-type/list/:id', 
        component: ThirdPartyTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/third-party-type/add/0', 
        component: ThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/third-party-type/view/:id', 
        component: ThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/third-party-type/edit/:id',
        component: ThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/third-party-type/delete/:id', 
        component: ThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]

