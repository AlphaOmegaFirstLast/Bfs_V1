
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, LucideSearch } from 'lucide-angular';

import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAutoComplete, AutoCompleteHelper } from '@bfs/_shared/helpers/auto-complete.class';

import { IBfsComponentBusinessActionFilter } from './bfs-component-business-action.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-bfs-component-business-action-filter',
    imports: [LucideAngularModule, FormsModule, CommonModule

    ],
    templateUrl: './bfs-component-business-action.filter.component.html',
    providers: [

    ],
  //styles: ``
})
export class BfsComponentBusinessActionFilterComponent implements OnInit {

    public result = {} as IBfsComponentBusinessActionFilter;
    protected readonly LucideSearch = LucideSearch;

    // Define look ups
    public BusinessActionOptions:  any[] = [];
public ActionLocationOptions:  any[] = [];

    // Define autocomplete fields
    bfsComponentAuto: AutoCompleteHelper;

    // Define range filters

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
    this.bfsComponentAuto = new AutoCompleteHelper({ queryUrl: "/BfsComponent/list", fieldName: 'BfsComponent', control: null, id: '', name: '', showDropDown: false, options: [], isLoading: false, isInitial: true } as IAutoComplete);

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

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
        target = "/BusinessAction/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BusinessActionOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Business Action data.';
                this.isLoading.list = false;
            }
        });
target = "/ActionLocation/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.ActionLocationOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Action Location data.';
                this.isLoading.list = false;
            }
        });

    }
    //---------------------------------------------------------
    async setAutoComplete() {
    this.bfsComponentAuto.setupFilter(this.parent.apiService, this.result);

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
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
}