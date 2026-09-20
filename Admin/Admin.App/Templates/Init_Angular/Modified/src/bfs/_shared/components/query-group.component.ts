
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryColumn } from '../interfaces';

@Component({
    selector: 'bfs-query-group',
    imports: [FormsModule, CommonModule],
    templateUrl: './query-group.component.html'
    //styles: ``
})
export class QueryGroupComponent implements OnInit{
    
    public result: string = '';
    public itemColumns: IQueryColumn[] = [];
    public parent: any;

    constructor(public activeModal: NgbActiveModal) {
    }
    
    ngOnInit(): void {
        this.itemColumns = this.parent.queryRequest.columns || [];
        this.result = this.parent.queryRequest.groupOption || {};
    }

    reset() {
        this.activeModal.close('Reset');
        this.parent.applyGroup(null);
    }

    apply() {
        this.activeModal.close('Apply');
        this.parent.applyGroup(this.result);
    }
}