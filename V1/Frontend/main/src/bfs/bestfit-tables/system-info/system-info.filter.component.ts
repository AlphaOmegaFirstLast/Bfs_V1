
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { ISystemInfoFilter } from './system-info.shared';

@Component({
    selector: 'app-system-info-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './system-info.filter.component.html'
    //styles: ``
})
export class SystemInfoFilterComponent implements OnInit {

    public result = {} as ISystemInfoFilter;

    // Define look ups
    public ClientOptions:  ILookup[] = [];
public SystemTemplateOptions:  ILookup[] = [];

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
        target = "/Client/list";
        (await this.parent.apiService.post(target,  {pageSize:30})).subscribe({
            next: (response: IQueryResponse) => {
                this.ClientOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Client data.';
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