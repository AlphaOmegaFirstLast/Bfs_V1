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

import { SystemTemplateFilterComponent } from './system-template.filter.component'; //ToDoReport
import { type ISystemTemplateWithLookup, type ISystemTemplateRequest, type ISystemTemplateFilter, initSystemTemplateRequest } from './system-template.shared';

@Component({
    selector: 'system-template-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class SystemTemplateListComponent      //ToDoReport SystemTemplateReportComponent
    extends BaseReportComponent<ISystemTemplateFilter, ISystemTemplateWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as ISystemTemplateRequest;
    override exportRequest = {} as ISystemTemplateRequest;
    override list: ISystemTemplateWithLookup[] = [];
    override downloadFileName: string = "System Templates";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/SystemTemplateReport';
        this.getApiUrl = '/SystemTemplate/list';
        this.getByIdApiUrl = '/SystemTemplate/';
        this.uploadApiUrl = '/SystemTemplate/upload';
        this.queryOwner = "system-template".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/system-template/add/0", displayText: "Add New System Templates" };

        this.filterComponent = SystemTemplateFilterComponent;
        this.queryRequest = initSystemTemplateRequest();
    }
    //---------------------------------------------------------
    override render(record: ISystemTemplateWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof ISystemTemplateWithLookup];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: ISystemTemplateWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/system-template/view", displayText: "View..." },
            { recordId: record.id, route: "/system-template/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/system-template/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: ISystemTemplateWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: ISystemTemplateWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/SystemTemplate", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/SystemTemplate/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/SystemTemplate/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

