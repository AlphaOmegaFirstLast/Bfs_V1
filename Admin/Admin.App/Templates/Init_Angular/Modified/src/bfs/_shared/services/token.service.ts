import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, map, of, Subject, defer, firstValueFrom } from 'rxjs';
import { shareReplay, catchError, tap, finalize } from 'rxjs/operators';
import { environment } from '@/environment/environment';
import { setCookie, getCookie } from '../helpers/cookie.helper';
import { safeHtmlDecode } from '../helpers/html.helper';

interface AuthorizationToken {
  accessToken: string;
  refreshToken?: string; // Optional, if your API provides it
}

interface TokenParsed {
  userId: string;
  exp: number; // e.g. offsetSeconds from Date.now()/1000
  role: string[];
  app: string[];
  api: string[];
  method: string[];
}

interface TokenModel {
  authorizationToken: AuthorizationToken;
  tokenParsed: TokenParsed | null;
}

@Injectable({ providedIn: 'root' })  // Ensure the service is a singleton and available application-wide
export class TokenService {

  http: HttpClient;
  getTokenUrl: string = '/token';
  refreshTokenUrl: string = '/token/refresh';
  logoutUrl: string = '/token/logout';
  identityWebOrigin: string = environment.tokenUrl;
  tokenModel: TokenModel | null = null;

  private inFlightRequest$!: Observable<AuthorizationToken> | null; // inFlightRequest known techniqueTo track ongoing token requests
  constructor() {
    this.http = inject(HttpClient);
  }
  //------------------------------------------------------------

