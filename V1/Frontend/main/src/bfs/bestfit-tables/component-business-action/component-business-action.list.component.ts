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

import { ComponentBusinessActionFilterComponent } from './component-business-action.filter.component'; //ToDoReport
import { type IComponentBusinessActionWithLookup, type IComponentBusinessActionRequest, type IComponentBusinessActionFilter, initComponentBusinessActionRequest } from './component-business-action.shared';

@Component({
    selector: 'component-business-action-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class ComponentBusinessActionListComponent      //ToDoReport ComponentBusinessActionReportComponent
    extends BaseReportComponent<IComponentBusinessActionFilter, IComponentBusinessActionWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IComponentBusinessActionRequest;
    override exportRequest = {} as IComponentBusinessActionRequest;
    override list: IComponentBusinessActionWithLookup[] = [];
    override downloadFileName: string = "Component - Business Actions";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/ComponentBusinessActionReport';
        this.getApiUrl = '/ComponentBusinessAction/list';
        this.getByIdApiUrl = '/ComponentBusinessAction/';
        this.uploadApiUrl = '/ComponentBusinessAction/upload';
        this.queryOwner = "component-business-action".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/component-business-action/add/0", displayText: "Add New Component - Business Actions" };

        this.filterComponent = ComponentBusinessActionFilterComponent;
        this.queryRequest = initComponentBusinessActionRequest();
    }
    //---------------------------------------------------------
    override render(record: IComponentBusinessActionWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IComponentBusinessActionWithLookup];
        switch (column.fieldName) {
            case 'componentId':
                return record.component?.toString();
case 'businessActionId':
                return record.businessAction?.toString();
case 'actionLocationId':
                return record.actionLocation?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IComponentBusinessActionWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/component-business-action/view", displayText: "View..." },
            { recordId: record.id, route: "/component-business-action/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/component-business-action/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IComponentBusinessActionWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.componentId, route: "/component/view", displayText: "Component Name" },
{ recordId: record.businessActionId, route: "/business-action/view", displayText: "Business Action" },
{ recordId: record.actionLocationId, route: "/action-location/view", displayText: "Menu Action" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IComponentBusinessActionWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/ComponentBusinessAction", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/ComponentBusinessAction/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/ComponentBusinessAction/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

