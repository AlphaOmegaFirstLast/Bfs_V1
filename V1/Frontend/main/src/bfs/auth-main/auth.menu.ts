import { MenuItemType } from '@/app/types/layout';

//------------------------------------------------------------
// Application specific menu items
export const AuthMenuItems: MenuItemType[] = [
    {label: 'Auth', 
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
        label: 'Role - System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/ath/role-component-system-action/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Users',
        icon: 'tablerLayoutSidebar',
        url: '/ath/user/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Applications',
        icon: 'tablerLayoutSidebar',
        url: '/ath/app/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Roles',
        icon: 'tablerLayoutSidebar',
        url: '/ath/role/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Role - Application',
        icon: 'tablerLayoutSidebar',
        url: '/ath/role-app/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Role - User',
        icon: 'tablerLayoutSidebar',
        url: '/ath/role-user/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'User Requests',
        icon: 'tablerLayoutSidebar',
        url: '/ath/user-request/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Resource - Rule',
        icon: 'tablerLayoutSidebar',
        url: '/ath/resource-rule/list',
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
        label: 'Roles',
        icon: 'tablerLayoutSidebar',
        url: '/ath/report/role-rep-compare/0',
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
        label: 'User Request Status',
        icon: 'tablerLayoutSidebar',
        url: '/ath/user-request-status/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Seed
        ]
    },
];
