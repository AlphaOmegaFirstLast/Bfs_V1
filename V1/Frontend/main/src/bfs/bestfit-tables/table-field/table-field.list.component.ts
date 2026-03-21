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

import { TableFieldFilterComponent } from './table-field.filter.component'; //ToDoReport
import { type ITableFieldWithLookup, type ITableFieldRequest, type ITableFieldFilter, initTableFieldRequest } from './table-field.shared';

@Component({
    selector: 'table-field-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class TableFieldListComponent      //ToDoReport TableFieldReportComponent
    extends BaseReportComponent<ITableFieldFilter, ITableFieldWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as ITableFieldRequest;
    override exportRequest = {} as ITableFieldRequest;
    override list: ITableFieldWithLookup[] = [];
    override downloadFileName: string = "Table Fields";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/TableFieldReport';
        this.getApiUrl = '/TableField/list';
        this.getByIdApiUrl = '/TableField/';
        this.uploadApiUrl = '/TableField/upload';
        this.queryOwner = "table-field".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/table-field/add/0", displayText: "Add New Table Fields" };

        this.filterComponent = TableFieldFilterComponent;
        this.queryRequest = initTableFieldRequest();
    }
    //---------------------------------------------------------
    override render(record: ITableFieldWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof ITableFieldWithLookup];
        switch (column.fieldName) {
            case 'componentId':
                return record.component?.toString();
case 'filterTypeId':
                return record.filterType?.toString();
case 'backendDataTypeId':
                return record.backendDataType?.toString();
case 'formControlTypeId':
                return record.formControlType?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: ITableFieldWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/table-field/view", displayText: "View..." },
            { recordId: record.id, route: "/table-field/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/table-field/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: ITableFieldWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.componentId, route: "/component/view", displayText: "Component" },
{ recordId: record.filterTypeId, route: "/filter-type/view", displayText: "Filter Type" },
{ recordId: record.backendDataTypeId, route: "/backend-data-type/view", displayText: "Backend Type" },
{ recordId: record.formControlTypeId, route: "/form-control-type/view", displayText: "Form Control Type" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: ITableFieldWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/TableField", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/TableField/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/TableField/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

