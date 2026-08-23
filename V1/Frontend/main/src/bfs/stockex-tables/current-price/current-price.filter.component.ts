
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ICurrentPriceFilter } from './current-price.shared';

@Component({
    selector: 'app-current-price-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './current-price.filter.component.html'
    //styles: ``
})
export class CurrentPriceFilterComponent implements OnInit {

    public result = {} as ICurrentPriceFilter;

    // Define look ups
    public StockShareOptions:  any[] = [];

    // Define range filters
    public TransactionDateFrom: Date | null | undefined;
    public TransactionDateTo: Date | null | undefined;
public PriceFrom: number | undefined;
    public PriceTo: number | undefined;

    isLoading: { list: boolean } = { list: false };
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
        this.TransactionDateFrom = this.result.TransactionDate?.from;
        this.TransactionDateTo   = this.result.TransactionDate?.to;
this.PriceFrom = this.result.Price?.from;
        this.PriceTo   = this.result.Price?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/StockShare/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockShareOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Stock Share data.';
                this.isLoading.list = false;
            }
        });

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
        this.result.TransactionDate = { from: this.TransactionDateFrom, to: this.TransactionDateTo };
this.result.Price = { from: this.PriceFrom, to: this.PriceTo };

        this.parent.applyFilter(this.result);
    }
}

