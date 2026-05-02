import {Routes} from '@angular/router';

export const Auth_ROUTES: Routes = [
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
