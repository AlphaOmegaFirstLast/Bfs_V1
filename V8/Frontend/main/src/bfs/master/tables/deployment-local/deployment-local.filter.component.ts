
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LucideAngularModule, LucideSearch } from 'lucide-angular';

import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

import { IDeploymentLocalFilter } from './deployment-local.shared';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

@Component({
    selector: 'app-deployment-local-filter',
    imports: [LucideAngularModule, FormsModule, CommonModule

    ],
    templateUrl: './deployment-local.filter.component.html',
    providers: [

    ],
  //styles: ``
})
export class DeploymentLocalFilterComponent implements OnInit {

    public result = {} as IDeploymentLocalFilter;
    protected readonly LucideSearch = LucideSearch;

    // Define look ups
    public BfsSystemOptions:  any[] = [];

    // Define autocomplete fields

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
        target = "/BfsSystem/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsSystemOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching System Info data.';
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
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
}