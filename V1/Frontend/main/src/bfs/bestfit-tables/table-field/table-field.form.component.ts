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
import { type ITableField, type ITableFieldRequest, initTableField, tableFieldUntypedFormGroup } from './table-field.shared';

@Component({
    selector: 'table-field-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './table-field.form.component.html',
})
export class TableFieldFormComponent extends BaseFormComponent<ITableField > implements OnInit {

    override apiUrl =  '/TableField/';
    override apiService: BestFitService = inject(BestFitService);
    override componentName: string = 'TableField'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public ComponentOptions: { id: number, name: string }[] = [];
public FilterTypeOptions: { id: number, name: string }[] = [];
public BackendDataTypeOptions: { id: number, name: string }[] = [];
public override FormControlTypeOptions: { id: number, name: string }[] = [];

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(tableFieldUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): ITableField  {
        return initTableField ();
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
                var msg = err.message || 'An error occurred while fetching Component data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/FilterType/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.FilterTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Filter Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/BackendDataType/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.BackendDataTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Backend Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });
target = "/FormControlType/list";
        (await this.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.FormControlTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.isLoading = false;
                var msg = err.message || 'An error occurred while fetching Form Control Type data.';
                this.messages.push({ text: msg, msgType: "danger" });
            }
        });

    }
    //---------------------------------------------------------

}
