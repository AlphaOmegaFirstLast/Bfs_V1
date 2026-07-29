import {Routes} from '@angular/router';

export const StockEx_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('../stockex-tables/tables.route').then((mod) => mod.TABLES_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../stockex-reports/reports.route').then((mod) => mod.REPORTS_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../stockex-seed/seed.route').then((mod) => mod.Seed_ROUTES),
    }   
];
