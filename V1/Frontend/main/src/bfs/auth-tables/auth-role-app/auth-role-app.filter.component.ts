
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAuthRoleAppFilter } from './auth-role-app.shared';

@Component({
    selector: 'app-auth-role-app-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './auth-role-app.filter.component.html'
    //styles: ``
})
export class AuthRoleAppFilterComponent implements OnInit {

    public result = {} as IAuthRoleAppFilter;

    // Define look ups
    public AuthRoleOptions:  any[] = [];
public AuthAppOptions:  any[] = [];

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
        target = "/AuthRole/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.AuthRoleOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Role data.';
                this.isLoading = false;
            }
        });
target = "/AuthApp/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.AuthAppOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching System Application data.';
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