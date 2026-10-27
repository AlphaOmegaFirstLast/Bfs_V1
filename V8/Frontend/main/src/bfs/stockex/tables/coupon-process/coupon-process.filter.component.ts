
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, LucideSearch } from 'lucide-angular';
import { FlatpickrDirective, provideFlatpickrDefaults } from 'angularx-flatpickr';

import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

import { ICouponProcessFilter } from './coupon-process.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-coupon-process-filter',
    imports: [LucideAngularModule, FormsModule, CommonModule
, FlatpickrDirective

    ],
    templateUrl: './coupon-process.filter.component.html',
    providers: [
  provideFlatpickrDefaults()

    ],
  //styles: ``
})
export class CouponProcessFilterComponent implements OnInit {

    public result = {} as ICouponProcessFilter;
    protected readonly LucideSearch = LucideSearch;

    // Define look ups
    public CouponOptions:  any[] = [];
public ProcessStatusOptions:  any[] = [];

    // Define autocomplete fields

    // Define range filters
    public ProcessDateFrom: Date | null | undefined;
    public ProcessDateTo: Date | null | undefined;

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

    }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        await this.setAutoComplete();
        // Initialize range filters if not set
        this.ProcessDateFrom = this.result.ProcessDate?.from;
        this.ProcessDateTo   = this.result.ProcessDate?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/Coupon/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.CouponOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Coupon data.';
                this.isLoading.list = false;
            }
        });
target = "/ProcessStatus/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ProcessStatusOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Process Status data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {

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
        this.result.ProcessDate = { from: this.ProcessDateFrom, to: this.ProcessDateTo };

        this.parent.applyFilter(this.result);
    }
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
}