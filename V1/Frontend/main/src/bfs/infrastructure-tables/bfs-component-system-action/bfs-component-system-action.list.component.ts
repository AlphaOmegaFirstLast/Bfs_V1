//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
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
import { type IColumns, formatFilter, IUIMessage, IQueryColumn, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

import { type IBfsComponentSystemActionWithLookup, type IBfsComponentSystemActionRequest, type IBfsComponentSystemActionFilter } from './bfs-component-system-action.shared';
import { getBfsComponentSystemActionActions,  initBfsComponentSystemActionRequest } from './bfs-component-system-action.shared';
import { BfsComponentSystemActionFilterComponent } from './bfs-component-system-action.filter.component'; 

@Component({
    selector: 'bfs-component-system-action-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class BfsComponentSystemActionListComponent         

    extends BaseReportComponent<IBfsComponentSystemActionFilter, IBfsComponentSystemActionWithLookup> {
    override apiService: InfrastructureService = inject(InfrastructureService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IBfsComponentSystemActionRequest;
    override exportRequest = {} as IBfsComponentSystemActionRequest;
    private sanitizer: DomSanitizer = inject(DomSanitizer);
    override downloadFileName: string = "Component - System Actions";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/bfs/bfs-component-system-action/add/0", displayText: "Add New Component - System Actions" };
        this.getApiUrl = '/BfsComponentSystemAction/List';
        this.uploadApiUrl = '/BfsComponentSystemAction/upload';

        this.filterComponent = BfsComponentSystemActionFilterComponent;
        this.queryRequest = initBfsComponentSystemActionRequest();
    }
    //---------------------------------------------------------
    override render(record: IQueryColumn, column: IColumns): any {
        const value = record[column.fieldName as keyof IQueryColumn];
        switch (column.fieldName) {
            case 'bfsComponentId':
                return record['bfsComponentName']?.toString();
case 'systemActionId':
                return record['systemActionName']?.toString();
case 'actionLocationId':
                return record['actionLocationName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

override getRecordLinks(record: IQueryColumn): ViewLink[] {
        let actions = getBfsComponentSystemActionActions(record);
        let links: ViewLink[] = actions.filter(action => 
               action.actionType == 'FrontendLink'
            && action.actionLocation == 'ListRow'
            ).map(action => {
            return { recordId: action.recordId, route: action.route?? '', displayText: action.displayText}
        });

        return links;
    }
    //---------------------------------------------------------
    override getRecordActions(record: IQueryColumn): ActionLink[] {
        let actions = getBfsComponentSystemActionActions(record);
        let links: ActionLink[] = actions.filter(action => 
               action.actionType == 'FrontendFunction'
            && action.actionLocation == 'ListRow'
            ).map(action => {
            return { recordId: action.recordId, action: action.action?? null, displayText: action.displayText, data: action.data}
        });

        return links;
    }

//--------------------------------------------------------------

}

