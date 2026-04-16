
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IBfsFieldFilter } from './bfs-field.shared';

@Component({
    selector: 'app-bfs-field-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './bfs-field.filter.component.html'
    //styles: ``
})
export class BfsFieldFilterComponent implements OnInit {

    public result = {} as IBfsFieldFilter;

    // Define look ups
    public BfsComponentOptions:  any[] = [];
public FilterTypeOptions:  any[] = [];
public BackendDataTypeOptions:  any[] = [];

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
        target = "/BfsComponent/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsComponentOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Component data.';
                this.isLoading = false;
            }
        });
target = "/FilterType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.FilterTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Filter Type data.';
                this.isLoading = false;
            }
        });
target = "/BackendDataType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BackendDataTypeOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Backend Type data.';
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

        this.parent.applyFilter(this.result);
    }
}

