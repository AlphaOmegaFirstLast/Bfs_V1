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

import { type INavReportCompareWithLookup, type INavReportCompareRequest, type INavReportCompareFilter } from './nav-report-compare.shared';
import { getNavReportCompareActions, initNavReportCompareRequest } from './nav-report-compare.shared';
import { NavReportCompareFilterComponent } from './nav-report-compare.filter.component';

@Component({
    selector: 'nav-report-compare',
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
        NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
        NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class NavReportCompareComponent

    extends BaseReportComponent<INavReportCompareFilter, INavReportCompareWithLookup> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as INavReportCompareRequest;
    override exportRequest = {} as INavReportCompareRequest;
    override downloadFileName: string = "Nav Report";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/reports/NavReportCompare';

        this.filterComponent = NavReportCompareFilterComponent;
        this.queryRequest = initNavReportCompareRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            case 'stockVakue':
                return record['stockValue']?.toString();
            case 'cash':
                return record['cash']?.toString();
           case 'nav':
                return record['nav']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

    override getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getNavReportCompareActions(this, record);
        let links: ViewLink[] = actions.filter(action =>
            action.actionType == 'FrontendLink'
            && action.actionLocation == 'ListRow'
        ).map(action => {
            return { recordId: action.recordId, route: action.route ?? '', displayText: action.displayText }
        });

        return links;
    }
    //---------------------------------------------------------
    override getRecordActions(record: IEntity): ActionLink[] {
        let actions = getNavReportCompareActions(this, record);
        let links: ActionLink[] = actions.filter(action =>
            action.actionType == 'FrontendFunction'
            && action.actionLocation == 'ListRow'
        ).map(action => {
            return { recordId: action.recordId, action: action.action ?? null, displayText: action.displayText, data: action.data }
        });

        return links;
    }
    //--------------------------------------------------------------

    override getChart(records: INavReportCompareWithLookup[]): EChartsOption {
        // return this.getDemoChart();
        // reorder records in reverse order to show same order of table records.
        let reversedRecords = records.reverse();
        let baseChart = this.getBaseChart();
        baseChart.yAxis = {
            data: reversedRecords.map(x => x['ssPortfolio_Name' as keyof INavReportCompareWithLookup] ?? "unknown"),

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

        ]
            ;

        return baseChart;
    }

}

