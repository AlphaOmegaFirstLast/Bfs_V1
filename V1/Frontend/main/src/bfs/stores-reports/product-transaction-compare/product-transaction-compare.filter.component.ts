
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IProductTransactionCompareFilter } from './product-transaction-compare.shared';

@Component({
    selector: 'app-product-transaction-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './product-transaction-compare.filter.component.html'
    //styles: ``
})
export class ProductTransactionCompareFilterComponent implements OnInit {

    public result = {} as IProductTransactionCompareFilter;

    // Define look ups

    // Define range filters

    isLoading: boolean = false;
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) { }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        // Initialize range filters if not set

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';

    }
    //---------------------------------------------------------
    reset() {
        this.activeModal.close('Reset');
        this.parent.applyFilter(null);
    }
    //---------------------------------------------------------
    apply() {
        this.activeModal.close('Apply');
        // Apply range filters

        this.parent.applyFilter(this.result);
    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

