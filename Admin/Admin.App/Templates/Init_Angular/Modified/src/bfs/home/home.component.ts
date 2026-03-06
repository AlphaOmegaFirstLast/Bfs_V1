import { Component, inject, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { safeHtmlDecode } from '@bfs/_shared/helpers/html.helper';
import { TokenService } from '@bfs/_shared/services/token.service';

@Component({
  selector: 'app-home-page',
  imports: [NgIf],
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

}
