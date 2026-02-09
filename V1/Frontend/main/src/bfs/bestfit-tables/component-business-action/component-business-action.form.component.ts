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
import { type IComponentBusinessAction, type IComponentBusinessActionRequest, initComponentBusinessAction, componentBusinessActionUntypedFormGroup } from './component-business-action.shared';

@Component({
    selector: 'component-business-action-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './component-business-action.form.component.html',
})
export class ComponentBusinessActionFormComponent extends BaseFormComponent<IComponentBusinessAction > implements OnInit {

    override apiUrl =  '/ComponentBusinessAction/';
    override apiService: BestFitService = inject(BestFitService);
    override componentName: string = 'ComponentBusinessAction'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public ComponentOptions: { id: number, name: string }[] = [];
public BusinessActionOptions: { id: number, name: string }[] = [];
public override ActionLocationOptions: { id: number, name: string }[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(componentBusinessActionUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IComponentBusinessAction  {
        return initComponentBusinessAction ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';
        target = "/Component/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.ComponentOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Component Name data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/BusinessAction/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.BusinessActionOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Business Action data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/ActionLocation/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.ActionLocationOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Menu Action data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

}
