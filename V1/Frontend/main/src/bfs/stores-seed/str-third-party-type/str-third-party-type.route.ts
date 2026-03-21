import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StrThirdPartyTypeListComponent } from './str-third-party-type.list.component';
import { StrThirdPartyTypeFormComponent } from './str-third-party-type.form.component';

// Example role, api, and app
export const StrThirdPartyType_ROUTES: Routes = [
    {
        path: 'str/str-third-party-type/list', 
        component: StrThirdPartyTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-third-party-type/list/:id', 
        component: StrThirdPartyTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-third-party-type/add/0', 
        component: StrThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-third-party-type/view/:id', 
        component: StrThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-third-party-type/edit/:id',
        component: StrThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-third-party-type/delete/:id', 
        component: StrThirdPartyTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]