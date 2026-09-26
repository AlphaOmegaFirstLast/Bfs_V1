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
import { MasterService } from '@bfs/master/main/master.service';
import { type IBfsSystemRequest, type IBfsSystemFilter, initBfsSystemRequest, renderBfsSystem, getBfsSystemActions} from './bfs-system.shared';
import { BfsSystemFilterComponent } from './bfs-system.filter.component'; 
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'bfs-system-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class BfsSystemListComponent         

    extends BaseReportComponent<IBfsSystemFilter,null> {
    override apiService: MasterService = inject(MasterService);
    override queryRequest = {} as IBfsSystemRequest;
    override exportRequest = {} as IBfsSystemRequest;
    override downloadFileName: string = "BestFit System";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/mstr/bfs-system/add/0", displayText: "Add New BestFit System" };
        this.getApiUrl = '/BfsSystem/List';
        this.uploadApiUrl = '/BfsSystem/upload';

        this.filterComponent = BfsSystemFilterComponent;
        this.queryRequest = initBfsSystemRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderBfsSystem(record, column);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getBfsSystemActions(this, record);
    }

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}

