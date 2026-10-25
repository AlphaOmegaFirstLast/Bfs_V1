//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { SafeHtml } from '@angular/platform-browser';

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
import { IQueryColumn, IEntity, IAction } from '@bfs/_shared/interfaces';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { StockExService } from '@bfs/stockex/main/stockex.service';
import { type IPortfolioProfitRepCompare, type IPortfolioProfitRepCompareRequest, type IPortfolioProfitRepCompareFilter, initPortfolioProfitRepCompareRequest, renderPortfolioProfitRepCompare, getPortfolioProfitRepCompareActions } from './portfolio-profit-rep-compare.shared';
import { PortfolioProfitRepCompareFilterComponent } from './portfolio-profit-rep-compare.filter.component';
//Template_Start_Code_DontOverwrite_1  
//Template_End_Code_DontOverwrite_1  

@Component({
    selector: 'portfolio-profit-rep-compare',
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
        NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
        NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../../_shared/components/base-report.component.html',
})
export class PortfolioProfitRepCompareComponent

    extends BaseReportComponent<IPortfolioProfitRepCompareFilter> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as IPortfolioProfitRepCompareRequest;
    override exportRequest = {} as IPortfolioProfitRepCompareRequest;
    override downloadFileName: string = "Portfolio Profit";
    //Template_Start_Code_DontOverwrite_2
    //Template_End_Code_DontOverwrite_2 

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/reports/PortfolioProfitRepCompare';

        this.filterComponent = PortfolioProfitRepCompareFilterComponent;
        this.queryRequest = initPortfolioProfitRepCompareRequest();
        //Template_Start_Code_DontOverwrite_3 
        //Template_End_Code_DontOverwrite_3 
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderPortfolioProfitRepCompare(record, column, this.sanitizer);
    }
    //---------------------------------------------------------
    override safeHtmlHeaders(field: string): SafeHtml {
        let column = this.queryRequest.columns.find(x => x.fieldName == field) || {} as IQueryColumn;
        return column.displayName;
    }
    //---------------------------------------------------------
    override safeHtmlData(record: any, field: string): SafeHtml {
        let column = this.queryRequest.columns.find(x => x.fieldName == field) || {} as IQueryColumn;
        return renderPortfolioProfitRepCompare(record, column, this.sanitizer);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getPortfolioProfitRepCompareActions(this, record);
    }

    //--------------------------------------------------------------

    override getChart(records: IPortfolioProfitRepCompare[]): EChartsOption {
        // return this.getDemoChart();
        // reorder records in reverse order to show same order of table records.
        let reversedRecords = records.reverse();
        let baseChart = this.getBaseChart();
        baseChart.yAxis = {
            data: reversedRecords.map(x => x['stockShareName' as keyof IPortfolioProfitRepCompare] ?? "unknown"),

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
                name: 'Profit',

                type: 'bar',
                barWidth: "10px",
                barGap: "0.25",
                z: 10,
                itemStyle: {
                    borderRadius: [4, 4, 0, 0], color: getColor('info')
                },
                data: reversedRecords.map(x => (x['currentPrice_Profit'] as unknown as number) ?? 0),
            }

        ]
            ;

        return baseChart;
    }

    //Template_Start_Code_DontOverwrite_4 
    //---------------------------------------------------------
    override isSafeHtml(field: string): boolean {
        return true;
    }
    //------------------------------------------------------
    //Template_End_Code_DontOverwrite_4  

}

