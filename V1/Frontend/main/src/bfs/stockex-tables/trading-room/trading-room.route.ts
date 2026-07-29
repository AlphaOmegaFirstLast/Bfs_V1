import { Routes } from '@angular/router';
import { RouteGuardService } from "@bfs/_shared/security/route-guard.service";
import { TradingRoomListComponent } from './trading-room.list.component';
import { TradingRoomFormComponent } from './trading-room.form.component';

// Example role, api, and app
export const TradingRoom_ROUTES: Routes = [
    {
        path: 'stkx/trading-room/list', 
        component: TradingRoomListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/trading-room/list/:id', 
        component: TradingRoomListComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/trading-room/add/0', 
        component: TradingRoomFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/trading-room/view/:id', 
        component: TradingRoomFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin', 'investor','broker'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/trading-room/edit/:id',
        component: TradingRoomFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    },
    {
        path: 'stkx/trading-room/delete/:id', 
        component: TradingRoomFormComponent,
        canActivate: [RouteGuardService],
        data: { role: ['admin'], api: ['stockex'], app: ['b.ofc'] } 
    }
]