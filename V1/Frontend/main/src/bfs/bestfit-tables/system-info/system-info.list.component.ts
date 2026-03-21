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

import { SystemInfoFilterComponent } from './system-info.filter.component'; //ToDoReport
import { type ISystemInfoWithLookup, type ISystemInfoRequest, type ISystemInfoFilter, initSystemInfoRequest } from './system-info.shared';

@Component({
    selector: 'system-info-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class SystemInfoListComponent      //ToDoReport SystemInfoReportComponent
    extends BaseReportComponent<ISystemInfoFilter, ISystemInfoWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as ISystemInfoRequest;
    override exportRequest = {} as ISystemInfoRequest;
    override list: ISystemInfoWithLookup[] = [];
    override downloadFileName: string = "Systems";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/SystemInfoReport';
        this.getApiUrl = '/SystemInfo/list';
        this.getByIdApiUrl = '/SystemInfo/';
        this.uploadApiUrl = '/SystemInfo/upload';
        this.queryOwner = "system-info".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/system-info/add/0", displayText: "Add New Systems" };

        this.filterComponent = SystemInfoFilterComponent;
        this.queryRequest = initSystemInfoRequest();
    }
    //---------------------------------------------------------
    override render(record: ISystemInfoWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof ISystemInfoWithLookup];
        switch (column.fieldName) {
            case 'clientId':
                return record.client?.toString();
case 'systemTemplateId':
                return record.systemTemplate?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: ISystemInfoWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/system-info/view", displayText: "View..." },
            { recordId: record.id, route: "/system-info/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/system-info/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: ISystemInfoWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.clientId, route: "/client/view", displayText: "Client" },
{ recordId: record.systemTemplateId, route: "/system-template/view", displayText: "Template" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: ISystemInfoWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/SystemInfo", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/SystemInfo/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/SystemInfo/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

