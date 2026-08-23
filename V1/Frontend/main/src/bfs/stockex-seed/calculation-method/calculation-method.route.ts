import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { CalculationMethodListComponent } from './calculation-method.list.component';
import { CalculationMethodFormComponent } from './calculation-method.form.component';

// Example role, api, and app
export const CalculationMethod_ROUTES: Routes = [
    {
        path: 'stkx/calculation-method/list', 
        component: CalculationMethodListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/calculation-method/list/:id', 
        component: CalculationMethodListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/calculation-method/add/0', 
        component: CalculationMethodFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/calculation-method/view/:id', 
        component: CalculationMethodFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/calculation-method/edit/:id',
        component: CalculationMethodFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/calculation-method/delete/:id', 
        component: CalculationMethodFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

