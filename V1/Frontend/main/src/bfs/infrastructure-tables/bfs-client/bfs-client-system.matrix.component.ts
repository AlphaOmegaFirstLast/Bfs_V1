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
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';
import { BfsClientSystemFilterComponent } from '../bfs-client-system/bfs-client-system.filter.component'; 
import { type IBfsClientSystemWithLookup, type IBfsClientSystemRequest, type IBfsClientSystemFilter, initBfsClientSystemRequest } from '../bfs-client-system/bfs-client-system.shared';

@Component({
    selector: 'bfs-client-system-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class BfsClientSystemMatrixComponent extends BaseMatrixComponent <IBfsClientSystemWithLookup, IBfsClientSystemFilter> {

    override apiService: InfrastructureService = inject(InfrastructureService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IBfsClientSystemRequest;
    override list: IBfsClientSystemWithLookup[] = [];

    override title: string = 'BfsSystem ⌄ | BfsClient >' ; // to be set from outside    

    override parentId: string =  "bfsClientId"; 
    override horizontalId:string = 'bfsClientId';
    override verticalId:string = 'bfsSystemId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/BfsClientSystem/list';
        this.saveApiUrl = '/Operations/BfsClientSystem/matrix';

        this.getHorizontalApiUrl = '/BfsClient/list';
        this.getVerticalApiUrl = '/BfsSystem/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = BfsClientSystemFilterComponent;
        this.queryRequest = initBfsClientSystemRequest();
    }
    //---------------------------------------------------------
}
