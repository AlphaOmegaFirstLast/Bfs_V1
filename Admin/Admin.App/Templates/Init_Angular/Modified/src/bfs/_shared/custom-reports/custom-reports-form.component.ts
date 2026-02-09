import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, type UntypedFormGroup } from '@angular/forms';

import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { ClipboardService } from '@core/services/clipboard.service';

import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { BestFitService } from '@bfs/bestfit-main/bestfit.service';
import { type ICustomReports, type ICustomReportsRequest, initCustomReports, customReportsUntypedFormGroup } from './custom-reports-shared';



@Component({
    selector: 'custom-reports-user-form',
    imports: [
    
    CommonModule, NgIcon, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './custom-reports-form.component.html',
})
export class CustomReportsFormComponent implements OnInit {
    public apiUrl =  '/CustomReports/';
    apiService: BestFitService = inject(BestFitService);
    private clipboard = inject(ClipboardService);
    public formBuilder = inject(UntypedFormBuilder);
    public validationForm!: UntypedFormGroup;
    public route: ActivatedRoute;
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public isLoading: boolean = false;
    public currentOperation: string = '';
    public parent: any;

    // Init object
    customReports: ICustomReports = initCustomReports();

    // Children filters
    

    // Define look ups
    
    //---------------------------------------------------------

    constructor(private activatedRoute: ActivatedRoute) {
        this.route = activatedRoute;
        if (this.route.snapshot.url.length == 3) {
            this.currentOperation = this.route.snapshot.url[this.route.snapshot.url.length - 2].path;
            this.customReports.id = this.route.snapshot.url[this.route.snapshot.url.length - 1].path;
        }

        this.setValidation();
    }
    //---------------------------------------------------------
    async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getLookups();
        if (this.customReports.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    setChildrenRequests() {
        
    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        
    }
    //---------------------------------------------------------
    copyFromElement(el: HTMLElement) {
        const value = (el as HTMLInputElement).value || el.innerText;
        this.clipboard.highlightElementText(el);
        this.clipboard.copyText(value)
    }
    //---------------------------------------------------------
    async view() {
        this.isLoading = true;
        var target = this.apiUrl + this.customReports.id;
        (await this.apiService.get(target)).subscribe({
            next: response => {
                this.customReports = response;
                this.validationForm.patchValue(this.customReports);
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Custom Reports data.';
                this.isLoading = false;
            }
        });
    }
    //---------------------------------------------------------
    async add() {
        var target = this.apiUrl;
            (await this.apiService.post(target, this.customReports)).subscribe({
            next: response => {
                this.isLoading = false;
                this.submit = false; 
                this.validationForm.patchValue(this.customReports); 
                this.infoMessage = 'CustomReports added successfully:';
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while adding Custom Reports data.';
                this.isLoading = false;
            }
        });
    }
    //---------------------------------------------------------
    async update() {
        var target = this.apiUrl;
            (await this.apiService.put(target, this.customReports)).subscribe({
            next: response => {
                this.isLoading = false;
                this.submit = false; 
                this.validationForm.patchValue(this.customReports); 
                this.infoMessage = 'CustomReports updated successfully:'
            },
                error: (err: any) => {
                this.isLoading = false;
                this.errorMessage = err.message || 'An error occurred while updating Custom Reports data.';
            }
        });
    }
    //---------------------------------------------------------
    async delete() {
        var target = this.apiUrl + this.customReports.id;
        (await this.apiService.delete(target)).subscribe({
            next: response => {
                this.isLoading = false;
                this.submit = false; 
                this.customReports = initCustomReports();
                this.validationForm.patchValue(this.customReports); 
                this.infoMessage = 'CustomReports deleted successfully:'
            },
                error: (err: any) => {
                this.isLoading = false;
                this.errorMessage = err.message || 'An error occurred while deleting Custom Reports data.';
            }
        });
    }
    //---------------------------------------------------------
    setValidation(): void {
        this.validationForm = this.formBuilder.group(customReportsUntypedFormGroup);
    }
    //---------------------------------------------------------
    validSubmit() {
        this.submit = true
        if (this.validationForm.valid) {
            // One-time sync to customReports object
            this.customReports = this.validationForm.getRawValue();
            // this.validationForm.disable();
            this.infoMessage = '';
            this.errorMessage = '';
            if (!this.isLoading) {  // to prevent multiple requests
                this.isLoading = true;
                this.applyOperation();
            }
        }
    }
    //---------------------------------------------------------
    applyOperation() {
        switch (this.currentOperation) {
            case 'add':
                this.add();
                break;
            case 'edit':
                this.update();
                break;
            case 'view':
                this.view();
                break;
            case 'delete':
                this.delete();
                break;
            default:
                console.error('Unknown operation:', this.currentOperation);
                break;
        }
    }
    //---------------------------------------------------------
    get form() {
        return this.validationForm.controls
    }
    //---------------------------------------------------------
    resetForm() {
        this.submit = false
        this.customReports = initCustomReports();
        this.validationForm.patchValue(this.customReports);
    }
    //---------------------------------------------------------
}

