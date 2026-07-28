
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';
import { IUserRequestFilter } from './user-request.shared';

@Component({
    selector: 'app-user-request-filter',
    imports: [FormsModule, CommonModule],
    templateUrl: './user-request.filter.component.html'
    //styles: ``
})
export class UserRequestFilterComponent implements OnInit {

    public result = {} as IUserRequestFilter;

    // Define look ups
    public UserRequestStatusOptions:  any[] = [];

    // Define range filters
    public RequestDateFrom: Date | null | undefined;
    public RequestDateTo: Date | null | undefined;
public ResponseDateFrom: Date | null | undefined;
    public ResponseDateTo: Date | null | undefined;

    isLoading: { list: boolean } = { list: false };
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
        this.RequestDateFrom = this.result.RequestDate?.from;
        this.RequestDateTo   = this.result.RequestDate?.to;
this.ResponseDateFrom = this.result.ResponseDate?.from;
        this.ResponseDateTo   = this.result.ResponseDate?.to;

    }
    //---------------------------------------------------------
    async getLookups(): Promise<void> {
        let target = '';
        target = "/UserRequestStatus/list";
        (await this.parent.apiService.post(target,  {pageSize:50})).subscribe({
            next: (response: IQueryResponse) => {
                this.UserRequestStatusOptions = response.items;
                this.isLoading.list = false;
            },
                error: (err: any) => {
                this.errorMessage = err.message || 'An error occurred while fetching User Request Status data.';
                this.isLoading.list = false;
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
        this.result.RequestDate = { from: this.RequestDateFrom, to: this.RequestDateTo };
this.result.ResponseDate = { from: this.ResponseDateFrom, to: this.ResponseDateTo };

        this.parent.applyFilter(this.result);
    }
}

