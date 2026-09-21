import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgFor, CommonModule } from '@angular/common';
import { NgIcon } from '@ng-icons/core';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

import { TokenService } from '@bfs/_shared/security/token.service';
import { AccessService } from '@bfs/_shared/security/access.service';
import { MasterService } from '@bfs/master-main/master.service';
import { IUIMessage } from '@bfs/_shared/interfaces';

@Component({
  selector: 'app-home-page',
  imports: [NgFor, NgIcon, NgbAlertModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  tokenService: TokenService = inject(TokenService);
  accessService: AccessService = inject(AccessService);
  bfsService: MasterService = inject(MasterService);

  userApps: any[] = [];
  welcomeData: any = { tenant: '', company: '', system: '', systemId: '', systemLogo: ''  };
    public messages: IUIMessage[] = [];

  constructor(private cdr: ChangeDetectorRef) { }
  //-------------------------------------------------------

  async ngOnInit(): Promise<void> {
    this.getWelcomeData();
    this.userApps = await this.getAccessData();
  }
  //-------------------------------------------------------

  async getAccessData(): Promise<any[]> {
    let dataReady = await this.accessService.IsAccessServiceReady();
    if (dataReady) {
      var userAppsOfCurrentTenant =  await this.accessService.getUserApplications();
      // Filter all user apps by systemId of current system, which is stored in session storage by Identity.Web
      var currentSystemId = sessionStorage.getItem('current-systemId');
      if (currentSystemId) {
        return userAppsOfCurrentTenant.filter((app: any) => app.bfsSystemId == currentSystemId);
      }
    }else{
      this.messages.push({ text: "Failed to retrieve access data.", msgType: "danger" });
    }

    return [];
  }
  //-------------------------------------------------------

  async setCurrentApp(appName: string): Promise<void> {
    let result = appName.split("-");
    let system = result[0].trim().toLocaleLowerCase();
    let app = result[1].trim().toLocaleLowerCase();
    sessionStorage.setItem('current-app', app);
    window.location.reload();
  }
  //-------------------------------------------------------

  getWelcomeData(): void {
    //read cookie "WelcomeData" and alert its value

    const cookieValue = document.cookie.split('; ').find(row => row.startsWith('welcome-data='));
    if (cookieValue) {
      const welcomeCookie = cookieValue.split('=')[1];
      const decodedValue = decodeURIComponent(welcomeCookie);
      let [tenant, company, system, systemId, systemLogo] = decodedValue.split('|');

      this.welcomeData.tenant = tenant.trim();
      this.welcomeData.company = company.trim();
      this.welcomeData.system = system.trim();
      this.welcomeData.systemId = systemId.trim();
      this.welcomeData.systemLogo = systemLogo.trim();

      sessionStorage.setItem('current-tenant', this.welcomeData.tenant);
      sessionStorage.setItem('current-company', this.welcomeData.company);
      sessionStorage.setItem('current-system', this.welcomeData.system);
      sessionStorage.setItem('current-systemId', this.welcomeData.systemId);
      sessionStorage.setItem('current-systemLogo', this.welcomeData.systemLogo);
    } else {
      this.welcomeData.tenant = sessionStorage.getItem('current-tenant');
      this.welcomeData.company = sessionStorage.getItem('current-company');
      this.welcomeData.system = sessionStorage.getItem('current-system');
      this.welcomeData.systemId = sessionStorage.getItem('current-systemId');
      this.welcomeData.systemLogo = sessionStorage.getItem('current-systemLogo');
    }

  }
  //-------------------------------------------------------
  //used for testing, can be removed later
  async getToken(): Promise<void> {
    var token = await this.tokenService.getToken();
    alert(token);
  }
  //-------------------------------------------------------
}
