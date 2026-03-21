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

import { ComponentFilterComponent } from './component.filter.component'; //ToDoReport
import { type IComponentWithLookup, type IComponentRequest, type IComponentFilter, initComponentRequest } from './component.shared';

@Component({
    selector: 'component-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class ComponentListComponent      //ToDoReport ComponentReportComponent
    extends BaseReportComponent<IComponentFilter, IComponentWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IComponentRequest;
    override exportRequest = {} as IComponentRequest;
    override list: IComponentWithLookup[] = [];
    override downloadFileName: string = "Components";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/ComponentReport';
        this.getApiUrl = '/Component/list';
        this.getByIdApiUrl = '/Component/';
        this.uploadApiUrl = '/Component/upload';
        this.queryOwner = "component".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/component/add/0", displayText: "Add New Components" };

        this.filterComponent = ComponentFilterComponent;
        this.queryRequest = initComponentRequest();
    }
    //---------------------------------------------------------
    override render(record: IComponentWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IComponentWithLookup];
        switch (column.fieldName) {
            case 'systemInfoId':
                return record.systemInfo?.toString();
case 'dataTypeId':
                return record.dataType?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IComponentWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/component/view", displayText: "View..." },
            { recordId: record.id, route: "/component/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/component/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IComponentWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.systemInfoId, route: "/system-info/view", displayText: "System Info" },
{ recordId: record.dataTypeId, route: "/data-type/view", displayText: "Data Type" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IComponentWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/Component", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/Component/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/Component/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

