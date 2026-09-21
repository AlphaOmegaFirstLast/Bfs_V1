
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

import { IBfsTenantSystemFilter } from './bfs-tenant-system.shared';
//Template_Component_AutoComplete

@Component({
    selector: 'app-bfs-tenant-system-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './bfs-tenant-system.filter.component.html'
    //styles: ``
})
export class BfsTenantSystemFilterComponent implements OnInit {

    public result = {} as IBfsTenantSystemFilter;

    // Define look ups
    public BfsTenantOptions:  any[] = [];

    // Define autocomplete fields
    bfsSystemAuto: AutoCompleteHelper;

    // Define range filters

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {
    this.bfsSystemAuto = new AutoCompleteHelper({ queryUrl: "/BfsSystem/list", fieldName: 'BfsSystem', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

    }

    async ngOnInit(): Promise<void> {
        this.result = this.parent.queryRequest.filter || {};
        await this.getLookups();
        await this.setAutoComplete();
        // Initialize range filters if not set

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/BfsTenant/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsTenantOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Tenant Name data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {
    this.bfsSystemAuto.setupFilter(this.parent.apiService, this.result);

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

