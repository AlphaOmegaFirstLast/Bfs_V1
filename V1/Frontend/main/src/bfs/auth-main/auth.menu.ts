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
        label: 'Roles',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/auth-role/list',
        data: {
           app: ['b.ofc'],
           role: ['admin']
        }
    },
    {
        label: 'Users',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/auth-user/list',
        data: {
           app: ['b.ofc'],
           role: ['admin']
        }
    },
    {
        label: 'Applications',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/auth-app/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Role - System Actions',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/auth-role-component-system-action/list',
        data: {
           app: ['b.ofc'],
           role: ['admin']
        }
    },
    {
        label: 'Role - Application',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/auth-role-app/list',
        data: {
           app: ['b.ofc'],
           role: ['admin']
        }
    },
    {
        label: 'Role - User',
        icon: 'tablerLayoutSidebar',
        url: '/bfs/auth-role-user/list',
        data: {
           app: ['b.ofc'],
           role: ['admin'] 
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
        url: '/bfs/report/role-rep-compare/0',
        data: {
            role: ['admin']
        }
    },
//Template_Component_Menu_Reports
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
        label: 'Seed Data',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
           app: ['b.ofc'],
           role: ['admin']
        },
        children: [
//Template_Component_Menu_Seed
        ]
    },
];
