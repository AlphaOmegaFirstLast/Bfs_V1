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
import { type ISystemInfo, type ISystemInfoRequest, initSystemInfo, systemInfoUntypedFormGroup } from './system-info.shared';

@Component({
    selector: 'system-info-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './system-info.form.component.html',
})
export class SystemInfoFormComponent extends BaseFormComponent<ISystemInfo > implements OnInit {

    override apiUrl =  '/SystemInfo/';
    override apiService: BestFitService = inject(BestFitService);
    override componentName: string = 'SystemInfo'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public ClientOptions: { id: number, name: string }[] = [];
public SystemTemplateOptions: { id: number, name: string }[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(systemInfoUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): ISystemInfo  {
        return initSystemInfo ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';
        target = "/Client/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.ClientOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Client data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/SystemTemplate/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.SystemTemplateOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Template data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

}
