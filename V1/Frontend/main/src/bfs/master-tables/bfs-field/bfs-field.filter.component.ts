
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IBfsFieldFilter } from './bfs-field.shared';
import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
//Template_Component_AutoComplete

@Component({
    selector: 'app-bfs-field-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './bfs-field.filter.component.html'
    //styles: ``
})
export class BfsFieldFilterComponent implements OnInit {

    public result = {} as IBfsFieldFilter;

    // Define look ups
    public FilterTypeOptions:  any[] = [];
public BackendDataTypeOptions:  any[] = [];

    showBfsComponent = false; // Toggle for the overlay
    bfsComponentOptions: any[] = [];

    // Define range filters

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
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
        await this.setAutoComplete();
        // Initialize range filters if not set

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/FilterType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.FilterTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Filter Type data.';
                this.isLoading.list = false;
            }
        });
target = "/BackendDataType/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BackendDataTypeOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Backend Type data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {
    await this.bfsComponentAutoComplete();

}
//---------------------------------------------------------

async bfsComponentAutoComplete(searchTerm: string = this.result.BfsComponentName ?? ''): Promise<void> {
        const term = (searchTerm ?? '').trim();
        if (term.length < 2) {
            this.bfsComponentOptions = [];
            this.showBfsComponent = false;
            return;
        }

        this.showBfsComponent = true;
        this.isLoading.autoComplete = true;
        try {
            const request = { pageSize: 20, filter: { name: term } };
            const response: any = await this.parent.apiService.postAutoComplete('/BfsComponent/list', request);
            this.bfsComponentOptions = response?.items ?? [];
        } catch (err: any) {
            this.errorMessage = err?.message || 'Error fetching data';
            this.bfsComponentOptions = [];
        } finally {
            this.isLoading.autoComplete = false;
        }
    }
    //---------------------------------------------------------
    onBfsComponentInput(value: string): void {
        const val = value ?? '';
        // Reset selected ID
        this.result.BfsComponentName = undefined;
        this.result.BfsComponentId = undefined;
        this.bfsComponentAutoComplete(val);
    }
    //---------------------------------------------------------
    selectBfsComponent(selectedOption: any) {
        this.result.BfsComponentName = selectedOption?.name ?? undefined;
        this.result.BfsComponentId = selectedOption?.id ?? undefined;
        this.bfsComponentOptions = [];
        this.showBfsComponent = false;
    }
    //---------------------------------------------------------
    hideBfsComponentOverlay() {
        setTimeout(() => {
            this.showBfsComponent = false;
        }, 200);
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

