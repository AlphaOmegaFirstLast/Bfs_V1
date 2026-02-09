import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IQueryResponse, ILookup, IUIMessage } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';

//---------------------- Component Specific ------------------------
import { type IComponent, type IComponentRequest, initComponent, componentUntypedFormGroup } from './component.shared';

import { TableFieldListComponent } from "../table-field/table-field.list.component"
import { ITableFieldFilter, ITableFieldRequest, initTableFieldRequest } from "../table-field/table-field.shared"

import { ComponentSystemActionMatrixComponent } from "./component-system-action.matrix.component"
import { IComponentSystemActionFilter, IComponentSystemActionRequest, initComponentSystemActionRequest } from "../component-system-action/component-system-action.shared"
import { ComponentBusinessActionMatrixComponent } from "./component-business-action.matrix.component"
import { IComponentBusinessActionFilter, IComponentBusinessActionRequest, initComponentBusinessActionRequest } from "../component-business-action/component-business-action.shared"

@Component({
    selector: 'component-form',
    imports: [
        TableFieldListComponent,
        ComponentSystemActionMatrixComponent,
        ComponentBusinessActionMatrixComponent,
        CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink],
    standalone: true,
    templateUrl: './component.form.component.html',
})
export class ComponentFormComponent extends BaseFormComponent<IComponent> implements OnInit {

    override apiUrl = '/Component/';
    override apiService: BestFitService = inject(BestFitService);
    override componentName: string = 'Component'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters
    presetTableFieldFilter: ITableFieldFilter | undefined;

    presetComponentSystemActionFilter: IComponentSystemActionFilter | undefined;
    presetComponentBusinessActionFilter: IComponentBusinessActionFilter | undefined;

    // Define look ups
    public SystemInfoOptions: { id: number, name: string }[] = [];
    public DataTypeOptions: { id: number, name: string }[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(componentUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IComponent {
        return initComponent();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {
        let presetTableFieldRequest: ITableFieldRequest = initTableFieldRequest();
        this.presetTableFieldFilter = presetTableFieldRequest.filter;
        if (this.presetTableFieldFilter) {
            this.presetTableFieldFilter.ComponentId = this.entity.id;
        }

        let presetComponentSystemActionRequest: IComponentSystemActionRequest = initComponentSystemActionRequest();
        this.presetComponentSystemActionFilter = presetComponentSystemActionRequest.filter;
        if (this.presetComponentSystemActionFilter) {
            this.presetComponentSystemActionFilter.ComponentId = this.entity.id;
        }
        let presetComponentBusinessActionRequest: IComponentBusinessActionRequest = initComponentBusinessActionRequest();
        this.presetComponentBusinessActionFilter = presetComponentBusinessActionRequest.filter;
        if (this.presetComponentBusinessActionFilter) {
            this.presetComponentBusinessActionFilter.ComponentId = this.entity.id;
        }

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';
        target = "/SystemInfo/list";
        (await this.apiService.post(target, { pageSize: 30 })).subscribe({
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
        target = "/DataType/list";
        (await this.apiService.post(target, { pageSize: 30 })).subscribe({
            next: (response: IQueryResponse) => {
                this.DataTypeOptions = response.items;
                this.isLoading = false;
            },
            error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Data Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

}
