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

import { ComponentSystemActionFilterComponent } from './component-system-action.filter.component'; //ToDoReport
import { type IComponentSystemActionWithLookup, type IComponentSystemActionRequest, type IComponentSystemActionFilter, initComponentSystemActionRequest } from './component-system-action.shared';

@Component({
    selector: 'component-system-action-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class ComponentSystemActionListComponent      //ToDoReport ComponentSystemActionReportComponent
    extends BaseReportComponent<IComponentSystemActionFilter, IComponentSystemActionWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IComponentSystemActionRequest;
    override exportRequest = {} as IComponentSystemActionRequest;
    override list: IComponentSystemActionWithLookup[] = [];
    override downloadFileName: string = "Component - System Actions";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/ComponentSystemActionReport';
        this.getApiUrl = '/ComponentSystemAction/list';
        this.getByIdApiUrl = '/ComponentSystemAction/';
        this.uploadApiUrl = '/ComponentSystemAction/upload';
        this.queryOwner = "component-system-action".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/component-system-action/add/0", displayText: "Add New Component - System Actions" };

        this.filterComponent = ComponentSystemActionFilterComponent;
        this.queryRequest = initComponentSystemActionRequest();
    }
    //---------------------------------------------------------
    override render(record: IComponentSystemActionWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IComponentSystemActionWithLookup];
        switch (column.fieldName) {
            case 'componentId':
                return record.component?.toString();
case 'systemActionId':
                return record.systemAction?.toString();
case 'actionLocationId':
                return record.actionLocation?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IComponentSystemActionWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/component-system-action/view", displayText: "View..." },
            { recordId: record.id, route: "/component-system-action/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/component-system-action/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IComponentSystemActionWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.componentId, route: "/component/view", displayText: "Component Name" },
{ recordId: record.systemActionId, route: "/system-action/view", displayText: "Menu Action" },
{ recordId: record.actionLocationId, route: "/action-location/view", displayText: "Menu Action" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IComponentSystemActionWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/ComponentSystemAction", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/ComponentSystemAction/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/ComponentSystemAction/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

