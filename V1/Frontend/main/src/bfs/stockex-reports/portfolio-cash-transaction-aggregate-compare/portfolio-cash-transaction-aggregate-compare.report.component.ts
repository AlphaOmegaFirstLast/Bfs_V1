//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
//---------------- Ng Bootstrap ------------------------------
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core'
//---------------- charts -------------------------------------
import { getColor } from "@/app/utils/color-utils";
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import type { EChartsType } from 'echarts/core';
import { echarts } from '@/app/config/echarts-config';
import { EChartsOption } from 'echarts';
//---------------- bfs shared -------------------------------------
import { IQueryColumn, IEntity, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/master-main/master.operations';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { StockExService } from '@bfs/stockex-main/stockex.service';

import { type IPortfolioCashTransactionAggregateCompareWithLookup, type IPortfolioCashTransactionAggregateCompareRequest, type IPortfolioCashTransactionAggregateCompareFilter } from './portfolio-cash-transaction-aggregate-compare.shared';
import { getPortfolioCashTransactionAggregateCompareActions,  initPortfolioCashTransactionAggregateCompareRequest } from './portfolio-cash-transaction-aggregate-compare.shared';
import { PortfolioCashTransactionAggregateCompareFilterComponent } from './portfolio-cash-transaction-aggregate-compare.filter.component'; 

@Component({
    selector: 'portfolio-cash-transaction-aggregate-compare',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class PortfolioCashTransactionAggregateCompareComponent         

    extends BaseReportComponent<IPortfolioCashTransactionAggregateCompareFilter, IPortfolioCashTransactionAggregateCompareWithLookup> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as IPortfolioCashTransactionAggregateCompareRequest;
    override exportRequest = {} as IPortfolioCashTransactionAggregateCompareRequest;
    override downloadFileName: string = "Total Portfolio Cash Transaction Aggregate";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/reports/PortfolioCashTransactionAggregateCompare';

        this.filterComponent = PortfolioCashTransactionAggregateCompareFilterComponent;
        this.queryRequest = initPortfolioCashTransactionAggregateCompareRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            case 'sumValue':
                return record['sumValue']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

   override getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getPortfolioCashTransactionAggregateCompareActions(this,record);
        let links: ViewLink[] = actions.filter(action => 
               action.actionType == 'FrontendLink'
            && action.actionLocation == 'ListRow'
            ).map(action => {
            return { recordId: action.recordId, route: action.route?? '', displayText: action.displayText}
        });

        return links;
    }
    //---------------------------------------------------------
    override getRecordActions(record: IEntity): ActionLink[] {
        let actions = getPortfolioCashTransactionAggregateCompareActions(this, record);
        let links: ActionLink[] = actions.filter(action => 
               action.actionType == 'FrontendFunction'
            && action.actionLocation == 'ListRow'
            ).map(action => {
            return { recordId: action.recordId, action: action.action?? null, displayText: action.displayText, data: action.data}
        });

        return links;
    }
//--------------------------------------------------------------

override getChart(records: IPortfolioCashTransactionAggregateCompareWithLookup[]): EChartsOption {
        // return this.getDemoChart();
        // reorder records in reverse order to show same order of table records.
        let reversedRecords = records.reverse();
        let baseChart = this.getBaseChart();
        baseChart.yAxis = {
        data: reversedRecords.map(x => x['ssPortfolio_Name' as keyof IPortfolioCashTransactionAggregateCompareWithLookup] ?? "unknown"),

            type: 'category',
            axisLine: {
                lineStyle: {
                    type: 'dashed', color: getColor('light')
                }
            },
            axisLabel: {
                show: true, color: getColor('body-color')
            },
            splitLine: {
                lineStyle: {
                    color: "rgba(133, 141, 152, 0.1)", type: 'dashed'
                }
            }
        };

        baseChart.series = [     
        {
            name: 'Value',

            type: 'bar',
            barWidth: "10px",
            barGap: "0.25",
            z: 10,
            itemStyle: {
                borderRadius: [4, 4, 0, 0], color: getColor('info')
            },
            data: reversedRecords.map(x => x.sumValue),
        }

        ]
        ;

        return baseChart;
    }

}

