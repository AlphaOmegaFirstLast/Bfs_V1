
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ISspStockFilter } from './ssp-stock.shared';

@Component({
    selector: 'app-ssp-stock-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './ssp-stock.filter.component.html'
    //styles: ``
})
export class SspStockFilterComponent implements OnInit {

    public result = {} as ISspStockFilter;

    // Define look ups
    public SsPortfolioOptions:  any[] = [];
public StockShareOptions:  any[] = [];

    // Define range filters
    public QuantityFrom: number | undefined;
    public QuantityTo: number | undefined;
public AverageCostFrom: number | undefined;
    public AverageCostTo: number | undefined;

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
        this.QuantityFrom = this.result.Quantity?.from;
        this.QuantityTo   = this.result.Quantity?.to;
this.AverageCostFrom = this.result.AverageCost?.from;
        this.AverageCostTo   = this.result.AverageCost?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/SsPortfolio/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.SsPortfolioOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching StockShare Portfolio data.';
                this.isLoading.list = false;
            }
        });
target = "/StockShare/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.StockShareOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching StockShare  data.';
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
        this.result.Quantity = { from: this.QuantityFrom, to: this.QuantityTo };
this.result.AverageCost = { from: this.AverageCostFrom, to: this.AverageCostTo };

        this.parent.applyFilter(this.result);
    }
}

