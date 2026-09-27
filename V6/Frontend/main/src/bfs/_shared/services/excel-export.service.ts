import * as XLSX from 'xlsx';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ExcelExportService {
  exportAsExcelFile(json: any[], fileName: string, sheetName: string = 'data'): void {
    if (!json || json.length === 0) {
      console.warn('No data to export');
      return;
    }

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
    const workbook: XLSX.WorkBook = {
      Sheets: { [sheetName]: worksheet },
      SheetNames: [sheetName],
    };
    const excelBuffer: ArrayBuffer = XLSX.write(workbook, {
      bookType: 'xlsx',
      type: 'array',
    });
    this.saveAsExcelFile(excelBuffer, fileName);
  }

  private saveAsExcelFile(buffer: ArrayBuffer, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8',
    });
    
    // Clean up any existing object URLs to prevent memory leaks
    const link = document.createElement('a');
    const url = URL.createObjectURL(data);
    link.href = url;
    link.download = `${fileName}.xlsx`;
    
    // Trigger download
    document.body.appendChild(link);
    link.click();
    
    // Cleanup
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
  }
}
