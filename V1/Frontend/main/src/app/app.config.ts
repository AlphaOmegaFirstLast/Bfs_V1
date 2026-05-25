import { ApplicationConfig, ErrorHandler, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { DecimalPipe } from '@angular/common'
import { provideDaterangepickerLocale} from 'ngx-daterangepicker-bootstrap';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpService } from '@bfs/_shared/services/http.service';
import { TokenService } from '@bfs/_shared/security/token.service';
import { RouteGuardService } from '@bfs/_shared/security/route-guard.service';
import { MenuGuardService } from '@bfs/_shared/security/menu-guard.service';

import { GlobalErrorHandler } from '@bfs/_shared/error-handling/global-error-handling.service';
import { httpErrorInterceptor } from '@bfs/_shared/error-handling/http-error.interceptor';

import { StoresService } from '@bfs/stores-main/stores.service';
import { AuthService } from '@bfs/auth-main/auth.service';
import { AccessService } from '@bfs/_shared/security/access.service';
import { MasterService } from '@bfs/master-main/master.service';
//Template_System_DeclareProviderEntry

// configure the providers for the application which will be used for dependency injection
export const appConfig: ApplicationConfig = {
  providers: [
      DecimalPipe,
      //Register the HTTP interceptor
      //provideHttpClient(withInterceptors([httpErrorInterceptor])),
      provideHttpClient(),
      //Replace Angular's default ErrorHandler
      { provide: ErrorHandler, useClass: GlobalErrorHandler },
  
      HttpService, //is added to Angular’s dependency injection container. it can be injected into constructors of components and other services.
      TokenService,
      AccessService,
StoresService,
AuthService,
MasterService,
//Template_System_AddProviderEntry
      RouteGuardService,
      MenuGuardService,
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideRouter(routes),
      provideAnimations(),
      provideDaterangepickerLocale({
          separator: ' - ',
          cancelLabel: 'Cancel',
      })
  ],
};
