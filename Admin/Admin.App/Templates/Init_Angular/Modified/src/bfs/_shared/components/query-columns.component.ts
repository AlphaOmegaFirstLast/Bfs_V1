import { Component,OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IColumns } from '../interfaces';

@Component({
    selector: 'bfs-query-columns',
    imports: [FormsModule, CommonModule],
    templateUrl: './query-columns.component.html'
    //styles: ``
})
export class QueryColumnsComponent implements OnInit{

    public result: IColumns[] = [];
    public parent: any;


    constructor(public activeModal: NgbActiveModal) {}

    ngOnInit(): void {
        this.result = this.parent.queryRequest.columns || [];
    }

    reset() {
        this.activeModal.close('Reset');
        let columns: IColumns[] = this.parent.queryRequest.columns;    // do casting first
        this.result = columns.map(x => ({ ...x, isVisible: true }));   // set all columns visible
        this.parent.applyColumns(this.result);
    }

    apply() {
        this.activeModal.close('Apply');
        this.parent.applyColumns(this.result);
    }
}