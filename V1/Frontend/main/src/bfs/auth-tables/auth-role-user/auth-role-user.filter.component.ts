
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IAuthRoleUserFilter } from './auth-role-user.shared';

@Component({
    selector: 'app-auth-role-user-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './auth-role-user.filter.component.html'
    //styles: ``
})
export class AuthRoleUserFilterComponent implements OnInit {

    public result = {} as IAuthRoleUserFilter;

    // Define look ups
    public AuthRoleOptions:  any[] = [];
public AuthUserOptions:  any[] = [];

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
target = "/AuthUser/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.AuthUserOptions = response.items;
                this.isLoading = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching Users data.';
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