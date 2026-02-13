import { MenuItemType } from '@/app/types/layout';

//------------------------------------------------------------
// Application specific menu items
export const InfrastructureMenuItems: MenuItemType[] = [
    {label: 'Infrastructure', 
     isTitle: true,
     data: {
            app: ['bestfit.b.ofc'],
            api: ['infrastructure'],
            role: ['admin']
        },
    },
    {
        label: 'BestFit Basic',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['infrastructure'],
            role: ['admin']
        },
        children: [
    {
        label: 'BestFit Component',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/bfs-component/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'BestFit Fields',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/bfs-field/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'BestFit System',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/bfs-system/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'BestFit Clients',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/bfs-client/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component - System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/bfs-component-system-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component - Business Actions',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/bfs-component-business-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    }
//Template_Component_Menu_Basic
        ]
    },

    {
        label: 'Reports',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['infrastructure'],
            role: ['admin']
        },
        children: [
    {
        label: 'Structure Compare',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/report/structure-compare/0',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
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
            api: ['infrastructure'],
            role: ['admin']
        },
        children: [

    {
        label: 'Local Deployment',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/deployment-local/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Azure Deployment',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/deployment-azure/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
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
            api: ['infrastructure'],
            role: ['admin']
        },
        children: [
    {
        label: 'Custom Reports',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/custom-reports/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Custom Field Definitions',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/custom-field-definition/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
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
            api: ['infrastructure'],
            role: ['admin']
        },
        children: [
    {
        label: 'Business Actions',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/business-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/system-action/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    }
//Template_Component_Menu_System
        ]
    },

    {
        label: 'Seed Data',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['bestfit.b.ofc'],
            api: ['infrastructure'],
            role: ['admin']
        },
        children: [
    {
        label: 'Data Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/data-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'System Templates',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/system-template/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Filter Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/filter-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Form Control Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/form-control-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Backend Data Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/backend-data-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Action Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/action-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Aggregate Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/aggregate-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Action Locations',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/action-location/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Chart Elements',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/chart-element/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Writer Types',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/writer-type/list',
        data: {
            app: ['stkex.b.ofc'],
            api: ['infrastructure'],
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Seed
        ]
    },
];
