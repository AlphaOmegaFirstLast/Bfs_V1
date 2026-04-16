import {Routes} from '@angular/router';

export const Master_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('../master-tables/tables.route').then((mod) => mod.TABLES_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../master-reports/reports.route').then((mod) => mod.REPORTS_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../master-seed/seed.route').then((mod) => mod.Seed_ROUTES),
    }   
];
