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
import { IQueryColumn, IEntity, IAction } from '@bfs/_shared/interfaces';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- component specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { MasterService } from '@bfs/master-main/master.service';
import { type IBfsTenantSystemRequest, type IBfsTenantSystemFilter, initBfsTenantSystemRequest, renderBfsTenantSystem, getBfsTenantSystemActions} from './bfs-tenant-system.shared';
import { BfsTenantSystemFilterComponent } from './bfs-tenant-system.filter.component'; 

@Component({
    selector: 'bfs-tenant-system-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class BfsTenantSystemListComponent         

    extends BaseReportComponent<IBfsTenantSystemFilter,null> {
    override apiService: MasterService = inject(MasterService);
    override queryRequest = {} as IBfsTenantSystemRequest;
    override exportRequest = {} as IBfsTenantSystemRequest;
    override downloadFileName: string = "Tenant - System";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/mstr/bfs-tenant-system/add/0", displayText: "Add New Tenant - System" };
        this.getApiUrl = '/BfsTenantSystem/List';
        this.uploadApiUrl = '/BfsTenantSystem/upload';

        this.filterComponent = BfsTenantSystemFilterComponent;
        this.queryRequest = initBfsTenantSystemRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderBfsTenantSystem(record, column);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getBfsTenantSystemActions(this, record);
    }

}

