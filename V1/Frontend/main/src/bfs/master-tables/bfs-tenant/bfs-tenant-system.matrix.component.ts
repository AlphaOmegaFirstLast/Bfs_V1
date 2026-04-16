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
import { BfsTenantSystemFilterComponent } from '../bfs-tenant-system/bfs-tenant-system.filter.component'; 
import { type IBfsTenantSystemWithLookup, type IBfsTenantSystemRequest, type IBfsTenantSystemFilter, initBfsTenantSystemRequest } from '../bfs-tenant-system/bfs-tenant-system.shared';

@Component({
    selector: 'bfs-tenant-system-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class BfsTenantSystemMatrixComponent extends BaseMatrixComponent <IBfsTenantSystemWithLookup, IBfsTenantSystemFilter> {

    override apiService: MasterService = inject(MasterService);
    override queryRequest = {} as IBfsTenantSystemRequest;
    override list: IBfsTenantSystemWithLookup[] = [];

    override title: string = 'BfsSystem ⌄ | BfsTenant >' ; // to be set from outside    

    override parentId: string =  "bfsTenantId"; 
    override horizontalId:string = 'bfsTenantId';
    override verticalId:string = 'bfsSystemId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/BfsTenantSystem/list';
        this.saveApiUrl = '/Operations/BfsTenantSystem/matrix';

        this.getHorizontalApiUrl = '/BfsTenant/list';
        this.getVerticalApiUrl = '/BfsSystem/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = BfsTenantSystemFilterComponent;
        this.queryRequest = initBfsTenantSystemRequest();
    }
    //---------------------------------------------------------
}
