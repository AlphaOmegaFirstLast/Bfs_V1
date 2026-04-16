
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IStructureCompareFilter } from './structure-compare.shared';

@Component({
    selector: 'app-structure-compare-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './structure-compare.filter.component.html'
    //styles: ``
})
export class StructureCompareFilterComponent implements OnInit {

    public result = {} as IStructureCompareFilter;

    // Define look ups
    public DataTypeOptions:  any[] = [];

    // Define range filters

    public countIdFrom: string | undefined;
    public countIdTo: string | undefined;

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

        this.countIdFrom = this.result.countId?.from;
        this.countIdTo   = this.result.countId?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/DataType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.DataTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Data Type data.';
                this.isLoading = false;
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

        this.result.countId = { from: this.countIdFrom, to: this.countIdTo };

        this.parent.applyFilter(this.result);
    }
}

