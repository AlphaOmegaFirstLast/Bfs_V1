import {Component} from '@angular/core';
import {AppMenuComponent} from '@layouts/components/sidenav/components/app-menu/app-menu.component';
import {SimplebarAngularModule} from 'simplebar-angular';
import {LayoutStoreService} from '@core/services/layout-store.service';
import {NgIcon} from '@ng-icons/core';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-sidenav',
    imports: [
        AppMenuComponent,
        SimplebarAngularModule,
        NgIcon,
        RouterLink
    ],
  templateUrl: './sidenav.component.html',
})
export class SidenavComponent {
  constructor(public layout: LayoutStoreService) {
  }

  hoverSidebar() {
    this.layout.setSidenavSize(this.layout.sidenavSize === 'on-hover-active' ? 'on-hover' : 'on-hover-active')
  }

  closeSidebar() {
    const html = document.documentElement;
    html.classList.toggle('sidebar-enable')
    this.layout.hideBackdrop()
  }
}
