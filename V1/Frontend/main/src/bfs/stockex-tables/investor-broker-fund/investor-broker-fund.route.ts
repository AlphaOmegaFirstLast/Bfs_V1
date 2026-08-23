import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { InvestorBrokerFundListComponent } from './investor-broker-fund.list.component';
import { InvestorBrokerFundFormComponent } from './investor-broker-fund.form.component';

// Example role, api, and app
export const InvestorBrokerFund_ROUTES: Routes = [
    {
        path: 'stkx/investor-broker-fund/list', 
        component: InvestorBrokerFundListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/investor-broker-fund/list/:id', 
        component: InvestorBrokerFundListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/investor-broker-fund/add/0', 
        component: InvestorBrokerFundFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/investor-broker-fund/view/:id', 
        component: InvestorBrokerFundFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/investor-broker-fund/edit/:id',
        component: InvestorBrokerFundFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/investor-broker-fund/delete/:id', 
        component: InvestorBrokerFundFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]

