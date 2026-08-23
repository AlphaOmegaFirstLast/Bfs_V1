import { Injectable } from '@angular/core';
import { MenuItemType } from '@/app/types/layout';
import { AccessService } from './access.service'; // Assume this service exists

import { StoresMenuItems } from '@bfs/stores-main/stores.menu';
import { AuthMenuItems } from '@bfs/auth-main/auth.menu';
import { MasterMenuItems } from '@bfs/master-main/master.menu';

import { StockExMenuItems } from '@bfs/stockex-main/stockex.menu';

//Template_System_AddMenuDeclare

@Injectable({
  providedIn: 'root'
})
export class MenuGuardService {

  constructor(private accessService: AccessService) { }

  async getMenuItems(): Promise<MenuItemType[]> {
    let dataReady = await this.accessService.IsAccessServiceReady();
    if (!dataReady) {
      console.error('AccessService data is not ready');
      return [];
    }
    var currentSystem = sessionStorage.getItem('current-system');
    var currentApp = sessionStorage.getItem('current-app');
    if (!currentSystem || !currentApp) {
      console.error('Current system or app is not set in session storage');
      return [];
    }

    // Based on the current app, load the corresponding menu items
    // currently designed to show one app menu at a time.
    // can be enhanced to show multiple app menu if needed in future, by adding a loop here to loop through all apps in session storage and load menu for each app.

    var appItems = [] as MenuItemType[];
    switch (currentSystem.toLowerCase()) {
      case 'stores':
        appItems = appItems.concat(await this.processItems(currentApp, StoresMenuItems));
        break;
      case 'auth':
        appItems = appItems.concat(await this.processItems(currentApp, AuthMenuItems));
        break;
      case 'master':
        appItems = appItems.concat(await this.processItems(currentApp, MasterMenuItems));
        break;

        case 'stockex':
           appItems = appItems.concat(await this.processItems(currentApp , StockExMenuItems));
        break;
//Template_System_AddMenuEntry
      default:
        break;
    }

    return appItems;
  }

  //-------------------------------------------------------------
  async processItems(currentApp: string, appItems: MenuItemType[]): Promise<MenuItemType[]> {

    // if no app defined in menu item, or app list includes current app, then show this menu item    
    appItems = appItems.filter(x => (!x.data?.app) || (x.data?.app || []).includes(currentApp));

    // Check "First menu level", inaccessible menu item will be filtered out, so it won't show in the menu at all.
    appItems = appItems.filter(item => this.accessService.isAccessible(item.data));

    // Check "Second menu level", the children menu items
    appItems.forEach(item => {
      if (item.children && item.children.length > 0) {
        item.children = item.children.filter(child => this.accessService.isAccessible(child.data));
      }
    });

    return appItems;
  }
}
