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
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';

//---------------------- Component Specific ------------------------
import { type IBfsClient, type IBfsClientRequest, initBfsClient, bfsClientUntypedFormGroup } from './bfs-client.shared';

@Component({
    selector: 'bfs-client-form',
    imports: [

        CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink],
    standalone: true,
    templateUrl: './bfs-client.form.component.html',
})
export class BfsClientFormComponent extends BaseFormComponent<IBfsClient> implements OnInit {

    override apiUrl = '/BfsClient/';
    override apiService: InfrastructureService = inject(InfrastructureService);
    override componentName: string = 'BfsClient'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(bfsClientUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls
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
    override initEntity(): IBfsClient {
        return initBfsClient();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        this.isLoading = true;
        let target = '';

    }
    //---------------------------------------------------------
    // getValidationErrorMessage(fieldName: string, control: any): string {
    //     if (!control || !control.errors || !control.touched) return '';

    //     // Find the first error key (e.g., 'required')
    //     const firstErrorKey = Object.keys(control.errors)[0];

    //     if (firstErrorKey.includes('required')) return `${fieldName} is required.`;
    //     else if (firstErrorKey.includes('email')) return `${fieldName} is not a valid email address.`;
    //     else if (firstErrorKey.includes('minlength')) return `${fieldName} must be at least ${control.errors?.['minlength'].requiredLength} characters long.`;
    //     else if (firstErrorKey.includes('maxlength')) return `${fieldName} cannot be more than ${control.errors?.['maxlength'].requiredLength} characters long.`;
    //     else if (firstErrorKey.includes('minvalue')) return `${fieldName} must be at least ${control.errors?.['minvalue'].minValue} characters long.`;
    //     else if (firstErrorKey.includes('maxvalue')) return `${fieldName} must be at most ${control.errors?.['maxvalue'].maxValue} characters long.`;
    //     else if (firstErrorKey.includes('pattern')) return `${fieldName} has an invalid format.`;

    //     return `Invalid ${fieldName}.`;
    // }
    // //---------------------------------------------------------

}
