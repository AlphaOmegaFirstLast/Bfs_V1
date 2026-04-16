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
import { AuthService } from '@bfs/auth-main/auth.service';
import { RoleComponentSystemActionFilterComponent } from '../role-component-system-action/role-component-system-action.filter.component'; 
import { type IRoleComponentSystemActionWithLookup, type IRoleComponentSystemActionRequest, type IRoleComponentSystemActionFilter, initRoleComponentSystemActionRequest } from '../role-component-system-action/role-component-system-action.shared';

@Component({
    selector: 'role-component-system-action-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class RoleComponentSystemActionMatrixComponent extends BaseMatrixComponent <IRoleComponentSystemActionWithLookup, IRoleComponentSystemActionFilter> {

    override apiService: AuthService = inject(AuthService);
    override queryRequest = {} as IRoleComponentSystemActionRequest;
    override list: IRoleComponentSystemActionWithLookup[] = [];

    override title: string = 'BfsComponent ⌄ | SystemAction >' ; // to be set from outside    

    override parentId: string =  "roleId"; 
    override horizontalId:string = 'systemActionId';
    override verticalId:string = 'bfsComponentId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/RoleComponentSystemAction/list';
        this.saveApiUrl = '/Operations/RoleComponentSystemAction/matrix';

        this.getHorizontalApiUrl = '/SystemAction/list';
        this.getVerticalApiUrl = '/BfsComponent/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = RoleComponentSystemActionFilterComponent;
        this.queryRequest = initRoleComponentSystemActionRequest();
    }
    //---------------------------------------------------------
}
