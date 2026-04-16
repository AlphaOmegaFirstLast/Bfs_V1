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
import { type IColumns, formatFilter, IUIMessage, IQueryColumn,IEntity, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { StoresService } from '@bfs/stores-main/stores.service';

import { type IProductTransactionCompareWithLookup, type IProductTransactionCompareRequest, type IProductTransactionCompareFilter } from './product-transaction-compare.shared';
import { getProductTransactionCompareActions,  initProductTransactionCompareRequest } from './product-transaction-compare.shared';
import { ProductTransactionCompareFilterComponent } from './product-transaction-compare.filter.component'; 

@Component({
    selector: 'product-transaction-compare',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class ProductTransactionCompareComponent         

    extends BaseReportComponent<IProductTransactionCompareFilter, IProductTransactionCompareWithLookup> {
    override apiService: StoresService = inject(StoresService);
    override queryRequest = {} as IProductTransactionCompareRequest;
    override exportRequest = {} as IProductTransactionCompareRequest;
    override downloadFileName: string = "Transactions By Product";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.addNew = false;
        this.getApiUrl = '/reports/ProductTransactionCompare';

        this.filterComponent = ProductTransactionCompareFilterComponent;
        this.queryRequest = initProductTransactionCompareRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IColumns): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {

            case 'sumQuantity':
                return record['sumQuantity']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

   override getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getProductTransactionCompareActions(this,record);
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
        let actions = getProductTransactionCompareActions(this, record);
        let links: ActionLink[] = actions.filter(action => 
               action.actionType == 'FrontendFunction'
            && action.actionLocation == 'ListRow'
            ).map(action => {
            return { recordId: action.recordId, action: action.action?? null, displayText: action.displayText, data: action.data}
        });

        return links;
    }
//--------------------------------------------------------------

override getChart(records: IProductTransactionCompareWithLookup[]): EChartsOption {
        // return this.getDemoChart();
        // reorder records in reverse order to show same order of table records.
        let reversedRecords = records.reverse();
        let baseChart = this.getBaseChart();
        baseChart.yAxis = {
        data: reversedRecords.map(x => x['strProduct_Name' as keyof IProductTransactionCompareWithLookup] ?? "unknown"),

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
            name: 'Sum of Quantity',

            type: 'bar',
            barWidth: "10px",
            barGap: "0.25",
            z: 10,
            itemStyle: {
                borderRadius: [4, 4, 0, 0], color: getColor('info')
            },
            data: reversedRecords.map(x => x.sumQuantity),
        }

        ]
        ;

        return baseChart;
    }

}

