import {Routes} from '@angular/router';

export const StockEx_ROUTES: Routes = [
    {
        path: '',
        loadChildren: () => import('../tables/tables.route').then((mod) => mod.TABLES_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../reports/reports.route').then((mod) => mod.REPORTS_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../seed/seed.route').then((mod) => mod.Seed_ROUTES),
    }   
];
