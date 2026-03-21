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
import { TokenService } from '@bfs/_shared/security/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/bestfit-main/bestfit.operations';

//--------------- omponent specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

import { BackendDataTypeFilterComponent } from './backend-data-type.filter.component'; //ToDoReport
import { type IBackendDataTypeWithLookup, type IBackendDataTypeRequest, type IBackendDataTypeFilter, initBackendDataTypeRequest } from './backend-data-type.shared';

@Component({
    selector: 'backend-data-type-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class BackendDataTypeListComponent      //ToDoReport BackendDataTypeReportComponent
    extends BaseReportComponent<IBackendDataTypeFilter, IBackendDataTypeWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IBackendDataTypeRequest;
    override exportRequest = {} as IBackendDataTypeRequest;
    override list: IBackendDataTypeWithLookup[] = [];
    override downloadFileName: string = "Backend Data Types";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/BackendDataTypeReport';
        this.getApiUrl = '/BackendDataType/list';
        this.getByIdApiUrl = '/BackendDataType/';
        this.uploadApiUrl = '/BackendDataType/upload';
        this.queryOwner = "backend-data-type".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/backend-data-type/add/0", displayText: "Add New Backend Data Types" };

        this.filterComponent = BackendDataTypeFilterComponent;
        this.queryRequest = initBackendDataTypeRequest();
    }
    //---------------------------------------------------------
    override render(record: IBackendDataTypeWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IBackendDataTypeWithLookup];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IBackendDataTypeWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/backend-data-type/view", displayText: "View..." },
            { recordId: record.id, route: "/backend-data-type/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/backend-data-type/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IBackendDataTypeWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IBackendDataTypeWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/BackendDataType", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/BackendDataType/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/BackendDataType/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

