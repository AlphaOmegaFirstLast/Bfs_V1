import { MenuItemType } from '@/app/types/layout';

//------------------------------------------------------------
// Application specific menu items
export const MasterMenuItems: MenuItemType[] = [
    {label: 'Master', 
     isTitle: true,
     data: {
            role: ['admin']
        },
    },
    {
        label: 'Basic',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
    {
        label: 'BestFit Component',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-component/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'BestFit Fields',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-field/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'BestFit System',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-system/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'BestFit Tenants',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-tenant/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Tenant - System',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-tenant-system/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component - Business Actions',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-component-business-action/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Component - System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/bfs-component-system-action/list',
        data: {
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
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
    {
        label: 'Structure Compare',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/report/structure-compare/0',
        data: {
            role: ['admin']
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
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
    {
        label: 'Azure Deployment',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/deployment-azure/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Local Deployment',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/deployment-local/list',
        data: {
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
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
    {
        label: 'Custom Reports',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/custom-reports/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Custom Field Definitions',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/custom-field-definition/list',
        data: {
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
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
    {
        label: 'System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/system-action/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Business Actions',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/business-action/list',
        data: {
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
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
    {
        label: 'System Templates',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/system-template/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Data Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/data-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Filter Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/filter-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Form Control Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/form-control-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Backend Data Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/backend-data-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Action Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/action-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Aggregate Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/aggregate-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Action Locations',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/action-location/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Chart Elements',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/chart-element/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Writer Types',
        icon: 'tablerLayoutSidebar',
        url: '/mstr/writer-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Seed
        ]
    },
];

