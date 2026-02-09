import {Routes} from '@angular/router';

export const BestFit_ROUTES: Routes = [
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
        loadChildren: () => import('../_shared/custom-reports/custom-reports.route').then((mod) => mod.CustomReportsList_ROUTES),
    }, 
    {
        path: '',
        loadChildren: () => import('../bestfit-tables/tables.route').then((mod) => mod.TABLES_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../bestfit-reports/reports.route').then((mod) => mod.REPORTS_ROUTES),
    },
    {
        path: '',
        loadChildren: () => import('../bestfit-seed/seed.route').then((mod) => mod.Seed_ROUTES),
    }   
];
