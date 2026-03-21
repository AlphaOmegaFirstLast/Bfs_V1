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
import { TokenService } from '@bfs/_shared/security/token.service';
import { BaseMatrixComponent } from '@bfs/_shared/components/base-matrix';
//--------------- component specific ------------------------------
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

import { ComponentBusinessActionFilterComponent } from '../component-business-action/component-business-action.filter.component'; 
import { type IComponentBusinessActionWithLookup, type IComponentBusinessActionRequest, type IComponentBusinessActionFilter, initComponentBusinessActionRequest } from '../component-business-action/component-business-action.shared';

@Component({
    selector: 'component-business-action-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class ComponentBusinessActionMatrixComponent extends BaseMatrixComponent <IComponentBusinessActionWithLookup, IComponentBusinessActionFilter> {

    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IComponentBusinessActionRequest;
    override list: IComponentBusinessActionWithLookup[] = [];

    override title: string = 'ActionLocation \ BusinessAction' ; // to be set from outside    

    override parentId: string =  "componentId"; 
    override horizontalId:string = 'actionLocationId';
    override verticalId:string = 'businessActionId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/ComponentBusinessAction/list';
        this.saveApiUrl = '/Operations/ComponentBusinessAction/matrix';

        this.getHorizontalApiUrl = '/ActionLocation/list';
        this.getVerticalApiUrl = '/BusinessAction/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = ComponentBusinessActionFilterComponent;
        this.queryRequest = initComponentBusinessActionRequest();
    }
    //---------------------------------------------------------
}

