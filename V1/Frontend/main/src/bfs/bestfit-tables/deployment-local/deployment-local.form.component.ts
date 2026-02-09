import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import {NgbPopoverModule} from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IQueryResponse, ILookup, IUIMessage } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

//---------------------- Component Specific ------------------------
import { type IDeploymentLocal, type IDeploymentLocalRequest, initDeploymentLocal, deploymentLocalUntypedFormGroup } from './deployment-local.shared';

@Component({
    selector: 'deployment-local-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './deployment-local.form.component.html',
})
export class DeploymentLocalFormComponent extends BaseFormComponent<IDeploymentLocal > implements OnInit {

    override apiUrl =  '/DeploymentLocal/';
    override apiService: BestFitService = inject(BestFitService);
    override componentName: string = 'DeploymentLocal'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public SystemInfoOptions: { id: number, name: string }[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(deploymentLocalUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
    }
    //---------------------------------------------------------
    override async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.getLookups();
        await this.getObjectFieldLookups();

        if (this.entity.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    override initEntity(): IDeploymentLocal  {
        return initDeploymentLocal ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';
        target = "/SystemInfo/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.SystemInfoOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching System Info data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

}
