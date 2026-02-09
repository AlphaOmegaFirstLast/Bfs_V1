
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IBfsSystemFilter } from './bfs-system.shared';

@Component({
    selector: 'app-bfs-system-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './bfs-system.filter.component.html'
    //styles: ``
})
export class BfsSystemFilterComponent implements OnInit {

    public result = {} as IBfsSystemFilter;

    // Define look ups
    public BfsClientOptions:  any[] = [];
public SystemTemplateOptions:  any[] = [];

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
        target = "/BfsClient/list";
        (await this.parent.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.BfsClientOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching BestFit Client data.';
                this.isLoading = false;
            }
        });
target = "/SystemTemplate/list";
        (await this.parent.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.SystemTemplateOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Template data.';
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