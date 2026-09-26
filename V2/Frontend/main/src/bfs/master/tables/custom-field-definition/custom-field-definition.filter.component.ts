
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

import { ICustomFieldDefinitionFilter } from './custom-field-definition.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-custom-field-definition-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './custom-field-definition.filter.component.html'
    //styles: ``
})
export class CustomFieldDefinitionFilterComponent implements OnInit {

    public result = {} as ICustomFieldDefinitionFilter;

    // Define look ups
    public BfsComponentOptions:  any[] = [];

    // Define autocomplete fields

    // Define range filters

    public isLoading: any = { list: false, view: false, save: false, lookups: false, autoComplete: false };
    public submit: boolean = false;
    public errorMessage: string = '';
    public infoMessage: string = '';
    public currentOperation: string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) {

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
        target = "/BfsComponent/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsComponentOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Component data.';
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

        this.parent.applyFilter(this.result);
    }
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

}