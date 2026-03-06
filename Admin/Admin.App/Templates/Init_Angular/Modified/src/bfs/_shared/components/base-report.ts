//---------------- angular ----------------------------------
import { Component, inject, OnInit, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
//---------------- Ng Bootstrap ------------------------------
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIcon } from '@ng-icons/core'
//---------------- charts -------------------------------------
import { getColor } from "@/app/utils/color-utils";
import { EChartsOption } from 'echarts';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import type { EChartsType } from 'echarts/core';
import { echarts } from '@/app/config/echarts-config';
//---------------- bfs shared -------------------------------------
import { IAction, IEntity, IEntityRequest, IIdentifiable, IQueryColumn, IUserInterface } from "@bfs/_shared/interfaces";
import { type IColumns, ICustomReports, formatFilter, IUIMessage, ViewLink, ActionLink } from '@bfs/_shared/interfaces';
import { QuerySortComponent } from '@bfs/_shared/components/query-sort.component';
import { QueryColumnsComponent } from '@bfs/_shared/components/query-columns.component';
import { QueryGroupComponent } from '@bfs/_shared/components/query-group.component';
import { SaveReportComponent } from '@bfs/_shared/components/save-report.component';
import { UploadFileComponent } from '@bfs/_shared/components/upload-file.component';
import { ExportComponent } from '@bfs/_shared/components/export.component';

import { TokenService } from '@bfs/_shared/services/token.service';
import { ExcelExportService } from '@bfs/_shared/services/excel-export.service';
import { getReportInfoData, getReportInfoHeaders } from '../objectFields';

@Component({
    selector: 'app-base-report',
    template: '' // descendant classes will use './base-report.component.html'
})
export class BaseReportComponent<IFilter, IWithLookup> {
    @ViewChild('exportExcel') exportComponent!: ExportComponent<IFilter>;
    @Input() presetFilter: IFilter | undefined;
    filter!: IFilter;
    lookup!: IWithLookup;
    public list: IEntity[] = [];
    public customReportInfo = { id: '0', name: 'NamePlaceHolder', url: 'UrlPlaceHolder' };
    public apiCustomReportsUrl = "/CustomReports/";

    public getApiUrl = '';
    public getByIdApiUrl = '';
    public uploadApiUrl = '';
    public apiService!: any;
    public tokenService: TokenService = inject(TokenService);
    public queryRequest = {} as IEntityRequest<IFilter>;
    public exportRequest = {} as IEntityRequest<IFilter>;
    public filterComponent: any;
    public chartOptions: EChartsOption = {};
    public queryOwner: string = "displayName"; // used to set the vertical axis in the chart
    public downloadFileName: string = "ReportData";  // file name for export. it gets overriden in derived classes
    public addNewRecordLink: ViewLink = { recordId: '', route: "/add/0", displayText: "Add New Record" }; // top left button of 'Add' functionality. it gets overriden in derived classes
    //---------------------------------------------------------

    public filterArray: string[] = [];
    public isLoading: boolean = false;
    public messages: IUIMessage[] = [];
    //-------------------------------------------------------- Set visibility of buttons and sections ----------
    public isSection = { chart: false, table: true, description: false };
    public isButton: any = { addNew: true, chart: true, description: true, columns: true, filter: true, sort: true, group: false };

    //-------------------------------------------------------- Set accessibility of features ----------
    public isCustomReport: any = { view: true, save: true, restore: true };
    //------------------------------------------------------ Set pagination ----------
    public pageSizes: number[] = [20, 30, 50, 100];
    public pagination: any = { currentPage: 1, pageSize: this.pageSizes[0], pageCount: 0, totalItems: 1, description: '' };
    //------------------------------------------------------
    public me = this;
    private sanitizer: DomSanitizer = inject(DomSanitizer);

