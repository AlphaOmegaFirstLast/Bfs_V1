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
import { type IAggregateTypeRequest, type IAggregateTypeFilter, initAggregateTypeRequest, renderAggregateType, getAggregateTypeActions} from './aggregate-type.shared';
import { AggregateTypeFilterComponent } from './aggregate-type.filter.component'; 
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'aggregate-type-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class AggregateTypeListComponent         

    extends BaseReportComponent<IAggregateTypeFilter,null> {
    override apiService: MasterService = inject(MasterService);
    override queryRequest = {} as IAggregateTypeRequest;
    override exportRequest = {} as IAggregateTypeRequest;
    override downloadFileName: string = "Aggregate Types";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/mstr/aggregate-type/add/0", displayText: "Add New Aggregate Types" };
        this.getApiUrl = '/AggregateType/List';
        this.uploadApiUrl = '/AggregateType/upload';

        this.filterComponent = AggregateTypeFilterComponent;
        this.queryRequest = initAggregateTypeRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IQueryColumn): any {
        return renderAggregateType(record, column);
    }
    //---------------------------------------------------------
    override getActions(record: IEntity): IAction[] {
        return getAggregateTypeActions(this, record);
    }

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}

