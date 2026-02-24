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
import { type IColumns, formatFilter, IUIMessage, ViewLink, ActionLink, IQueryColumn } from '@bfs/_shared/interfaces';
import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';
//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';
import { CustomReportsFilterComponent } from './custom-reports-filter.component'; //ToDoReport
import { type ICustomReportsWithLookup, type ICustomReportsRequest, type ICustomReportsFilter, initCustomReportsRequest } from './custom-reports-shared';
@Component({
    selector: 'custom-reports-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class CustomReportsListComponent      //ToDoReport CustomReportsReportComponent
    extends BaseReportComponent<ICustomReportsFilter, ICustomReportsWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as ICustomReportsRequest;
    override exportRequest = {} as ICustomReportsRequest;
    override downloadFileName: string = "Custom Reports";
    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);
     //ToDoReport   this.getApiUrl = '/reports/CustomReportsReport';
        this.getApiUrl = '/CustomReports/list';
        this.getByIdApiUrl = '/CustomReports/';
        this.uploadApiUrl = '/CustomReports/upload';
        this.queryOwner = "custom-reports".split("-")[0];
        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/custom-reports/add/0", displayText: "Add New Custom Reports" };
        this.filterComponent = CustomReportsFilterComponent;
        this.queryRequest = initCustomReportsRequest();
    }
    //---------------------------------------------------------
    override render(record: IQueryColumn, column: IColumns): any {
        const value = record[column.fieldName as keyof IQueryColumn];
        switch (column.fieldName) {
            
            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    getRecordBusinessActions(record: ICustomReportsWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: this.duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/CustomReports/duplicateTree" } },
            { recordId: record.id, action: this.goToCustomReport, displayText: "Go To Custom Report", data: { postUrl: "/Operations/CustomReports/duplicateTree" } },
        ];
        return actionLinks;
    }
    //---------------------------------------------------------
}

