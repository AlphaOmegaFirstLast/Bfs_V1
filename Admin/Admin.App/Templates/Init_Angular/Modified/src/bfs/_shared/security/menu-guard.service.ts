import { Injectable } from '@angular/core';
import { MenuItemType } from '@/app/types/layout';
import { TokenService } from '../services/token.service'; // Assume this service exists
import { environment } from '@/environment/environment'; // Assume this service exists
import { BestFitMenuItems } from '../../bestfit-main/bestfit.menu'; // Assume this service exists
import { InfrastructureMenuItems } from '@bfs/infrastructure-main/infrastructure.menu';
//Template_System_AddMenuDeclare

@Injectable({
  providedIn: 'root'
})
export class MenuGuardService {

  constructor(private tokenService: TokenService) { }

  async getMenuItems(): Promise<MenuItemType[]> {
    var currentApp = sessionStorage.getItem('current-app') || 'bestfit.b.ofc'; //default app is b.office
    var appItems = [] as MenuItemType[];

    // appItems = appItems.concat(await this.processItems(currentApp , BestFitMenuItems));
    appItems = appItems.concat(await this.processItems(currentApp , InfrastructureMenuItems));
    //Template_System_AddMenuEntry
    return appItems;
  }

  //-------------------------------------------------------------
  async processItems(currentApp: string, appItems:MenuItemType[]): Promise<MenuItemType[]> {
        
    appItems = appItems.filter(x => (x.data?.app || []).includes(currentApp));

    if (!environment.isSecurityEnabled) {
      return appItems; // Allow access if security is disabled
    }

    //Check if auth left set claims in session storage
    var tokenParsed = this.tokenService.getTokenParsed();
    if (!tokenParsed) {
      await this.tokenService.getAccessToken();
    }
    // First menu level check
    appItems = appItems.filter(item => this.tokenService.isAccessible(item.data));
    // Check children menu items
    appItems.forEach(item => {
      if (item.children && item.children.length > 0) {
        item.children = item.children.filter(child => this.tokenService.isAccessible(child.data));
      }
    });
    return appItems;
  }
}
