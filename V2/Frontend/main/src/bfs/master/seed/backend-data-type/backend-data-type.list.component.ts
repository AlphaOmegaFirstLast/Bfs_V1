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
import { type IBackendDataTypeRequest, type IBackendDataTypeFilter, initBackendDataTypeRequest, renderBackendDataType, getBackendDataTypeActions} from './backend-data-type.shared';
import { BackendDataTypeFilterComponent } from './backend-data-type.filter.component'; 
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'backend-data-type-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class BackendDataTypeListComponent         

    extends BaseReportComponent<IBackendDataTypeFilter,null> {
    override apiService: MasterService = inject(MasterService);
    override queryRequest = {} as IBackendDataTypeRequest;
    override exportRequest = {} as IBackendDataTypeRequest;
    override downloadFileName: string = "Backend Data Types";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/mstr/backend-data-type/add/0", displayText: "Add New Backend Data Types" };
        this.getApiUrl = '/BackendDataType/List';
        this.uploadApiUrl = '/BackendDataType/upload';

        this.filterComponent = BackendDataTypeFilterComponent;
        this.queryRequest = initBackendDataTypeRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderBackendDataType(record, column);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getBackendDataTypeActions(this, record);
    }

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}

