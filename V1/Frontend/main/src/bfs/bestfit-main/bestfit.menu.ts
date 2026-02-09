import { MenuItemType } from '@/app/types/layout';

//------------------------------------------------------------
// Application specific menu items
export const BestFitMenuItems: MenuItemType[] = [
    {
        label: 'BestFit Basic',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['bestfit'],
            role: ['admin']
        },
        children: [
    {
        label: 'Components',
        icon: 'tablerLayoutSidebar',
        url: '/component/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Table Fields',
        icon: 'tablerLayoutSidebar',
        url: '/table-field/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Systems',
        icon: 'tablerLayoutSidebar',
        url: '/system-info/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Clients',
        icon: 'tablerLayoutSidebar',
        url: '/client/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component - System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/component-system-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component - Business Actions',
        icon: 'tablerLayoutSidebar',
        url: '/component-business-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },

//Template_Component_Menu_Basic
        ]
    },

    {
        label: 'Reports',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['bestfit'],
            role: ['admin']
        },
        children: [
    {
        label: 'Structure Report',
        icon: 'tablerLayoutSidebar',
        url: '/report/structure-report/0',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'DataType1 List',
        icon: 'tablerLayoutSidebar',
        url: '/report/data-type1/0',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Reports
        ]
    },

    {
        label: 'Deployment',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['bestfit'],
            role: ['admin']
        },
        children: [
    {
        label: 'Azure Staging Deployment',
        icon: 'tablerLayoutSidebar',
        url: '/deployment-azure-staging/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Local Deployment',
        icon: 'tablerLayoutSidebar',
        url: '/deployment-local/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Deployment
        ]
    },

    {
        label: 'Custom',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['bestfit'],
            role: ['admin']
        },
        children: [
    {
        label: 'Custom Field Definitions',
        icon: 'tablerLayoutSidebar',
        url: '/custom-field-definition/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Custom
        ]
    },

    {
        label: 'System',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['bestfit'],
            role: ['admin']
        },
        children: [
    {
        label: 'Business Actions',
        icon: 'tablerLayoutSidebar',
        url: '/business-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_System
        ]
    },

    {
        label: 'Seed Data',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['bestfit'],
            role: ['admin']
        },
        children: [
    {
        label: 'Action Types',
        icon: 'tablerLayoutSidebar',
        url: '/action-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Data Types',
        icon: 'tablerLayoutSidebar',
        url: '/data-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'System Templates',
        icon: 'tablerLayoutSidebar',
        url: '/system-template/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Filter Types',
        icon: 'tablerLayoutSidebar',
        url: '/filter-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Form Control Types',
        icon: 'tablerLayoutSidebar',
        url: '/form-control-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component Types',
        icon: 'tablerLayoutSidebar',
        url: '/component-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Backend Data Types',
        icon: 'tablerLayoutSidebar',
        url: '/backend-data-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Aggregate Types',
        icon: 'tablerLayoutSidebar',
        url: '/aggregate-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Chart Elements',
        icon: 'tablerLayoutSidebar',
        url: '/chart-element/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Action Locations',
        icon: 'tablerLayoutSidebar',
        url: '/action-location/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/system-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Custom Reports',
        icon: 'tablerLayoutSidebar',
        url: '/custom-reports/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['bestfit'],
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Seed
        ]
    },
];
