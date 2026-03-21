import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { AccessService } from './access.service'; // Assume this service exists

@Injectable({
  providedIn: 'root'
})
export class RouteGuardService implements CanActivate {

  constructor(private accessService: AccessService, private router: Router) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean | UrlTree> {
    // Check if the user has access to the route based on the route data and user permissions
    if (!this.accessService.isAccessible(route.data)) {
      // If not, redirect to a forbidden page or login
      return this.router.createUrlTree(['error/403']);
    }

    return true;
  }
}