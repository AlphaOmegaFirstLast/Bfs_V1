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
import { TokenService } from '@bfs/_shared/services/token.service';
import { BaseMatrixComponent } from '@bfs/_shared/components/base-matrix';
//--------------- component specific ------------------------------
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';
import { ComponentSystemActionFilterComponent } from '../component-system-action/component-system-action.filter.component'; 
import { type IComponentSystemActionWithLookup, type IComponentSystemActionRequest, type IComponentSystemActionFilter, initComponentSystemActionRequest } from '../component-system-action/component-system-action.shared';

@Component({
    selector: 'component-system-action-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class ComponentSystemActionMatrixComponent extends BaseMatrixComponent <IComponentSystemActionWithLookup, IComponentSystemActionFilter> {

    override apiService: BestFitService = inject(BestFitService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IComponentSystemActionRequest;
    override list: IComponentSystemActionWithLookup[] = [];

    override title: string = 'ActionLocation | SystemAction' ; // to be set from outside    

    override parentId: string =  "componentId"; 
    override horizontalId:string = 'actionLocationId';
    override verticalId:string = 'systemActionId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/ComponentSystemAction/list';
        this.saveApiUrl = '/Operations/ComponentSystemAction/matrix';

        this.getHorizontalApiUrl = '/ActionLocation/list';
        this.getVerticalApiUrl = '/SystemAction/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = ComponentSystemActionFilterComponent;
        this.queryRequest = initComponentSystemActionRequest();
    }
    //---------------------------------------------------------
}