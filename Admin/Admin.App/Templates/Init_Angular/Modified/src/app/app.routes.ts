import {Routes} from '@angular/router';
import {MainLayoutComponent} from '@layouts/main-layout/main-layout.component';
import { HomeComponent } from '../bfs/home/home.component';
import { environment } from '@environment/environment.staging';

const BFS_SYSTEMS_ROUTES: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',

    },
    {
        path: 'landing',
        component: HomeComponent,
    },
    //Template_System_AddRouteEntry
];

export const routes: Routes = BFS_SYSTEMS_ROUTES;
