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
import { IEntity, IQueryResponse, IAction } from '@bfs/_shared/interfaces';

//----------------------- System Specific -------------------------- 
import { StockExService } from '@bfs/stockex/main/stockex.service';

//---------------------- Component Specific ------------------------
import {isEnabled, isVisible, type ITransactionType, initTransactionType, transactionTypeUntypedFormGroup, getTransactionTypeActions} from './transaction-type.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'transaction-type-form',
    imports: [

    CommonModule, NgIcon, NgbPopoverModule, NgbAlertModule, FormsModule, ReactiveFormsModule, NgbDropdownModule, NgbNavModule,RouterLink],
    standalone: true,
    templateUrl: './transaction-type.form.component.html',
})
export class TransactionTypeFormComponent extends BaseFormComponent<ITransactionType > implements OnInit {

    override apiUrl =  '/TransactionType/';
    override apiService: StockExService = inject(StockExService);
    override componentName: string = 'TransactionType'.toLowerCase();  // used to grab its related custom field definitions

    // Children filters

    // Define look ups
    public EffectTypeOptions: any[] = [];
public StockEntityTypeOptions: any[] = [];
public CalculationMethodOptions: any[] = [];
public SourceTypeOptions: any[] = [];
public StockFieldTypeOptions: any[] = [];
public NextTransactionTypeOptions: any[] = [];

    // Define autocomplete

    //---------------------------------------------------------

    constructor(activatedRoute: ActivatedRoute) {

       super(activatedRoute);
       this.validationForm = this.formBuilder.group(transactionTypeUntypedFormGroup(this.formBuilder)); // Use Angular Validation Controls

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
    override initEntity(): ITransactionType  {
        return initTransactionType ();
    }
    //---------------------------------------------------------
    override setChildrenRequests() {

    }
    //---------------------------------------------------------
    override async getLookups(): Promise<void> {
        this.messages = [];
        let target = '';
        this.isLoading.lookups = true;
// Promise.all to improve performance. apply later
         try{
         const [
              effectTypeResponse,
stockEntityTypeResponse,
calculationMethodResponse,
sourceTypeResponse,
stockFieldTypeResponse,
nextTransactionTypeResponse,

         ] = await Promise.all
         ([
        this.apiService.getItems<IQueryResponse>("/EffectType/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/StockEntityType/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/CalculationMethod/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/SourceType/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/StockFieldType/list", { pageSize: 30 }),
this.apiService.getItems<IQueryResponse>("/NextTransactionType/list", { pageSize: 30 }),

         ]);
        this.EffectTypeOptions = effectTypeResponse.items;
this.StockEntityTypeOptions = stockEntityTypeResponse.items;
this.CalculationMethodOptions = calculationMethodResponse.items;
this.SourceTypeOptions = sourceTypeResponse.items;
this.StockFieldTypeOptions = stockFieldTypeResponse.items;
this.NextTransactionTypeOptions = nextTransactionTypeResponse.items;

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
    //---------------------------------------------------------

    override getActions(record: IEntity): IAction[] {
        return getTransactionTypeActions(this, record);
    }

   //---------------------------------------------------------
    isVisible(fieldName: string): boolean {

        return isVisible(this.entity,this.validationForm, fieldName);
    }
    //--------------------------------------------------------------
    isEnabled(fieldName: string): boolean {

        return isEnabled(this.entity,this.validationForm, fieldName);
    }
    //--------------------------------------------------------------

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}
