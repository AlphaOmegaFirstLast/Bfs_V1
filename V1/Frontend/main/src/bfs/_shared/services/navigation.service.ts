import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { BaseReportComponent } from '../components/base-report';

interface ReportInfo {
  name: string;
  info: any;
}

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  private _previousUrl: string = '';
  private _currentUrl: string = '';
  private _isReset: boolean = false;

  constructor(private router: Router) {
    this.resetUrl(false);
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this._previousUrl = this._currentUrl;
        this._currentUrl = event.url;
        if (!this._isReset) {
          this.pushUrl();
        }
        this._isReset = false;
      }
    });
  }
  //-----------------------------------------------

  resetUrl(isResetFlag: boolean = true) {
    this._isReset = isResetFlag;

    //to retrieve "back url"
    var urlStack: string[] = [];
    sessionStorage.setItem("navHistory", JSON.stringify(urlStack));

    //to retrieve "report settings"
    var reportStack: string[] = [];
    sessionStorage.setItem("navReportHistory", JSON.stringify(reportStack));

    //to retrieve "form Tab"
    var formStack: string[] = [];
    sessionStorage.setItem("navFormHistory", JSON.stringify(formStack));
  }

  //-----------------------------------------------
  getStackFromSessionStorage(key: string): any {
    var objStack: string[] = [];
    var jsonString = sessionStorage.getItem(key);
    if (jsonString) {
      objStack = JSON.parse(jsonString) as string[];
    }
    return objStack;
  }
  //-----------------------------------------------
  getReport(reportName: string): any {
    var reportStack = this.getStackFromSessionStorage("navReportHistory") as ReportInfo[];
    var reportInfo = reportStack.find(x => x.name == reportName);
    return reportInfo?.info;
  }
  //-----------------------------------------------

  pushUrl() {
    var previousUrl = this._previousUrl;
    var urlStack = this.getStackFromSessionStorage("navHistory");

    if (previousUrl.includes('/list') || previousUrl.includes('/report')) {
      var tempReport = JSON.parse(sessionStorage.getItem("tempReport") || '{}');
      if (tempReport) {
        var reportName = tempReport["name"];
        previousUrl = BaseReportComponent.writeCustomReportIdParameter(previousUrl, reportName);

        // so base-report can retrieve the report from session storage when user navigates back to it
        var reportStack = this.getStackFromSessionStorage("navReportHistory");
        reportStack.push({ name: reportName, info: tempReport });
        sessionStorage.setItem("navReportHistory", JSON.stringify(reportStack));
      }
    }

    urlStack.push(previousUrl);
    sessionStorage.setItem("navHistory", JSON.stringify(urlStack));
  }
  //-----------------------------------------------

  popUrl() {

    var urlStack = this.getStackFromSessionStorage("navHistory");
    var nextUrl = urlStack.pop();
    sessionStorage.setItem("navHistory", JSON.stringify(urlStack));

    if (nextUrl) {
      this.router.navigateByUrl(nextUrl);
    }
  }
}
