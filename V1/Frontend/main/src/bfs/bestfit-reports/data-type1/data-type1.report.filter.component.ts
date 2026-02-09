
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IDataType1Filter } from './data-type1.shared';

@Component({
    selector: 'app-data-type1-report-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './data-type1.report.filter.component.html'
    //styles: ``
})
export class DataType1FilterComponent implements OnInit {

    public result = {} as IDataType1Filter;

    // Define look ups

    // Define range filters
    public dataTypeIdFrom: number | undefined;
    public dataTypeIdTo: number | undefined;

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
        this.dataTypeIdFrom = this.result.dataTypeId?.from;
        this.dataTypeIdTo   = this.result.dataTypeId?.to;

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
        this.result.dataTypeId = { from: this.dataTypeIdFrom, to: this.dataTypeIdTo };

        this.parent.applyFilter(this.result);
    }
}