    constructor(public modalService: NgbModal, public router: Router, public excelService: ExcelExportService, public activatedRoute: ActivatedRoute) {
        // Initialize queryRequest with default values
        this.readCustomReportIdParameter();
        this.queryRequest = this.setRequestType();
    }
    //---------------------------------------------------------
    async ngOnInit(): Promise<void> {
        if (this.presetFilter) {
            this.queryRequest.filter = this.presetFilter;
        }
        this.queryRequest.pageSize = this.pageSizes[0];
        // the call could be from a custom report, in that case restore it. or Directly from base report (List or report) 
        if (this.customReportInfo.id && this.customReportInfo.id != '0') {
            await this.restoreCustomReport();
            await this.getReport();
        }
        else {
            await this.getReport();
        }
        this.setAccessible();
    }
    //---------------------------------------------------------

    async getReport(): Promise<void> {

        if (!this.isLoading) {  // to prevent multiple requests
            this.messages = [];
            this.isLoading = true;
            var target = this.getApiUrl;
            (await this.apiService.post(target, this.queryRequest)).subscribe({
                next: (res: any) => {
                    this.isLoading = false;
                    this.list = res.items;
                    this.pagination.totalItems = res.totalItems;
                    this.pagination.pageCount = res.totalPages;
                    this.chartOptions = this.getChart(res.items);
                    this.setPaginationDescription();
                },
                error: (err: any) => {
                    this.isLoading = false;
                    var msg = err.message || `An error occurred while processing ${this.getApiUrl} data.`;
                    this.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    getDescription() {
        return ["Phrasing the filter object, working on this.queryRequest"];
    }
    //---------------------------------------------------------
    openFilter() {
        const modalRef = this.modalService.open(this.filterComponent, { 'backdrop': 'static' });
        modalRef.componentInstance.parent = this;
    }
    //---------------------------------------------------------
    applyFilter(result?: any) {
        this.queryRequest.filter = result;
        this.queryRequest.pageIndex = 1; // Reset to first page after applying filter
        this.getReport();
        this.pagination.currentPage = 1;
        this.filterArray = this.getDescription();
    }
    //---------------------------------------------------------
    openSort() {
        const modalRef = this.modalService.open(QuerySortComponent, { 'backdrop': 'static' });
        modalRef.componentInstance.parent = this;
    }
    //---------------------------------------------------------
    applySort(result?: any) {
        this.queryRequest.sortOption = result;
        this.queryRequest.pageIndex = 1; // Reset to first page after applying filter
        this.getReport();
        this.pagination.currentPage = 1;
    }
    //---------------------------------------------------------
    openGroup() {
        const modalRef = this.modalService.open(QueryGroupComponent, { 'backdrop': 'static' });
        modalRef.componentInstance.parent = this;
    }
    //---------------------------------------------------------
    applyGroup(result?: any) {
        this.queryRequest.group = result;
        this.queryRequest.pageIndex = 1; // Reset to first page after applying filter
        this.getReport();
        this.pagination.currentPage = 1;
    }
    //---------------------------------------------------------
    openColumns() {
        const modalRef = this.modalService.open(QueryColumnsComponent, { 'backdrop': 'static' });
        modalRef.componentInstance.parent = this;
    }
    //---------------------------------------------------------
    applyColumns(result?: any) {
        this.queryRequest.columns = result;
    }
    //---------------------------------------------------------
    openSaveReport(me: any) {
        const modalRef = me.modalService.open(SaveReportComponent, { 'backdrop': 'static' });
        modalRef.componentInstance.parent = me;

    }
    //---------------------------------------------------------   
    viewDescription() {
        this.isSection.description = !this.isSection.description;
    }
    //---------------------------------------------------------
    viewChart() {
        this.isSection.chart = !this.isSection.chart;
    }
    //---------------------------------------------------------

    getReportActions(): ActionLink[] {

        let actionLinks: ActionLink[] = [
            { recordId: '', action: this.exportList, displayText: "Export data as Excel" },
            { recordId: '', action: this.exportJson, displayText: "Export data as Json" },
            { recordId: '', action: this.importList, displayText: "Import data as Json" },
            { recordId: '', action: this.openSaveReport, displayText: "Save settings as Custom Report" }
        ];

        return actionLinks;
    }
    //---------------------------------------------------------
    exportList(me: any) {
        (me.exportComponent as ExportComponent<IFilter>).export();
    }
    //---------------------------------------------------------
    async exportJson(me: any): Promise<void> {
        if (!me.isLoading) {  // to prevent multiple requests
            me.messages = [];
            me.isLoading = true;
            var target = me.getApiUrl;
            me.queryRequest.pageSize = me.pagination.totalItems;
            me.queryRequest.pageIndex = 1;

            (await me.apiService.downloadJson(target, me.queryRequest, {}, me.downloadFileName)).subscribe({
                next: (res: any) => {
                    me.isLoading = false;
                    me.queryRequest.pageSize = me.pageSizes[0];
                },
                error: (err: any) => {
                    me.isLoading = false;
                    var msg = err.message || 'An error occurred while processing Tables Fields data.';
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    importList(me: any) {
        const modalRef = me.modalService.open(UploadFileComponent, { 'backdrop': 'static' });
        modalRef.componentInstance.uploadUrl = me.apiService.origin + me.uploadApiUrl;
    }
    //---------------------------------------------------------
    isObjectField(field: string): boolean {
        field = field.toLowerCase();
        return field.includes('fieldvalidation') || field.includes('reportinfo');
    }
    //---------------------------------------------------------        
    objectFieldHeaders(field: string): SafeHtml {
        var result = '';
        switch (field.toLowerCase()) {
            case 'reportinfo':
                result = getReportInfoHeaders();
                break;
            default:
                result = '';
        }

        return this.sanitizer.bypassSecurityTrustHtml(result) || '';
    }
    //---------------------------------------------------------
    objectFieldData(record: any, field: string): SafeHtml {
        var result = '';

        switch (field.toLowerCase()) {
            case 'reportinfo':
                result = getReportInfoData(record[field] as string);
                break;
            default:
                result = '';
        }
        
        return this.sanitizer.bypassSecurityTrustHtml(result) || '';
    }
    //---------------------------------------------------------
    readCustomReportIdParameter() {
        let route = this.activatedRoute;
        // customReportId either in this format: "/report/structure-report/0"  or in this format:   "/client/list/0"
        // invalid format /component/edit/15 when the component has "tab list" EntityChildren. CustomReportId is not expected in that format and should not block data retrieval 
        if (route.snapshot.url.length == 4) {
            let segment0 = route.snapshot.url[route.snapshot.url.length - 4].path;
            let segment1 = route.snapshot.url[route.snapshot.url.length - 3].path;
            let segment2 = route.snapshot.url[route.snapshot.url.length - 2].path;
            let segment3 = route.snapshot.url[route.snapshot.url.length - 1].path;
            if (segment1 == 'report') {
                this.customReportInfo.url = `${segment0}/${segment1}/${segment2}/`;
                this.customReportInfo.name = segment2;
                this.customReportInfo.id = segment3;
            }
            if (segment2 == 'list') {
                this.customReportInfo.url = `${segment0}/${segment1}/${segment2}/`;
                this.customReportInfo.name = segment1;
                this.customReportInfo.id = segment3;
            }
        }
    }
    //---------------------------------------------------------
    goToCustomReport(me: IUserInterface, record: any, data: any) {
        let customReportrecord = data?.record || record;
        let url = customReportrecord?.url || "";
        me.router.navigate([`${url}/${customReportrecord.id}`]);
    }
    //---------------------------------------------------------
    getCustomReportUrl(): string {
        let route = this.activatedRoute;
        if (route.snapshot.url.length >= 3) {
            let segment0 = route.snapshot.url[0].path;     // system prefix segment, e.g. "bfs"
            let segment1 = route.snapshot.url[1].path;     // entity/list segment, e.g. "report" or "client"
            let segment2 = route.snapshot.url[2].path;     // repoty/entity name segment, e.g. "structure-report" or "list"
            return `${segment0}/${segment1}/${segment2}/`;
        }
        return '';
    }
    //---------------------------------------------------------
    getCustomReportBaseReport(): string {
        let route = this.activatedRoute;
        if (route.snapshot.url.length >= 3) {
            let segment1 = route.snapshot.url[1].path;
            let segment2 = route.snapshot.url[2].path;
            return (segment1 == 'report') ? segment2 : (segment2 == 'list') ? segment1 : '';
        }
        return '';
    }
    //---------------------------------------------------------
    async saveCustomReport(reportName: string) {
        var target = this.apiCustomReportsUrl;
        var data = {
            "isDeleted": false,
            "id": 0,
            "name": reportName,
            "request": JSON.stringify(this.queryRequest),
            "createdBy": "CurrentUserId",
            "isPrivate": true,
            "baseReport": this.getCustomReportBaseReport(),
            "url": this.getCustomReportUrl()
        };
        if (!this.isLoading) {  // to prevent multiple requests
            this.messages = [];
            this.isLoading = true;
            (await this.apiService.post(target, data)).subscribe({
                next: (response: ICustomReports) => {
                    this.isLoading = false;
                    let customReport = response;
                    this.messages.push({ text: `${customReport.name} is saved successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    this.isLoading = false;
                    var msg = err.message || 'An error occurred while adding Custom Reports data.';
                    this.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    async restoreCustomReport(): Promise<void> {
        if (!this.isLoading) {  // to prevent multiple requests
            this.messages = [];
            this.isLoading = true;
            var target = this.apiCustomReportsUrl + this.customReportInfo.id;
            (await this.apiService.get(target)).subscribe({
                next: (response: ICustomReports) => {
                    this.isLoading = false;
                    this.queryRequest = response.request ? JSON.parse(response.request) : null;
                    this.getReport();
                },
                error: (err: any) => {
                    this.isLoading = false;
                    var msg = err.message || 'An error occurred while fetching Custom Reports data.';
                    this.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    async duplicateRecord(me: any, record: IWithLookup, data: any): Promise<void> {
        const id = (record as IIdentifiable).id;
        if (!me.isLoading) {  // to prevent multiple requests
            me.messages = [];
            me.isLoading = true;
            var target = `${me.getByIdApiUrl}${id}`;
            (await me.apiService.get(target)).subscribe({
                next: (res: any) => {
                    me.isLoading = false;
                    var duplicatedRecord = res as IWithLookup;
                    me.postDuplicateRecord(me, duplicatedRecord, data);
                },
                error: (err: any) => {
                    me.isLoading = false;
                    var msg = err.message || 'An error occurred while processing Systems data.';
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
    //---------------------------------------------------------
    async postDuplicateRecord(me: any, record: IWithLookup, data: any) {
        if (record as IIdentifiable) {
            (record as IIdentifiable).id = 0; // reset id only for record duplication and not for tree duplication

            var target = data.postUrl;  // for record duplication the default postUrl is used, for tree duplication a different url is used
            (await me.apiService.post(target, record)).subscribe({
                next: (res: any) => {
                    me.messages.push({ text: 'Record duplicated successfully.', msgType: "info" });
                    me.getReport();
                },
                error: (err: any) => {
                    var msg = err.message || 'An error occurred while duplicating the record.';
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
        else {
            var msg = 'Record does not implement IIdentifiable interface. Cannot reset id for duplication.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    }
    //---------------------------------------------------------

    async duplicateTree(me: any, record: IWithLookup, data: any) {
        if (record as IIdentifiable) {
            var target = data.postUrl;  // for record duplication the default postUrl is used, for tree duplication a different url is used
            (await me.apiService.post(target, `${(record as IIdentifiable).id}`)).subscribe({
                next: (res: any) => {
                    me.messages.push({ text: 'Record duplicated successfully.', msgType: "info" });
                    me.getReport();
                },
                error: (err: any) => {
                    var msg = err.message || 'An error occurred while duplicating the record.';
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
        else {
            var msg = 'Record does not implement IIdentifiable interface. Cannot reset id for duplication.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    }
    //---------------------------------------------------------   
    setAccessible(): void {
    }
    //---------------------------------------------------------
    formatRequest() {
        this.queryRequest.filter = JSON.parse(JSON.stringify(this.queryRequest.filter));
    }
    //---------------------------------------------------------
    onPageChange(page: number) {
        this.pagination.currentPage = page;
        this.queryRequest.pageIndex = page;
        this.getReport();
    }
    //---------------------------------------------------------
    onPageSizeChange(event: Event) {
        const select = event.target as HTMLSelectElement;
        const pageSize = Number(select.value);
        if (!pageSize) return;
        this.pagination.pageSize = pageSize;
        this.queryRequest.pageIndex = 1;
        this.queryRequest.pageSize = pageSize;
        this.getReport();
    }
    //---------------------------------------------------------
    selectPage(pageCount: number) {
        this.pagination.currentPage = pageCount;
        this.queryRequest.pageIndex = pageCount;
        this.getReport();
    }
    //---------------------------------------------------------
    setPaginationDescription() {
        //                    <div>Showing {{ queryRequest.pageSize * (queryRequest.pageIndex - 1) + 1 }} to {{ queryRequest.pageSize * queryRequest.pageIndex }} records of {{ pagination.totalItems }}</div>
        var total = this.pagination.totalItems;
        var from = this.queryRequest.pageSize * (this.queryRequest.pageIndex - 1) + 1;
        var to = this.queryRequest.pageSize * this.queryRequest.pageIndex;
        to = to > total ? total : to;

        var isFiltered = false;
        // loop all filter properties to check if any property has value
        if (this.queryRequest.filter != null && this.queryRequest.filter !== '{}' && this.queryRequest.filter != undefined) {
            for (const key in this.queryRequest.filter) {
                const value = this.queryRequest.filter[key as keyof IFilter];
                if (value != null && value != undefined && value != '') {
                    isFiltered = true;
                    break;
                }
            }
        }
        var filtered = isFiltered ? ' (filtered)' : '';
        this.pagination.description = `Showing ${from} to ${to} records of ${total}${filtered}`;
    }
    //---------------------------------------------------------
    setRequestType() {
        let request!: IEntityRequest<IFilter>;
        return request;
    }
    //---------------------------------------------------------
    render(record: IEntity, column: IColumns): any {
        const value = record[column.fieldName as keyof IEntity];
        switch (column.fieldName) {
            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
    getRecordLinks(record: IEntity): ViewLink[] {
        //to be overridden in descendant classes to provide record level links
        return [];
    }
    //---------------------------------------------------------
    getRecordActions(record: IEntity): ActionLink[] {
        //to be overridden in descendant classes to provide record level links
        return [];
    }
    //---------------------------------------------------------

    isAccessible(linkOrAction: ViewLink | ActionLink): boolean {
        return true;
    }
    //---------------------------------------------------------
    getBaseChart(): EChartsOption {

        return {
            tooltip: {
                trigger: "axis",
                padding: [5, 0],
                backgroundColor: getColor("secondary-bg"),
                borderColor: getColor("border-color"),
                textStyle: { color: getColor("light-text-emphasis") },
                borderWidth: 1,
                transitionDuration: 0.125,
                axisPointer: { type: "none" },
                shadowBlur: 2,
                shadowColor: "rgba(76, 76, 92, 0.15)",
                shadowOffsetX: 0,
                shadowOffsetY: 1,
                formatter: function (params: any) {
                    const title = params[0].name;
                    let content = `<div style="font-size: 14px; font-weight: 600; text-transform: uppercase; border-bottom: 1px solid ${getColor("border-color")}; margin-bottom: 8px; padding: 3px 10px 8px;">${title}</div>`;
                    params.forEach((item: any) => {
                        content += `<div style="margin-top: 4px; padding: 3px 15px;">
                            <span style="display:inline-block;margin-right:5px;border-radius:50%;width:10px;height:10px;background-color:${item.color};"></span>
                            ${item.seriesName} : <strong>${item.value}</strong>
                        </div>`;
                    });
                    return content;
                }
            }, textStyle: {
                fontFamily: getComputedStyle(document.body).fontFamily
            }, legend: {
                show: false,
            }, color: [getColor("primary"), getColor("secondary")], grid: {
                left: '3%', right: '4%', bottom: '3%', top: 0, containLabel: true
            }
            , xAxis: {
                type: 'value',
                boundaryGap: [0, 0.05],
                axisLabel: {
                    show: true, color: getColor('body-color')
                }, splitLine: {
                    lineStyle: {
                        color: "rgba(133, 141, 152, 0.1)", type: 'dashed'
                    }
                }
            }
            , yAxis: {
                type: 'category',
                data: [],
                axisLine: {
                    lineStyle: {
                        type: 'dashed', color: getColor('light')
                    }
                },
                axisLabel: {
                    show: true, color: getColor('body-color')
                },
                splitLine: {
                    lineStyle: {
                        color: "rgba(133, 141, 152, 0.1)", type: 'dashed'
                    }
                }
            }
            , series: []
        };
    }
    //---------------------------------------------------------

    getDemoChart(): EChartsOption {

        return {
            tooltip: {
                trigger: "axis",
                padding: [5, 0],
                backgroundColor: getColor("secondary-bg"),
                borderColor: getColor("border-color"),
                textStyle: { color: getColor("light-text-emphasis") },
                borderWidth: 1,
                transitionDuration: 0.125,
                axisPointer: { type: "none" },
                shadowBlur: 2,
                shadowColor: "rgba(76, 76, 92, 0.15)",
                shadowOffsetX: 0,
                shadowOffsetY: 1,
                formatter: function (params: any) {
                    const title = params[0].name;
                    let content = `<div style="font-size: 14px; font-weight: 600; text-transform: uppercase; border-bottom: 1px solid ${getColor("border-color")}; margin-bottom: 8px; padding: 3px 10px 8px;">${title}</div>`;
                    params.forEach((item: any) => {
                        content += `<div style="margin-top: 4px; padding: 3px 15px;">
                            <span style="display:inline-block;margin-right:5px;border-radius:50%;width:10px;height:10px;background-color:${item.color};"></span>
                            ${item.seriesName} : <strong>${item.value}</strong>
                        </div>`;
                    });
                    return content;
                }
            }, textStyle: {
                fontFamily: getComputedStyle(document.body).fontFamily
            }, legend: {
                show: false,
            }, color: [getColor("primary"), getColor("secondary")], grid: {
                left: '3%', right: '4%', bottom: '3%', top: 0, containLabel: true
            }, xAxis: {
                type: 'value', boundaryGap: [0, 0.01], axisLabel: {
                    show: true, color: getColor('body-color')
                }, splitLine: {
                    lineStyle: {
                        color: "rgba(133, 141, 152, 0.1)", type: 'dashed'
                    }
                }
            }, yAxis: {
                type: 'category', data: ['Brazil', 'Indonesia', 'USA', 'India', 'China', 'World'], axisLine: {
                    lineStyle: {
                        type: 'dashed', color: getColor('light')
                    }
                }, axisLabel: {
                    show: true, color: getColor('body-color')
                }, splitLine: {
                    lineStyle: {
                        color: "rgba(133, 141, 152, 0.1)", type: 'dashed'
                    }
                }
            }, series: [{
                name: '2011', type: 'bar', data: [18203, 23489, 29034, 104970, 131744, 630230]
            }, {
                name: '2012', type: 'bar', data: [19325, 23438, 31000, 121594, 134141, 681807]
            }]
        };
    }
    //---------------------------------------------------------
    getChart(records: IWithLookup[]): EChartsOption {
        return this.getDemoChart();
    }
}

