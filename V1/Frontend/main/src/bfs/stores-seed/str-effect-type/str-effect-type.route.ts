import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { StrEffectTypeListComponent } from './str-effect-type.list.component';
import { StrEffectTypeFormComponent } from './str-effect-type.form.component';

// Example role, api, and app
export const StrEffectType_ROUTES: Routes = [
    {
        path: 'str/str-effect-type/list', 
        component: StrEffectTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-effect-type/list/:id', 
        component: StrEffectTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-effect-type/add/0', 
        component: StrEffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-effect-type/view/:id', 
        component: StrEffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-effect-type/edit/:id',
        component: StrEffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/str-effect-type/delete/:id', 
        component: StrEffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]