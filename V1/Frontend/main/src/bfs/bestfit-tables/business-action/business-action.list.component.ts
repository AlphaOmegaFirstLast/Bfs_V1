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
import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/bestfit-main/bestfit.operations';

//--------------- omponent specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

import { BusinessActionFilterComponent } from './business-action.filter.component'; //ToDoReport
import { type IBusinessActionWithLookup, type IBusinessActionRequest, type IBusinessActionFilter, initBusinessActionRequest } from './business-action.shared';

@Component({
    selector: 'business-action-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class BusinessActionListComponent      //ToDoReport BusinessActionReportComponent
    extends BaseReportComponent<IBusinessActionFilter, IBusinessActionWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IBusinessActionRequest;
    override exportRequest = {} as IBusinessActionRequest;
    override list: IBusinessActionWithLookup[] = [];
    override downloadFileName: string = "Business Actions";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/BusinessActionReport';
        this.getApiUrl = '/BusinessAction/list';
        this.getByIdApiUrl = '/BusinessAction/';
        this.uploadApiUrl = '/BusinessAction/upload';
        this.queryOwner = "business-action".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/business-action/add/0", displayText: "Add New Business Actions" };

        this.filterComponent = BusinessActionFilterComponent;
        this.queryRequest = initBusinessActionRequest();
    }
    //---------------------------------------------------------
    override render(record: IBusinessActionWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IBusinessActionWithLookup];
        switch (column.fieldName) {
            case 'actionTypeId':
                return record.actionType?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IBusinessActionWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/business-action/view", displayText: "View..." },
            { recordId: record.id, route: "/business-action/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/business-action/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IBusinessActionWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.actionTypeId, route: "/action-type/view", displayText: "Action Type" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IBusinessActionWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/BusinessAction", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/BusinessAction/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/BusinessAction/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

