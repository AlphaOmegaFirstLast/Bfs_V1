import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { EffectTypeListComponent } from './effect-type.list.component';
import { EffectTypeFormComponent } from './effect-type.form.component';

// Example role, api, and app
export const EffectType_ROUTES: Routes = [
    {
        path: 'str/effect-type/list', 
        component: EffectTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/effect-type/list/:id', 
        component: EffectTypeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/effect-type/add/0', 
        component: EffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/effect-type/view/:id', 
        component: EffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/effect-type/edit/:id',
        component: EffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    },
    {
        path: 'str/effect-type/delete/:id', 
        component: EffectTypeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stores'], app: ['b.ofc'] } 
    }
]

