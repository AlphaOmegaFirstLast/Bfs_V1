//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
//---------------- Ng Bootstrap ------------------------------
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core'
//---------------- bfs shared -------------------------------------
import { BaseMatrixComponent } from '@bfs/_shared/components/base-matrix';
//--------------- component specific ------------------------------
import { MasterService } from '@bfs/master-main/master.service';
import { BfsComponentBusinessActionFilterComponent } from '../bfs-component-business-action/bfs-component-business-action.filter.component'; 
import { type IBfsComponentBusinessActionWithLookup, type IBfsComponentBusinessActionRequest, type IBfsComponentBusinessActionFilter, initBfsComponentBusinessActionRequest } from '../bfs-component-business-action/bfs-component-business-action.shared';

@Component({
    selector: 'bfs-component-business-action-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class BfsComponentBusinessActionMatrixComponent extends BaseMatrixComponent <IBfsComponentBusinessActionWithLookup, IBfsComponentBusinessActionFilter> {

    override apiService: MasterService = inject(MasterService);
    override queryRequest = {} as IBfsComponentBusinessActionRequest;
    override list: IBfsComponentBusinessActionWithLookup[] = [];

    override title: string = 'BusinessAction ⌄ | ActionLocation >' ; // to be set from outside    

    override parentId: string =  "bfsComponentId"; 
    override horizontalId:string = 'actionLocationId';
    override verticalId:string = 'businessActionId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/BfsComponentBusinessAction/list';
        this.saveApiUrl = '/Operations/BfsComponentBusinessAction/matrix';

        this.getHorizontalApiUrl = '/ActionLocation/list';
        this.getVerticalApiUrl = '/BusinessAction/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = BfsComponentBusinessActionFilterComponent;
        this.queryRequest = initBfsComponentBusinessActionRequest();
    }
    //---------------------------------------------------------
}
