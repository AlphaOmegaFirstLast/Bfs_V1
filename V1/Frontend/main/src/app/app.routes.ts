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
    {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/home/home.route').then((mod) => mod.Home_ROUTES),
    },
    {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/error/error.route').then((mod) => mod.ERROR_PAGES_ROUTES)
    },
    {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/infrastructure-main/infrastructure.route').then((mod) => mod.Infrastructure_ROUTES)
    },
    {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/stores-main/stores.route').then((mod) => mod.Stores_ROUTES)
    },
    {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/auth-main/auth.route').then((mod) => mod.Auth_ROUTES)
    },
    {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/master-main/master.route').then((mod) => mod.Master_ROUTES)
    },
//Template_System_AddRouteEntry
];

export const routes: Routes = BFS_SYSTEMS_ROUTES;
