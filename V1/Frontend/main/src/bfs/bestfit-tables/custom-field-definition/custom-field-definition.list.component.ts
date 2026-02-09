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
import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { ExportComponent } from '@bfs/_shared/components/export.component';

//--------------- system specific ------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/bestfit-main/bestfit.operations';

//--------------- omponent specific ------------------------------
import { BaseReportComponent } from '@bfs/_shared/components/base-report';
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

import { CustomFieldDefinitionFilterComponent } from './custom-field-definition.filter.component'; //ToDoReport
import { type ICustomFieldDefinitionWithLookup, type ICustomFieldDefinitionRequest, type ICustomFieldDefinitionFilter, initCustomFieldDefinitionRequest } from './custom-field-definition.shared';

@Component({
    selector: 'custom-field-definition-list',      //ToDoReport  -report
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class CustomFieldDefinitionListComponent      //ToDoReport CustomFieldDefinitionReportComponent
    extends BaseReportComponent<ICustomFieldDefinitionFilter, ICustomFieldDefinitionWithLookup> {
    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as ICustomFieldDefinitionRequest;
    override exportRequest = {} as ICustomFieldDefinitionRequest;
    override list: ICustomFieldDefinitionWithLookup[] = [];
    override downloadFileName: string = "Custom Field Definitions";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

     //ToDoReport   this.getApiUrl = '/reports/CustomFieldDefinitionReport';
        this.getApiUrl = '/CustomFieldDefinition/list';
        this.getByIdApiUrl = '/CustomFieldDefinition/';
        this.uploadApiUrl = '/CustomFieldDefinition/upload';
        this.queryOwner = "custom-field-definition".split("-")[0];

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/custom-field-definition/add/0", displayText: "Add New Custom Field Definitions" };

        this.filterComponent = CustomFieldDefinitionFilterComponent;
        this.queryRequest = initCustomFieldDefinitionRequest();
    }
    //---------------------------------------------------------
    override render(record: ICustomFieldDefinitionWithLookup, column: IColumns): any {
        const value = record[column.fieldName as keyof ICustomFieldDefinitionWithLookup];
        switch (column.fieldName) {
            case 'componentId':
                return record.component?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    override getRecordLinks(record: ICustomFieldDefinitionWithLookup): ViewLink[] {
        let recordLinks: ViewLink[] = [
            { recordId: record.id, route: "/custom-field-definition/view", displayText: "View..." },
            { recordId: record.id, route: "/custom-field-definition/edit", displayText: "Edit..." },
            { recordId: record.id, route: "/custom-field-definition/delete", displayText: "Delete..." },
        ];

        return recordLinks;
    }
     //---------------------------------------------------------
     override getRecordLookupLinks(record: ICustomFieldDefinitionWithLookup): ViewLink[] {
        let viewLinks: ViewLink[] = [
         { recordId: record.componentId, route: "/component/view", displayText: "Component" },

        ];

        return viewLinks;
    }
     //---------------------------------------------------------
    override getListRecordActions(record: ICustomFieldDefinitionWithLookup): ActionLink[] {
        let actionLinks: ActionLink[] = [
            { recordId: record.id, action: duplicateRecord, displayText: "Duplicate Record", data: { postUrl: "/CustomFieldDefinition", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: duplicateTree, displayText: "Duplicate Tree", data: { postUrl: "/Operations/CustomFieldDefinition/duplicateTree", onSuccessMethodName: "getReport" } },
            { recordId: record.id, action: deleteTree, displayText: "Delete Tree", data: { deleteUrl: "/Operations/CustomFieldDefinition/fieldList", onSuccessMethodName: "getReport" } },
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
}

