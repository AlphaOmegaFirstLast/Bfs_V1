import {Routes} from '@angular/router';

export const Stores_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('../stores-tables/tables.route').then((mod) => mod.TABLES_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../stores-reports/reports.route').then((mod) => mod.REPORTS_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../stores-seed/seed.route').then((mod) => mod.Seed_ROUTES),
    }   
];

