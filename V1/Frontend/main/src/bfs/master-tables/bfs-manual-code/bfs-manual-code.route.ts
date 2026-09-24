import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BfsManualCodeListComponent } from './bfs-manual-code.list.component';
import { BfsManualCodeFormComponent } from './bfs-manual-code.form.component';

// Example role, api, and app
export const BfsManualCode_ROUTES: Routes = [
    {
        path: 'mstr/bfs-manual-code/list', 
        component: BfsManualCodeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-manual-code/list/:id', 
        component: BfsManualCodeListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-manual-code/add/0', 
        component: BfsManualCodeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-manual-code/view/:id', 
        component: BfsManualCodeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-manual-code/edit/:id',
        component: BfsManualCodeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    },
    {
        path: 'mstr/bfs-manual-code/delete/:id', 
        component: BfsManualCodeFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['master'], app: ['b.ofc'] } 
    }
]