//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { SafeHtml } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';
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
import { type IProcessStatus, type IProcessStatusRequest, type IProcessStatusFilter, initProcessStatusRequest, renderProcessStatus, getProcessStatusActions} from './process-status.shared';
import { ProcessStatusFilterComponent } from './process-status.filter.component'; 
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'process-status-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts }), DatePipe],
    standalone: true,
    templateUrl: '../../../_shared/components/base-report.component.html',
})
export class ProcessStatusListComponent         

    extends BaseReportComponent<IProcessStatusFilter> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as IProcessStatusRequest;
    override exportRequest = {} as IProcessStatusRequest;
    override downloadFileName: string = "Process Status";
//Template_Start_Code_DontOverwrite_2
    private datePipe = inject(DatePipe);
//Template_End_Code_DontOverwrite_2

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/stkx/process-status/add/0", displayText: "Add New Process Status" };
        this.getApiUrl = '/ProcessStatus/List';
        this.uploadApiUrl = '/ProcessStatus/upload';

        this.filterComponent = ProcessStatusFilterComponent;
        this.queryRequest = initProcessStatusRequest();
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
    }
    //---------------------------------------------------------
//Template_Start_Code_DontOverwrite_4
    override isSafeHtml(field: string): boolean {
        return false;
    } 
//Template_End_Code_DontOverwrite_4
   //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderProcessStatus(record, column, this.datePipe);
    }
    //---------------------------------------------------------
    override safeHtmlHeaders(field: string): SafeHtml {
        let column = this.queryRequest.columns.find(x => x.fieldName == field) || {} as IQueryColumn;
        return column.displayName;
    }
    //---------------------------------------------------------
    override safeHtmlData(record: any, field: string): SafeHtml {
        let column = this.queryRequest.columns.find(x => x.fieldName == field) || {} as IQueryColumn;
        return renderProcessStatus(record, column, this.datePipe, this.sanitizer);

    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getProcessStatusActions(this, record);
    }

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

}

