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

import { FilterTypeFilterComponent } from './filter-type.filter.component'; //ToDoReport
import { type IFilterTypeWithLookup, type IFilterTypeRequest, type IFilterTypeFilter, initFilterTypeRequest } from './filter-type.shared';

@Component({
    selector: 'filter-type-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class FilterTypeListComponent      //ToDoReport FilterTypeReportComponent
    extends BaseReportComponent<IFilterTypeFilter, IFilterTypeWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IFilterTypeRequest;
    override exportRequest = {} as IFilterTypeRequest;
    override list: IFilterTypeWithLookup[] = [];
    override downloadFileName: string = "Filter Types";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/FilterTypeReport';
        this.getApiUrl = '/FilterType/list';
        this.getByIdApiUrl = '/FilterType/';
        this.uploadApiUrl = '/FilterType/upload';
        this.queryOwner = "filter-type".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/filter-type/add/0", displayText: "Add New Filter Types" };

        this.filterComponent = FilterTypeFilterComponent;
        this.queryRequest = initFilterTypeRequest();
    }
    //---------------------------------------------------------
    override render(record: IFilterTypeWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof IFilterTypeWithLookup];
        switch (column.fieldName) {

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: IFilterTypeWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/filter-type/view", displayText: "View..." },
            { recordId: record.id, route: "/filter-type/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/filter-type/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: IFilterTypeWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: IFilterTypeWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/FilterType", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/FilterType/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/FilterType/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

