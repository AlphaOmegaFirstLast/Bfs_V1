import { MenuItemType } from '@/app/types/layout';

//------------------------------------------------------------
// Application specific menu items
export const StockExMenuItems: MenuItemType[] = [
    {
        label: 'StockEx',
        isTitle: true,
        data: {
            role: ['admin']
        },
    },
    {
        label: 'Quick Access',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['b.ofc'],
            role: ['admin']
        },
        children: [
            {
                label: 'Investors',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/investor/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
                        {
                label: 'Portfolios Balances',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/ss-portfolio-balance/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Stock Share Stocks',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/ssp-stock/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
        ]
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
                label: 'Trading Rooms',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/trading-room/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Brokers',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/broker/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Investors',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/investor/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Broker Agreements',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/broker-agreement/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Currencies',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/currency/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Expenses Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/expenses-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'StockShare Portfolios',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/ss-portfolio/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Stock Shares',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/stock-share/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            //Template_Component_Menu_Basic
        ]
    },
    {
        label: 'Transactions',
        icon: 'tablerMail',
        isCollapsed: true,
        data: {
            app: ['b.ofc'],
            role: ['admin']
        },
        children: [
            {
                label: 'Cash Transactions',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/cash-transaction/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'StockShare Transactions',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/ssp-transaction/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Coupons',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/coupon/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Investor Broker Funds',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/investor-broker-fund/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Overdraft Portfolios',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/overdraft-portfolio/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Current Prices',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/current-price/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Coupons',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/coupon/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            //Template_Component_Menu_Transactions
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
                label: 'Trading Room Reports',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/report/trading-room-rep-compare/0',
                data: {
                    role: ['admin']
                }
            },
            {
                label: 'Portfolio Report',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/report/portfolio-compare/0',
                data: {
                    role: ['admin']
                }
            },
            {
                label: 'Portfolio Aggregate Report',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/report/portfolio-aggregate-compare/0',
                data: {
                    role: ['admin']
                }
            },
            {
                label: 'Portfolios Cash Transactions',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/report/portfolio-cash-transaction-compare/0',
                data: {
                    role: ['admin']
                }
            },
            {
                label: 'Total Portfolio Cash Transaction Aggregate',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/report/portfolio-cash-transaction-aggregate-compare/0',
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
            {
                label: 'Custom Reports',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/custom-reports/list',
                data: {
                    role: ['admin', 'investor', 'broker']
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
                label: 'Portfolios Balances',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/ss-portfolio-balance/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Stock Share Stocks',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/ssp-stock/list',
                data: {
                    role: ['admin', 'investor', 'broker']
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
                label: 'Coupon Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/coupon-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Coupon Status',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/coupon-status/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Transfer Cost Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/transfer-cost-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Transaction Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/transaction-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Effect Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/effect-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Calculation Methods',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/calculation-method/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Source Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/source-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Stock Field Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/stock-field-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            {
                label: 'Stock Entity Types',
                icon: 'tablerLayoutSidebar',
                url: '/stkx/stock-entity-type/list',
                data: {
                    role: ['admin', 'investor', 'broker']
                }
            },
            //Template_Component_Menu_Seed
        ]
    },
];
