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
import { type IColumns, formatFilter, IUIMessage, IQueryColumn, IEntity, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/master-main/master.operations';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { AuthService } from '@bfs/auth-main/auth.service';

import { type IRoleAppWithLookup, type IRoleAppRequest, type IRoleAppFilter } from './role-app.shared';
import { getRoleAppActions,  initRoleAppRequest } from './role-app.shared';
import { RoleAppFilterComponent } from './role-app.filter.component'; 

@Component({
    selector: 'role-app-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class RoleAppListComponent         

    extends BaseReportComponent<IRoleAppFilter, IRoleAppWithLookup> {
    override apiService: AuthService = inject(AuthService);
    override queryRequest = {} as IRoleAppRequest;
    override exportRequest = {} as IRoleAppRequest;
    override downloadFileName: string = "Role - Application";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/ath/role-app/add/0", displayText: "Add New Role - Application" };
        this.getApiUrl = '/RoleApp/List';
        this.uploadApiUrl = '/RoleApp/upload';

        this.filterComponent = RoleAppFilterComponent;
        this.queryRequest = initRoleAppRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IColumns): any {
        const value = record[column.fieldName as keyof IQueryColumn];
        switch (column.fieldName) {
            case 'roleId':
                return record['roleName']?.toString();
case 'appId':
                return record['appName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

    override getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getRoleAppActions(this,record);
        let links: ViewLink[] = actions.filter(action => 
               action.actionType == 'FrontendLink'
            && action.actionLocation == 'ListRow'
            ).map(action => {
            return { recordId: action.recordId, route: action.route?? '', displayText: action.displayText}
        });

        return links;
    }
    //---------------------------------------------------------
    override getRecordActions(record: IEntity): ActionLink[] {
        let actions = getRoleAppActions(this,record);
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

