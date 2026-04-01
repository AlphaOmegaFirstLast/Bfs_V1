import { Component, inject, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { safeHtmlDecode } from '@bfs/_shared/helpers/html.helper';
import { TokenService } from '@bfs/_shared/security/token.service';
import { appConfig } from '@/app/app.config';
import { AccessService } from '@bfs/_shared/security/access.service';
import { IQueryResponse, TokenParsed } from '@bfs/_shared/interfaces';

@Component({
  selector: 'app-home-page',
  imports: [NgIf, NgFor],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public apiUrl = 'token';
  public errorMessage: string = '';
  public tokenResponse: any;
  currentSystemApp: any;
  imgUrl!: string;
  isLoading: boolean = false;
  tokenService: TokenService = inject(TokenService);
  accessService: AccessService = inject(AccessService);

  userApps: any[] = []; // = [{ name: 'infrastructure', app: 'b.ofc' }, { name: 'stores', app: 'b.ofc' }, { name: 'auth', app: 'b.ofc' }]; // for testing only, can be removed after system selection page is implemented

  //--------------------------------------------------------------------------------------------

  ngOnInit(): void {
    // var systemApplicationsEncoded = sessionStorage.getItem('system-applications');
    // var systemApplicationsDecoded = safeHtmlDecode(systemApplicationsEncoded);

    // if (systemApplicationsDecoded) {
    //   var systemApplications = JSON.parse(systemApplicationsDecoded) ?? [];

    //   var currentApp = sessionStorage.getItem('current-app') || 'stkex.b.ofc'; //default app is b.office
    //   this.currentSystemApp = systemApplications.find((x: { name: string; }) => x.name === currentApp);
    //   this.imgUrl = 'assets/images/' + this.currentSystemApp?.system + '/' + this.currentSystemApp?.image;
    // }
    // else{
    //   this.tokenService.logout();
    // }
  }
  //--------------------------------------------------------------------------------------------

  async getToken(): Promise<void> {
    var token = await this.tokenService.getToken();
    // var infrastructureToken = await this.tokenService.getSystemToken('infrastructure');
    // alert(token);
  }
  //--------------------------------------------------------------------------------------------

  async getAccessData(): Promise<void> {
    let dataReady = await this.accessService.IsAccessServiceReady();
    if (!dataReady) {
      console.error('AccessService data is not ready');
      return;   
    }

  this.userApps = await this.accessService.getUserApps();
 //this.userApps = ['infrastructure - b.ofc','infrastructure - f.ofc', 'stores - b.ofc','stores - f.ofc', 'auth - b.ofc' ,'auth - f.ofc']; // for testing only, can be removed after system selection page is implemented
  }
  //--------------------------------------------------------------------------------------------

  async setCurrentSystem(itemSystem: string, itemApp: string): Promise<void> {
    let result = itemSystem.split("-");
    let system = result[0].trim().toLocaleLowerCase();
    let app = result[1].trim().toLocaleLowerCase();
    sessionStorage.setItem('current-system', system);
    sessionStorage.setItem('current-app', app);
    window.location.reload();
  }
  //--------------------------------------------------------------------------------------------
}
