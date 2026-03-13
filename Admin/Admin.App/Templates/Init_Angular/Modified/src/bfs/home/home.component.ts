import { Component, inject, OnInit } from '@angular/core';
import { NgIf , NgFor} from '@angular/common';
import { safeHtmlDecode } from '@bfs/_shared/helpers/html.helper';
import { TokenService } from '@bfs/_shared/services/token.service';
import { appConfig } from '@/app/app.config';

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
  tempSystems = [{ name: 'infrastructure', app: 'b.ofc' }, { name: 'stores', app: 'b.ofc' }, { name: 'auth', app: 'b.ofc' }]; // for testing only, can be removed after system selection page is implemented

  //--------------------------------------------------------------------------------------------

  ngOnInit(): void {
    var systemApplicationsEncoded = sessionStorage.getItem('system-applications');
    var systemApplicationsDecoded = safeHtmlDecode(systemApplicationsEncoded);

    if (systemApplicationsDecoded) {
      var systemApplications = JSON.parse(systemApplicationsDecoded) ?? [];

      var currentApp = sessionStorage.getItem('current-app') || 'stkex.b.ofc'; //default app is b.office
      this.currentSystemApp = systemApplications.find((x: { name: string; }) => x.name === currentApp);
      this.imgUrl = 'assets/images/' + this.currentSystemApp?.system + '/' + this.currentSystemApp?.image;
    }
    else{
      this.tokenService.logout();
    }
  }
  //--------------------------------------------------------------------------------------------
  
  setCurrentSystem(system: string, app: string): void {
    sessionStorage.setItem('current-system', system);
    sessionStorage.setItem('current-app', app);
    window.location.reload();
  }
  //--------------------------------------------------------------------------------------------
}
