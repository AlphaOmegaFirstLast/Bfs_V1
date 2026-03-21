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

import { DeploymentLocalFilterComponent } from './deployment-local.filter.component'; //ToDoReport
import { type IDeploymentLocalWithLookup, type IDeploymentLocalRequest, type IDeploymentLocalFilter, initDeploymentLocalRequest } from './deployment-local.shared';

@Component({
    selector: 'deployment-local-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class DeploymentLocalListComponent      //ToDoReport DeploymentLocalReportComponent
    extends BaseReportComponent<IDeploymentLocalFilter, IDeploymentLocalWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IDeploymentLocalRequest;
    override exportRequest = {} as IDeploymentLocalRequest;
    override list: IDeploymentLocalWithLookup[] = [];
    override downloadFileName: string = "Local Deployment";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/DeploymentLocalReport';
        this.getApiUrl = '/DeploymentLocal/list';
        this.getByIdApiUrl = '/DeploymentLocal/';
        this.uploadApiUrl = '/DeploymentLocal/upload';
        this.queryOwner = "deployment-local".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/deployment-local/add/0", displayText: "Add New Local Deployment" };

        this.filterComponent = DeploymentLocalFilterComponent;
        this.queryRequest = initDeploymentLocalRequest();
    }
    //---------------------------------------------------------
    override render(record: IDeploymentLocalWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IDeploymentLocalWithLookup];
        switch (column.fieldName) {
            case 'systemInfoId':
                return record.systemInfo?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IDeploymentLocalWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/deployment-local/view", displayText: "View..." },
            { recordId: record.id, route: "/deployment-local/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/deployment-local/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IDeploymentLocalWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.systemInfoId, route: "/system-info/view", displayText: "System Info" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IDeploymentLocalWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/DeploymentLocal", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/DeploymentLocal/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/DeploymentLocal/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

