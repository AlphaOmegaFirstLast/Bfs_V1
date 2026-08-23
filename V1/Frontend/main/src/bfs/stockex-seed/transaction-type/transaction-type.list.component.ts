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
import { StockExService } from '@bfs/stockex-main/stockex.service';

import { type ITransactionTypeWithLookup, type ITransactionTypeRequest, type ITransactionTypeFilter } from './transaction-type.shared';
import { getTransactionTypeActions,  initTransactionTypeRequest } from './transaction-type.shared';
import { TransactionTypeFilterComponent } from './transaction-type.filter.component'; 

@Component({
    selector: 'transaction-type-list',     
    imports: [ CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,
               NgbAlertModule, NgbProgressbarModule, RouterLink, ExportComponent,
               NgxEchartsDirective],
    providers: [provideEchartsCore({ echarts })],
    standalone: true,
    templateUrl: '../../_shared/components/base-report.component.html',
})
export class TransactionTypeListComponent         

    extends BaseReportComponent<ITransactionTypeFilter, ITransactionTypeWithLookup> {
    override apiService: StockExService = inject(StockExService);
    override queryRequest = {} as ITransactionTypeRequest;
    override exportRequest = {} as ITransactionTypeRequest;
    override downloadFileName: string = "Transaction Types";

    //------------------------------------------------------
    constructor(modalService: NgbModal, router: Router, excelService: ExcelExportService, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, excelService, activatedRoute);

        this.isButton.chart = false;
        this.addNewRecordLink = { route: "/stkx/transaction-type/add/0", displayText: "Add New Transaction Types" };
        this.getApiUrl = '/TransactionType/List';
        this.uploadApiUrl = '/TransactionType/upload';

        this.filterComponent = TransactionTypeFilterComponent;
        this.queryRequest = initTransactionTypeRequest();
    }
    //---------------------------------------------------------
    override render(record: IEntity, column: IColumns): any {
        const value = record[column.fieldName as keyof IQueryColumn];
        switch (column.fieldName) {
            case 'effectTypeId':
                return record['effectTypeName']?.toString();
case 'stockEntityTypeId':
                return record['stockEntityTypeName']?.toString();
case 'calculationMethodId':
                return record['calculationMethodName']?.toString();
case 'sourceTypeId':
                return record['sourceTypeName']?.toString();
case 'stockFieldTypeId':
                return record['stockFieldTypeName']?.toString();
case 'nextTransactionTypeId':
                return record['nextTransactionTypeName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------

    override getRecordLinks(record: IEntity): ViewLink[] {
        let actions = getTransactionTypeActions(this,record);
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
        let actions = getTransactionTypeActions(this,record);
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

