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

import { ActionLocationFilterComponent } from './action-location.filter.component'; //ToDoReport
import { type IActionLocationWithLookup, type IActionLocationRequest, type IActionLocationFilter, initActionLocationRequest } from './action-location.shared';

@Component({
    selector: 'action-location-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class ActionLocationListComponent      //ToDoReport ActionLocationReportComponent
    extends BaseReportComponent<IActionLocationFilter, IActionLocationWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IActionLocationRequest;
    override exportRequest = {} as IActionLocationRequest;
    override list: IActionLocationWithLookup[] = [];
    override downloadFileName: string = "Action Locations";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/ActionLocationReport';
        this.getApiUrl = '/ActionLocation/list';
        this.getByIdApiUrl = '/ActionLocation/';
        this.uploadApiUrl = '/ActionLocation/upload';
        this.queryOwner = "action-location".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/action-location/add/0", displayText: "Add New Action Locations" };

        this.filterComponent = ActionLocationFilterComponent;
        this.queryRequest = initActionLocationRequest();
    }
    //---------------------------------------------------------
    override render(record: IActionLocationWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IActionLocationWithLookup];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IActionLocationWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/action-location/view", displayText: "View..." },
            { recordId: record.id, route: "/action-location/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/action-location/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IActionLocationWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IActionLocationWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/ActionLocation", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/ActionLocation/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/ActionLocation/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

