import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { TokenService } from '../services/token.service'; // Assume this service exists
import { environment } from '@/environment/environment'; // Assume this service exists

@Injectable({
  providedIn: 'root'
})
export class RouteGuardService implements CanActivate {

  constructor(private tokenService: TokenService, private router: Router) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean | UrlTree> {
    if (!environment.isSecurityEnabled) {
      return true; // Allow access if security is disabled
    }
    //Check if the user is authenticated
    var tokenParsed = this.tokenService.getTokenParsed();
    if (!tokenParsed) {
      await this.tokenService.getAccessToken();
    }

    if (!this.tokenService.isAccessible(route.data)) {
      // If not, redirect to a forbidden page or login
      return this.router.createUrlTree(['error/403']);
    }

    return true;
  }
}