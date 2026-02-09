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
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';
import { BfsComponentSystemActionFilterComponent } from '../bfs-component-system-action/bfs-component-system-action.filter.component'; 
import { type IBfsComponentSystemActionWithLookup, type IBfsComponentSystemActionRequest, type IBfsComponentSystemActionFilter, initBfsComponentSystemActionRequest } from '../bfs-component-system-action/bfs-component-system-action.shared';

@Component({
    selector: 'bfs-component-system-action-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class BfsComponentSystemActionMatrixComponent extends BaseMatrixComponent <IBfsComponentSystemActionWithLookup, IBfsComponentSystemActionFilter> {

    override apiService: InfrastructureService = inject(InfrastructureService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IBfsComponentSystemActionRequest;
    override list: IBfsComponentSystemActionWithLookup[] = [];

    override title: string = 'SystemAction ⌄ | ActionLocation >' ; // to be set from outside    

    override parentId: string =  "bfsComponentId"; 
    override horizontalId:string = 'actionLocationId';
    override verticalId:string = 'systemActionId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/BfsComponentSystemAction/list';
        this.saveApiUrl = '/Operations/BfsComponentSystemAction/matrix';

        this.getHorizontalApiUrl = '/ActionLocation/list';
        this.getVerticalApiUrl = '/SystemAction/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = BfsComponentSystemActionFilterComponent;
        this.queryRequest = initBfsComponentSystemActionRequest();
    }
    //---------------------------------------------------------
}
