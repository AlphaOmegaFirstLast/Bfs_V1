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
        url: '/str/str-store/list',
        data: {
            role: ['admin']
        }
    },
    {
        label: 'Products',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-product/list',
        data: {
            role: ['admin']
        }
    },
    {
        label: 'Transactions',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-transaction/list',
        data: {
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
    {
        label: 'Effect Types',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-effect-type/list',
        data: {
            role: ['admin']
        }
    },
    {
        label: 'Third Party Types',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-third-party-type/list',
        data: {
            role: ['admin']
        }
    },
    {
        label: 'Units',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-unit/list',
        data: {
            role: ['admin']
        }
    },
    {
        label: 'Currencies',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-currency/list',
        data: {
            role: ['admin']
        }
    },
    {
        label: 'Operations',
        icon: 'tablerLayoutSidebar',
        url: '/str/str-operation/list',
        data: {
            role: ['admin']
        }
    },
//Template_Component_Menu_Seed
        ]
    },
];
