import { MenuItemType } from '@/app/types/layout';

//------------------------------------------------------------
// Application specific menu items
export const StoresMenuItems: MenuItemType[] = [
    {label: 'Stores', 
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
        label: 'Stores',
        icon: 'tablerLayoutSidebar',
        url: '/str/store/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Products',
        icon: 'tablerLayoutSidebar',
        url: '/str/product/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Transactions',
        icon: 'tablerLayoutSidebar',
        url: '/str/transaction/list',
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
        label: 'Transactions By Product',
        icon: 'tablerLayoutSidebar',
        url: '/str/report/product-transaction-compare/0',
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
        label: 'Effect Types',
        icon: 'tablerLayoutSidebar',
        url: '/str/effect-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Third Party Types',
        icon: 'tablerLayoutSidebar',
        url: '/str/third-party-type/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Units',
        icon: 'tablerLayoutSidebar',
        url: '/str/unit/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Currencies',
        icon: 'tablerLayoutSidebar',
        url: '/str/currency/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
    {
        label: 'Operations',
        icon: 'tablerLayoutSidebar',
        url: '/str/operation/list',
        data: {
            role: ['admin', 'investor','broker']
        }
    },
//Template_Component_Menu_Seed
        ]
    },
];
