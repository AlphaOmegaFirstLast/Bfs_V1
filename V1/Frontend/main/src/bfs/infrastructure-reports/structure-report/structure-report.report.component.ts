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
import { type IColumns, formatFilter, IUIMessage, IQueryColumn, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

import { type IStructureReportWithLookup, type IStructureReportRequest, type IStructureReportFilter } from './structure-report.shared';
import {  initStructureReportRequest } from './structure-report.shared';
import { StructureReportFilterComponent } from './structure-report.filter.component'; 
@Component({
    selector: 'structure-report-compare',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class StructureReportCompareComponent         

    extends BaseReportComponent<IStructureReportFilter, IStructureReportWithLookup> {
    override apiService: InfrastructureService = inject(InfrastructureService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IStructureReportRequest;
    override exportRequest = {} as IStructureReportRequest;
 //   override list: IQueryColumn ; //IStructureReportWithLookup[] = [];
    override downloadFileName: string = "Structure Report";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/bfs/reports/StructureReportCompare';

        this.filterComponent = StructureReportFilterComponent;
        this.queryRequest = initStructureReportRequest();
    }
    //---------------------------------------------------------
    override render(record: IQueryColumn, column: IColumns): any {
        const value = record[column.fieldName as keyof IQueryColumn];
        switch (column.fieldName) {
            case 'bfsComponentDataTypeId':
                return record['dataTypeName?'].toString();

            case 'countId':
                return record['countId?'].toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

//--------------------------------------------------------------
override getChart(records: IStructureReportWithLookup[]): EChartsOption {
        // return this.getDemoChart();
        // reorder records in reverse order to show same order of table records.
        let reversedRecords = records.reverse();
        let baseChart = this.getBaseChart();
        baseChart.yAxis = {
        data: reversedRecords.map(x => x['bfsComponentDisplayName' as keyof IStructureReportWithLookup] ?? "unknown"),

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
            name: 'Fields Count Per Component',

            type: 'bar',
            barWidth: "10px",
            barGap: "0.25",
            z: 10,
            itemStyle: {
                borderRadius: [4, 4, 0, 0], color: getColor('info')
            },
            data: reversedRecords.map(x => x.countId),
        }

        ]
        ;

        return baseChart;
    }

}

