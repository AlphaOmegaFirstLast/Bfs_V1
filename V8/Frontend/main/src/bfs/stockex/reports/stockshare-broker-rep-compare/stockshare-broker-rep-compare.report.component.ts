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
import { IQueryColumn, IEntity, IAction } from '@bfs/_shared/interfaces';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { StockExService } from '@bfs/stockex/main/stockex.service';
import { type IStockshareBrokerRepCompare, type IStockshareBrokerRepCompareRequest, type IStockshareBrokerRepCompareFilter, initStockshareBrokerRepCompareRequest, renderStockshareBrokerRepCompare, getStockshareBrokerRepCompareActions} from './stockshare-broker-rep-compare.shared';
import { StockshareBrokerRepCompareFilterComponent } from './stockshare-broker-rep-compare.filter.component'; 
//Template_Start_Code_DontOverwrite_1  
//Template_End_Code_DontOverwrite_1  

@Component({
    selector: 'stockshare-broker-rep-compare',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../../_shared/components/base-report.component.html',
})
export class StockshareBrokerRepCompareComponent         

    extends BaseReportComponent<IStockshareBrokerRepCompareFilter> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as IStockshareBrokerRepCompareRequest;
    override exportRequest = {} as IStockshareBrokerRepCompareRequest;
    override downloadFileName: string = "Stockshare Broker Reports";
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2 

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/reports/StockshareBrokerRepCompare';

        this.filterComponent = StockshareBrokerRepCompareFilterComponent;
        this.queryRequest = initStockshareBrokerRepCompareRequest();
//Template_Start_Code_DontOverwrite_3 
//Template_End_Code_DontOverwrite_3 
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderStockshareBrokerRepCompare(record, column);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getStockshareBrokerRepCompareActions(this, record);
    }

//--------------------------------------------------------------

override getChart(records: IStockshareBrokerRepCompare[]): EChartsOption {
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

//Template_Start_Code_DontOverwrite_4 
//Template_End_Code_DontOverwrite_4  

}

