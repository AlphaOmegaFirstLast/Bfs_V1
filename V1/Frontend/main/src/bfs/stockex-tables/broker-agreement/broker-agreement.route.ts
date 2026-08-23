import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { BrokerAgreementListComponent } from './broker-agreement.list.component';
import { BrokerAgreementFormComponent } from './broker-agreement.form.component';

// Example role, api, and app
export const BrokerAgreement_ROUTES: Routes = [
    {
        path: 'stkx/broker-agreement/list', 
        component: BrokerAgreementListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/broker-agreement/list/:id', 
        component: BrokerAgreementListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/broker-agreement/add/0', 
        component: BrokerAgreementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/broker-agreement/view/:id', 
        component: BrokerAgreementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/broker-agreement/edit/:id',
        component: BrokerAgreementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/broker-agreement/delete/:id', 
        component: BrokerAgreementFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

