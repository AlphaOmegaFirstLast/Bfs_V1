
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IQueryResponse, ILookup } from '@bfs/_shared/interfaces';

@Component({
    selector: 'app-save-report',
    imports: [FormsModule, CommonModule],
    templateUrl: './save-report.component.html'
    //styles: ``
})
export class SaveReportComponent {

    public result :string = '';
    public parent: any;
    //---------------------------------------------------------
    constructor(public activeModal: NgbActiveModal) { }
    //---------------------------------------------------------
    reset() {
        this.activeModal.close('Reset');
    }
    //---------------------------------------------------------
    apply() {
        this.activeModal.close('Apply');
        this.parent.saveCustomReport(this.result);
    }
}