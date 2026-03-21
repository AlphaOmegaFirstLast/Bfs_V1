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
import { AuthService } from '@bfs/auth-main/auth.service';
import { AuthRoleComponentSystemActionFilterComponent } from '../auth-role-component-system-action/auth-role-component-system-action.filter.component'; 
import { type IAuthRoleComponentSystemActionWithLookup, type IAuthRoleComponentSystemActionRequest, type IAuthRoleComponentSystemActionFilter, initAuthRoleComponentSystemActionRequest } from '../auth-role-component-system-action/auth-role-component-system-action.shared';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

@Component({
    selector: 'auth-role-component-system-action-matrix',      
    imports: [CommonModule, NgIcon, NgbDropdownModule, NgbPaginationModule,NgbAlertModule, NgbProgressbarModule],
    standalone: true,
    templateUrl: '../../_shared/components/base-matrix.component.html',
})
export class AuthRoleComponentSystemActionMatrixComponent extends BaseMatrixComponent <IAuthRoleComponentSystemActionWithLookup, IAuthRoleComponentSystemActionFilter> {

    bfsService: InfrastructureService = inject(InfrastructureService);
    override apiHorizontalService = this.bfsService;
    override apiVerticalService = this.bfsService;

    override apiService: AuthService = inject(AuthService);
    override tokenService: TokenService = inject(TokenService);
    override queryRequest = {} as IAuthRoleComponentSystemActionRequest;
    override list: IAuthRoleComponentSystemActionWithLookup[] = [];

    override title: string = 'BfsComponent ⌄ | SystemAction >' ; // to be set from outside    

    override parentId: string =  "authRoleId"; 
    override horizontalId:string = 'systemActionId';
    override verticalId:string = 'bfsComponentId';
    //------------------------------------------------------

    constructor(modalService: NgbModal, router: Router, activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        super(modalService, router, activatedRoute);

        this.getApiUrl = '/AuthRoleComponentSystemAction/list';
        this.saveApiUrl = '/Operations/AuthRoleComponentSystemAction/matrix';

        this.getHorizontalApiUrl = '/SystemAction/list';
        this.getVerticalApiUrl = '/BfsComponent/list';

        this.isButton.chart = false;
        this.isButton.addNew = false;
        this.filterComponent = AuthRoleComponentSystemActionFilterComponent;
        this.queryRequest = initAuthRoleComponentSystemActionRequest();
    }
    //---------------------------------------------------------
}
