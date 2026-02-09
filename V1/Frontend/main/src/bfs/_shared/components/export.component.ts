
import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core';

import { HttpService } from '../services/http.service';
import { ExcelExportService } from '../services/excel-export.service';
import { IEntityRequest } from '../interfaces';

@Component({
    selector: 'bfs-export',
    imports: [CommonModule, NgbProgressbarModule,NgbAlertModule, NgIcon],
    templateUrl: './export.component.html'
    //styles: ``
})
export class ExportComponent<T> {
    @Input() getApiUrl: string | undefined;
    @Input() apiService: HttpService | undefined;
    @Input() downloadFileName: string | undefined;

    public exportRequest = {} as IEntityRequest<T>;
    items= [] as any[];
    totalPages: number = 0;
    pageIndex: number = 1;
    progress: number = 0;
    isExporting: boolean = false;
    infoMessage = '';
    errorMessage = '';

    constructor(private excelService: ExcelExportService) { }
    //--------------------------------------------------------------------------------------------

    public export() {
        this.exportRequest = JSON.parse(JSON.stringify(this.exportRequest));
        this.exportRequest.pageSize = 2;
        this.exportRequest.pageIndex = 1;
        var pageIndex = 1;
        this.items = [];
        this.progress = 0;
        this.infoMessage = 'Export started...';
        this.errorMessage = '';
        this.totalPages = 0;

        if (!this.isExporting) {
            this.isExporting = true;
            this.exportPage(pageIndex);
        }
    }
    //--------------------------------------------------------------------------------------------

    async exportPage(pageIndex: number) {
        if (this.isExporting) {
            this.exportRequest.pageIndex = pageIndex;
            var target = this.getApiUrl;
            if (this.apiService && target) {
                (await this.apiService.post(target, this.exportRequest)).subscribe({
                    next: response => {
                        this.totalPages = response.totalPages;
                        if (this.totalPages == 0) {
                            this.isExporting = false;
                            this.progress = 100;
                            this.infoMessage = 'No data to export';
                        }
                        else {
                            this.items.push(...response.items); //ToDo set maximum items to export
                            this.progress = Math.floor((this.exportRequest.pageIndex / this.totalPages) * 100);
                            this.exportRequest.pageIndex++;
                            this.isExporting = this.exportRequest.pageIndex <= this.totalPages;
                            if (this.isExporting) {
                                // Recursive call while isExporting is true
                                this.exportPage(this.exportRequest.pageIndex);
                            }
                            else {
                                this.excelService.exportAsExcelFile(this.items, this.downloadFileName || 'exported_data');
                                this.progress = 100; // Set progress to 100% when done
                                this.infoMessage = 'Export completed successfully.';
                            }

                        }
                    }
                    , error: err => {
                        this.errorMessage = err.message || 'An error occurred while adding tradingRoom data.';
                        this.isExporting = false;
                    }
                });
            }
        }
    }
    //--------------------------------------------------------------------------------------------
}