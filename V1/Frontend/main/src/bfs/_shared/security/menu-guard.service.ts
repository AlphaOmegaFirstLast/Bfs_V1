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
    var currentSystem = sessionStorage.getItem('current-system') || 'infrastructure'; //default app is b.office
    var currentApp = sessionStorage.getItem('current-app') || 'b.ofc'; //default app is b.office
    var appItems = [] as MenuItemType[];

    // Based on the current app, load the corresponding menu items
    // currently designed to show one app menu at a time.
    // can be enhanced to show multiple app menu if needed in future, by adding a loop here to loop through all apps in session storage and load menu for each app.
    switch (currentSystem) {
      case 'bestfit':
        appItems = appItems.concat(await this.processItems(currentApp , BestFitMenuItems));
        break;
      case 'infrastructure':
        appItems = appItems.concat(await this.processItems(currentApp , InfrastructureMenuItems));
        break;
      //Template_System_AddMenuEntry
      default:
        break;
    }

    return appItems;
  }

  //-------------------------------------------------------------
  async processItems(currentApp: string, appItems:MenuItemType[]): Promise<MenuItemType[]> {

    // if no app defined in menu item, or app list includes current app, then show this menu item    
    appItems = appItems.filter(x => (!x.data?.app) || (x.data?.app || []).includes(currentApp));

    if (!environment.isSecurityEnabled) {
      return appItems; // Allow access if security is disabled
    }

    //Check if auth left set claims in session storage
    var tokenParsed = this.tokenService.getTokenParsed();
    if (!tokenParsed) {
      await this.tokenService.getAccessToken();
    }

    // Check "First menu level", inaccessible menu item will be filtered out, so it won't show in the menu at all.
    appItems = appItems.filter(item => this.tokenService.isAccessible(item.data));

    // Check "Second menu level", the children menu items
    appItems.forEach(item => {
      if (item.children && item.children.length > 0) {
        item.children = item.children.filter(child => this.tokenService.isAccessible(child.data));
      }
    });
    
    return appItems;
  }
}
