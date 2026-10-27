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
import { type ICouponProcess, type ICouponProcessRequest, type ICouponProcessFilter, initCouponProcessRequest, renderCouponProcess, getCouponProcessActions } from './coupon-process.shared';
import { CouponProcessFilterComponent } from './coupon-process.filter.component';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'coupon-process-list',
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
        NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
        NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts }), DatePipe],
    standalone: true,
    templateUrl: '../../../_shared/components/base-report.component.html',
})
export class CouponProcessListComponent

    extends BaseReportComponent<ICouponProcessFilter> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as ICouponProcessRequest;
    override exportRequest = {} as ICouponProcessRequest;
    override downloadFileName: string = "Coupon Process";
    //Template_Start_Code_DontOverwrite_2
    private datePipe = inject(DatePipe);
    //Template_End_Code_DontOverwrite_2

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/stkx/coupon-process/add/0", displayText: "Add New Coupon Process" };
        this.getApiUrl = '/CouponProcess/List';
        this.uploadApiUrl = '/CouponProcess/upload';

        this.filterComponent = CouponProcessFilterComponent;
        this.queryRequest = initCouponProcessRequest();

        //Template_Start_Code_DontOverwrite_3
        this.isButton.addNew = false;
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
        return renderCouponProcess(record, column, this.datePipe);
    }
    //---------------------------------------------------------
    override safeHtmlHeaders(field: string): SafeHtml {
        let column = this.queryRequest.columns.find(x => x.fieldName == field) || {} as IQueryColumn;
        return column.displayName;
    }
    //---------------------------------------------------------
    override safeHtmlData(record: any, field: string): SafeHtml {
        let column = this.queryRequest.columns.find(x => x.fieldName == field) || {} as IQueryColumn;
        return renderCouponProcess(record, column, this.datePipe, this.sanitizer);

    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getCouponProcessActions(this, record);
    }

    //Template_Start_Code_DontOverwrite_5
    //Template_End_Code_DontOverwrite_5

}