  async getAccessToken(): Promise<string | null> {
    var accessToken: string | null = null;
    var urlToken!: string;

    if (environment.isSecurityEnabled === false) {
      return 'isSecurityEnabled=False-ThisIsDummyToken';
    }

    const model = this.getTokenModel();
    if (model?.authorizationToken?.accessToken) {
      if (model.tokenParsed?.exp && model.tokenParsed.exp > Date.now() / 1000) { // If the token is still valid, return it. Date is referencing the Epoch time in seconds
        accessToken = model.authorizationToken.accessToken;
      } else {
        console.log('token expired, attempting to refresh');
        urlToken = this.refreshTokenUrl; // if the token is expired, the request picks up the refreshtoken that is stored in the cookie. it is not stored in the session or local storage
      }
    } else {
      urlToken = this.getTokenUrl; // If there's no token model or access token, fetch a new token
    }

    if (urlToken) {
      // Convert the Observable to a Promise and await its resolution
      try {
        const tokenObservable = defer(() => this.getTokenObservable(urlToken));
        const token = await firstValueFrom(tokenObservable);
        accessToken = token.accessToken;
      } catch (error) {
        // call logout to clear any cookies and tokenModel
        if (environment.isSecurityEnabled){
          this.logout();
        }
        return null;
      }
    }

    return accessToken;
  }
  //------------------------------------------------------------
  logout() {
    //call backend to invalidate the refreshToken
    const target = this.identityWebOrigin + this.logoutUrl;

    // The Observable is created and piped with operators for data transformation and error handling.
    var tokenObservable = this.http.get<AuthorizationToken>(target).pipe(

      map((response: any) => {
        console.log('token response', response);
        return response as AuthorizationToken;
      }),

      tap((authorizationToken: AuthorizationToken) => {
        this.setTokenModel(null);
      }),

      // Map and error handling as in your original code
      catchError((_error: any) => {
        return throwError(() => 'logout error');
      }),

      finalize(() => {
        this.inFlightRequest$ = null;
      }),

      // shareReplay(1) ensures the request is only made once and the result is replayed to all
      // subsequent subscribers, even if they subscribe after the request completes.
      shareReplay({ bufferSize: 1, refCount: true })
    );
    tokenObservable.subscribe({
      next: () => {
        console.log('Logged out successfully');
        sessionStorage.clear();
        window.location.href = environment.loginUrl;
      }
    });
  }
  //------------------------------------------------------------
  getTokenObservable(url: string): Observable<AuthorizationToken> {
    if (this.inFlightRequest$) {
      return this.inFlightRequest$;
    }

    const target = this.identityWebOrigin + url;
    const opts = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }), withCredentials: true };

    // The Observable is created and piped with operators for data transformation and error handling.
    var tokenObservable = this.http.get<AuthorizationToken>(target, opts).pipe(

      map((response: any) => {
        console.log('token response', response);
        return response as AuthorizationToken;
      }),

      tap((authorizationToken: AuthorizationToken) => {
        this.setTokenModel(authorizationToken);
      }),

      // Map and error handling as in your original code
      catchError((_error: any) => {
        return throwError(() => 'token fetch error');
      }),

      finalize(() => {
        this.inFlightRequest$ = null;
      }),

      // shareReplay(1) ensures the request is only made once and the result is replayed to all
      // subsequent subscribers, even if they subscribe after the request completes.
      shareReplay({ bufferSize: 1, refCount: true })
    );

    // Store the observable so subsequent calls get the same instance. As at app setup each api requests a token at the same time, we need to ensure only one request is made.
    this.inFlightRequest$ = tokenObservable;
    return tokenObservable;
  }
  //------------------------------------------------------------

  setTokenModel(authorizationToken: AuthorizationToken | null) {
    if (!authorizationToken) {
      this.tokenModel = null;
      return;
    }
    const parsedToken = this.parseJwt(authorizationToken.accessToken);
    this.tokenModel = { authorizationToken, tokenParsed: parsedToken };
  }
  //------------------------------------------------------------
  getTokenModel(): TokenModel | null {
    return this.tokenModel;
  }
  //------------------------------------------------------------
  getTokenParsed(): TokenParsed | null {
    return this.tokenModel?.tokenParsed || JSON.parse(safeHtmlDecode(sessionStorage.getItem('token-parsed')?.toString()) || 'null');
  }
  //------------------------------------------------------------
  getUserRoles(): string[] {
    const tokenParsed = this.getTokenParsed();
    return tokenParsed?.role || [];
  }
  //------------------------------------------------------------
  getUserApis(): string[] {
    const tokenParsed = this.getTokenParsed();
    return tokenParsed?.api || [];
  }
  //------------------------------------------------------------
  getUserApps(): string[] {
    const tokenParsed = this.getTokenParsed();
    return tokenParsed?.app || [];
  }
  //------------------------------------------------------------
  getUserMethods(): string[] {
    const tokenParsed = this.getTokenParsed();
    return tokenParsed?.method || [];
  }
  //------------------------------------------------------------
  parseJwt(token: string): TokenParsed | null {
    const tokenPayload = token.split('.')[1];
    try {
      const payloadJson = atob(tokenPayload);
      const payload = JSON.parse(payloadJson);
      return payload as TokenParsed;
    } catch (error) {
      console.error('Failed to decode or parse token payload:', error);
      return null;
    }
  }
  //------------------------------------------------------------
  // to handle a special case when an array has only one value, it is passed as a string.
  ensureArray(value: null|undefined|string | string[]): string[] {
    if (value === null || value === undefined) {
      return [];
    }
    return Array.isArray(value) ? value : [value];
  }
  //------------------------------------------------------------

  isAccessible(data: any): boolean {
    if (environment.isSecurityEnabled === false) {
      return true; // Allow access if security is disabled
    }
    // Get the required permissions by the requester
    let requiredRoles = this.ensureArray((data['role'] as string[]));
    let requiredApis = this.ensureArray((data['api'] as string[]));
    let requiredApps = this.ensureArray((data['app'] as string[]));
    let requiredMethods = this.ensureArray((data['method'] as string[]));

    // Get the current user's permissions from token service
    let userRoles = this.ensureArray(this.getUserRoles() as string[]);
    let userApis = this.ensureArray(this.getUserApis() as string[]);
    let userApps = this.ensureArray(this.getUserApps() as string[]);
    let userMethods = this.ensureArray(this.getUserMethods() as string[]);

    if (userRoles.includes('bfs.admin')) {
      return true; // BfsAdmin has access to everything
    }
    requiredRoles = requiredRoles.map(r => r.toLowerCase());
    requiredApis = requiredApis.map(r => r.toLowerCase());
    requiredApps = requiredApps.map(r => r.toLowerCase());
    requiredMethods = requiredMethods.map(r => r.toLowerCase());
    userRoles = userRoles.map(r => r.toLowerCase());
    userApis = userApis.map(r => r.toLowerCase());
    userApps = userApps.map(r => r.toLowerCase());
    userMethods = userMethods.map(r => r.toLowerCase());

    // Check if the user has a required role, api, or app. it is case sensitive, so ensure the values are consistent
    // .some is more efficient than .includes in this case as it stops checking as soon as a match is found
    if ((requiredRoles.length === 0  || requiredRoles.some(role => userRoles.some(userRole => userRole === role))) &&
      (requiredApis.length === 0 || requiredApis.some(api => userApis.some(userApi => userApi === api))) &&
      (requiredApps.length === 0  || requiredApps.some(app => userApps.some(userApp => userApp === app))) &&
      (requiredMethods.length === 0 || requiredMethods.some(method => userMethods.some(userMethod => userMethod === method)))) {
      return true; // Allow access
    }

    return false;
  }

}

/* Explanation of the differences between .pipe().subscribe() and await firstValueFrom().pipe():
.pipe() is for data manipulation and stream transformation. 
It's where you define the processing steps that the data will go through before it gets to the final consumer. The result of a pipe is a new, transformed Observable.

.subscribe() is for data consumption. and gets the observable to execute.
It's where you define what happens to the final, processed value. It's the "end of the line" where you handle the success (next) and error (error) scenarios.

.subscribe() is the native way to activate an Observable and handle its emitted values and errors directly with callback functions (next, error). This is the classic RxJS approach.

await is a more modern, syntactic sugar over Promises. It pauses the execution of an async function until the Promise it's waiting on is resolved.

By using firstValueFrom in your pipe, you are effectively converting the RxJS stream into a Promise, which allows you to use await.
*/
