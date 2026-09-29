import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';
import { FlatpickrDirective } from 'angularx-flatpickr';
import { BaseFormComponent } from '@bfs/_shared/components/base-form.component';
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';

//---------------------- Component Specific ------------------------
import { isEnabled, isVisible, type ISspTransaction, initSspTransaction, sspTransactionUntypedFormGroup, getSspTransactionActions } from './ssp-transaction.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'ssp-transaction-form',
    imports: [

        CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule, RouterLink, FlatpickrDirective],
    standalone: true,
    templateUrl: './ssp-transaction.form.component.html',
})
export class SspTransactionFormComponent extends BaseFormComponent<ISspTransaction> implements OnInit {

    override apiUrl = '/SspTransaction/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'SspTransaction'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public SsPortfolioOptions: any[] = [];
    public TransactionTypeOptions: any[] = [];
    public StockShareOptions: any[] = [];
    public ToPortfolioOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

        super(activatedRoute);
        this.validationForm = this.formBuilder.group(sspTransactionUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

    }
    //---------------------------------------------------------
    override async ngOnInit(): Promise<void> {
        this.setChildrenRequests();
        await this.getCustomFieldDefinitions();
        await this.setAutoComplete();
        await this.getLookups();
        await this.getObjectFieldLookups();

        if (this.entity.id != '0') {
            this.view();
        }
    }
    //---------------------------------------------------------
    override initEntity(): ISspTransaction {
        return initSspTransaction();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async setAutoComplete() {

    }
    //---------------------------------------------------------
    // required to set the control with the entity initial values
    override async setDataAutoComplete() {

    }
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getSspTransactionActions(this, record);
    }

    //---------------------------------------------------------
    isVisible(fieldName: string): boolean {

        return isVisible(this.entity, this.validationForm, fieldName);
    }
    //--------------------------------------------------------------
    isEnabled(fieldName: string): boolean {

        return isEnabled(this.entity, this.validationForm, fieldName);
    }
    //--------------------------------------------------------------

    //Template_Start_Code_DontOverwrite_2
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;
        // Promise.all to improve performance. apply later
        try {
            const [
                ssPortfolioResponse,
                transactionTypeResponse,
                stockShareResponse,
                toPortfolioResponse,

            ] = await Promise.all
                ([
                    this.apiService.getItems<IQueryResponse>("/SsPortfolio/list", { pageSize: 30 }),
                    this.apiService.getItems<IQueryResponse>("/TransactionType/list", { "filter": { "stockEntityTypeId": 3, "sourceTypeId": 2 }, pageSize: 50 }),
                    this.apiService.getItems<IQueryResponse>("/StockShare/list", { pageSize: 30 }),
                    this.apiService.getItems<IQueryResponse>("/SsPortfolio/list", { pageSize: 30 }),

                ]);
            this.SsPortfolioOptions = ssPortfolioResponse.items;
            this.TransactionTypeOptions = transactionTypeResponse.items;
            this.StockShareOptions = stockShareResponse.items;
            this.ToPortfolioOptions = toPortfolioResponse.items;

        } catch (err: any) {
            const msg = err?.message || "An error occurred while loading data.";
            this.messages.push({ text: msg, msgType: "danger" });
        } finally {
            this.isLoading.lookups = false;
        }
        /*
               this.isLoading.lookups = true;
               target = "/[LookupNameCapital]/list";
               (await this.apiService.post(target,  {pageSize:50})).subscribe({
                   next: (response: IQueryResponse) => {
                       this.[LookupNameCapital]Options = response.items;
                       this.isLoading.lookups = false;
                   },
                       error: (err: any) => {
                       this.isLoading.lookups = false;
                       var msg = err.message || 'An error occurred while fetching [DisplayName] data.';
                       this.messages.push({ text: msg, msgType: "danger" });
                   }
               });
               */
    }
    //Template_End_Code_DontOverwrite_2

}
