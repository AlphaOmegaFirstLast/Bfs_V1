
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IColumns,ISort } from '../interfaces';

@Component({
    selector: 'bfs-query-sort',
    imports: [FormsModule, CommonModule],
    templateUrl: './query-sort.component.html'
    //styles: ``
})
export class QuerySortComponent implements OnInit{
    
    public result = {} as ISort;
    public itemColumns: IColumns[] = [];
    public parent: any;

    constructor(public activeModal: NgbActiveModal) {
    }
    
    ngOnInit(): void {
        this.itemColumns = this.parent.queryRequest.columns || [];
        this.result = this.parent.queryRequest.sortOption || {};
    }

    reset() {
        this.activeModal.close('Reset');
        this.parent.applySort(null);
    }

    apply() {
        this.activeModal.close('Apply');
        this.parent.applySort(this.result);
    }
}