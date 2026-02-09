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
import { type IColumns, formatFilter, IUIMessage, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

import { type IDataType1WithLookup, type IDataType1Request, type IDataType1Filter, initDataType1Request } from './data-type1.shared';
import { DataType1FilterComponent } from './data-type1.report.filter.component'; //ToDoReport .report

@Component({
selector: 'data-type1-report',

    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class DataType1ReportComponent

    extends BaseReportComponent<IDataType1Filter, IDataType1WithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IDataType1Request;
    override exportRequest = {} as IDataType1Request;
    override list: IDataType1WithLookup[] = [];
    override downloadFileName: string = "DataType1 List";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/reports/DataType1Report';

        this.filterComponent = DataType1FilterComponent;
        this.queryRequest = initDataType1Request();
    }
    //---------------------------------------------------------
    override render(record: IDataType1WithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IDataType1WithLookup];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

    override getChart(records: IDataType1WithLookup[]): EChartsOption {
        // return this.getDemoChart();
        // reorder records in reverse order to show same order of table records.
        let reversedRecords = records.reverse();
        let baseChart = this.getBaseChart();
        baseChart.yAxis = {

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

