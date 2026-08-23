
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ICouponFilter } from './coupon.shared';

@Component({
    selector: 'app-coupon-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './coupon.filter.component.html'
    //styles: ``
})
export class CouponFilterComponent implements OnInit {

    public result = {} as ICouponFilter;

    // Define look ups
    public TradingRoomOptions:  any[] = [];
public StockShareOptions:  any[] = [];
public CouponTypeOptions:  any[] = [];
public CouponStatusOptions:  any[] = [];

    // Define range filters
    public ValueFrom: number | undefined;
    public ValueTo: number | undefined;
public AnnounceDateFrom: Date | null | undefined;
    public AnnounceDateTo: Date | null | undefined;
public ValueDateFrom: Date | null | undefined;
    public ValueDateTo: Date | null | undefined;
public DueDateFrom: Date | null | undefined;
    public DueDateTo: Date | null | undefined;
public CouponPercentFrom: number | undefined;
    public CouponPercentTo: number | undefined;

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
        this.ValueFrom = this.result.Value?.from;
        this.ValueTo   = this.result.Value?.to;
this.AnnounceDateFrom = this.result.AnnounceDate?.from;
        this.AnnounceDateTo   = this.result.AnnounceDate?.to;
this.ValueDateFrom = this.result.ValueDate?.from;
        this.ValueDateTo   = this.result.ValueDate?.to;
this.DueDateFrom = this.result.DueDate?.from;
        this.DueDateTo   = this.result.DueDate?.to;
this.CouponPercentFrom = this.result.CouponPercent?.from;
        this.CouponPercentTo   = this.result.CouponPercent?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/TradingRoom/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.TradingRoomOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Trading Room data.';
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
                this.errorMessage = err.message || 'An error occurred while fetching Stock Share data.';
                this.isLoading.list = false;
            }
        });
target = "/CouponType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CouponTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Coupon Type data.';
                this.isLoading.list = false;
            }
        });
target = "/CouponStatus/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CouponStatusOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Coupon Status data.';
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
        this.result.Value = { from: this.ValueFrom, to: this.ValueTo };
this.result.AnnounceDate = { from: this.AnnounceDateFrom, to: this.AnnounceDateTo };
this.result.ValueDate = { from: this.ValueDateFrom, to: this.ValueDateTo };
this.result.DueDate = { from: this.DueDateFrom, to: this.DueDateTo };
this.result.CouponPercent = { from: this.CouponPercentFrom, to: this.CouponPercentTo };

        this.parent.applyFilter(this.result);
    }
}

