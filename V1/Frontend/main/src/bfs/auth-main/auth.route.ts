import {Routes} from '@angular/router';

export const Auth_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('../home/home.route').then((mod) => mod.Home_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../error/error.route').then((mod) => mod.ERROR_PAGES_ROUTES)
    },
    {
        path: '',
        loadChildren: () => import('../auth-tables/tables.route').then((mod) => mod.TABLES_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../auth-reports/reports.route').then((mod) => mod.REPORTS_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../auth-seed/seed.route').then((mod) => mod.Seed_ROUTES),
    }   
];
