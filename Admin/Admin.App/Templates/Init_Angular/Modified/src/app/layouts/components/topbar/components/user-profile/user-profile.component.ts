import {Component, inject} from '@angular/core';
import {NgbDropdown, NgbDropdownMenu, NgbDropdownToggle} from "@ng-bootstrap/ng-bootstrap";
import {userDropdownItems} from '@layouts/components/data';
import {RouterLink} from '@angular/router';
import {NgIcon} from '@ng-icons/core';
import { TokenService } from '@bfs/_shared/services/token.service';

@Component({
  selector: 'app-user-profile-topbar',
  imports: [
    NgbDropdown,
    NgbDropdownMenu,
    NgbDropdownToggle,
    RouterLink,
    NgIcon
  ],
  templateUrl: './user-profile.component.html'
})
export class UserProfileComponent {
  menuItems = userDropdownItems;
  tokenService: TokenService= inject(TokenService);
  logout(): void {
    this.tokenService.logout();
  }
}