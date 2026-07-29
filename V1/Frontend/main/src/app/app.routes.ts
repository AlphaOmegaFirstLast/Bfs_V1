import { Routes } from '@angular/router';
import { MainLayoutComponent } from '@layouts/main-layout/main-layout.component';

const BFS_SYSTEMS_ROUTES: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            {
                path: '',
                loadChildren: () => import('../bfs/_shared/pages/home/home.route').then((mod) => mod.Home_ROUTES),
            },
            {
                path: '',
                loadChildren: () => import('../bfs/_shared/pages/error/error.route').then((mod) => mod.ERROR_PAGES_ROUTES)
            },
            {
                path: '',
                loadChildren: () => import('../bfs/stores-main/stores.route').then((mod) => mod.Stores_ROUTES)
            },
            {
                path: '',
                loadChildren: () => import('../bfs/auth-main/auth.route').then((mod) => mod.Auth_ROUTES)
            },
            {
                path: '',
                loadChildren: () => import('../bfs/master-main/master.route').then((mod) => mod.Master_ROUTES)
            },
                {
        path: '',
        component: MainLayoutComponent,
        loadChildren: () => import('../bfs/stockex-main/stockex.route').then((mod) => mod.StockEx_ROUTES)
    },
//Template_System_AddRouteEntry
        ]
    },
];

export const routes: Routes = BFS_SYSTEMS_ROUTES;
