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
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

//--------------- omponent specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

import { SystemActionFilterComponent } from './system-action.filter.component'; //ToDoReport
import { type ISystemActionWithLookup, type ISystemActionRequest, type ISystemActionFilter, initSystemActionRequest } from './system-action.shared';

@Component({
    selector: 'system-action-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class SystemActionListComponent      //ToDoReport SystemActionReportComponent
    extends BaseReportComponent<ISystemActionFilter, ISystemActionWithLookup> {
    override apiService: InfrastructureService = inject(InfrastructureService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as ISystemActionRequest;
    override exportRequest = {} as ISystemActionRequest;
    override list: ISystemActionWithLookup[] = [];
    override downloadFileName: string = "System Actions";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/SystemActionReport';
        this.getApiUrl = '/SystemAction/list';
        this.getByIdApiUrl = '/SystemAction/';
        this.uploadApiUrl = '/SystemAction/upload';
        this.queryOwner = "system-action".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/system-action/add/0", displayText: "Add New System Actions" };

        this.filterComponent = SystemActionFilterComponent;
        this.queryRequest = initSystemActionRequest();
    }
    //---------------------------------------------------------
    override render(record: ISystemActionWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof ISystemActionWithLookup];
        switch (column.fieldName) {
            case 'actionTypeId':
                return record.actionType?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: ISystemActionWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/system-action/view", displayText: "View..." },
            { recordId: record.id, route: "/system-action/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/system-action/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: ISystemActionWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.actionTypeId, route: "/action-type/view", displayText: "Action Type" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: ISystemActionWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/SystemAction", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/SystemAction/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/SystemAction/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

