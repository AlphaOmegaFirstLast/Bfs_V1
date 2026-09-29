import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, map, of, Subject, defer, firstValueFrom, forkJoin } from 'rxjs';
import { shareReplay, catchError, tap, finalize } from 'rxjs/operators';
import { environment } from '@/environment/environment';
import { safeHtmlDecode } from '../helpers/html.helper';
import { TokenModel, TokenParsed } from '../interfaces';

@Injectable({ providedIn: 'root' })  // Ensure the service is a singleton and available application-wide
export class TokenService {

  http: HttpClient;
  //getTokenUrl: string = '/token';

  refreshTokenUrl: string = '/token/refresh'; // it calls method refresh of TokenController, which will use the refresh token stored in cookie to get a new access token. the new access token will be returned in the response body, and the old refresh token will be replaced by a new refresh token in the cookie.
  logoutUrl: string = '/token/logout';
  identityWebOrigin: string = environment.identityWebOrigin;
  tokenModel: TokenModel | null = null;

  private inFlightRequest$!: Observable<string> | null; // inFlightRequest known techniqueTo track ongoing token requests
  constructor() {
    this.http = inject(HttpClient);
  }
  //------------------------------------------------------------

  async getToken(): Promise<string | null> {
    var jwtToken: string | null = null;
    var urlToken!: string;

    if (environment.isSecurityEnabled === false) {
      return 'isSecurityEnabled=False-ThisIsDummyToken';
    }

    const model = this.getTokenModel();
    if (model && model.token && model.tokenParsed?.exp && model.tokenParsed.exp > Date.now() / 1000) { // If the token is still valid, return it. Date is referencing the Epoch time in seconds
      jwtToken = model.token;
    } else {
      console.log('token expired, attempting to refresh');
      urlToken = this.refreshTokenUrl; // if the token is expired, the request picks up the refreshtoken that is stored in the cookie. it is not stored in the session or local storage
    }
    //--------------
    if (urlToken) {
      try {
        // Convert the Observable to a Promise and await its resolution
        const tokenObservable = defer(() => this.getTokenObservable(urlToken));
        jwtToken = await firstValueFrom(tokenObservable);
      } catch (error) {
        if (environment.isSecurityEnabled) {
          this.logout();         // call logout to clear any cookies and tokenModel
        }
        return null;
      }
    }

    return jwtToken;
  }
  //------------------------------------------------------------

  logout() {

    sessionStorage.clear(); // Clear session storage to remove any stored token information
    //call backend to invalidate the refreshToken
    const target = this.identityWebOrigin + this.logoutUrl;

    // The Observable is created and piped with operators for data transformation and error handling.
    var observable = this.http.get<string>(target).pipe(
      map((response: any) => {
        console.log('logout response', response);
        return response as string;
      }),

      tap((response: string) => {
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

    observable.subscribe({
      next: () => {
        console.log('Logged out successfully');
        sessionStorage.clear();
        window.location.href = environment.loginUrl;
      }
    });
  }
  //------------------------------------------------------------
  getTokenObservable(url: string): Observable<string | null> {
    if (this.inFlightRequest$) {
      return this.inFlightRequest$;
    }
    const target = this.identityWebOrigin + url;
    //withCredentials is required for the browser to include cookies in the request, which is necessary for the refresh token flow to work, as the refresh token is stored in an HttpOnly cookie for security reasons. The server will look for the refresh token in the cookie when processing the refresh request, and if withCredentials is not set to true, the cookie will not be sent, and the server will not be able to authenticate the request to issue a new access token.
    const opts = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }), withCredentials: true };

    // The Observable is created and piped with operators for data transformation and error handling.
    var tokenObservable = this.http.get(target, opts).pipe(

      map((response: any) => {
        const jwtToken =  response.jwtToken as string ;
        const isValid = this.setTokenModel(jwtToken);
        return isValid ? jwtToken : ''
      }),

      // Map and error handling as in your original code
      catchError((_error: any) => {
        console.error('Token fetch error', _error);
        return ''; // Return an empty string on error to indicate failure without throwing, allowing the app to handle it gracefully'';
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
  setTokenModel(jwtToken: string | null): boolean {
    this.tokenModel = null;
    if (!jwtToken) {
      console.error('Invalid token: null');
      return false;
    }
    const parsedToken = this.parseJwt(jwtToken);
    if (!parsedToken || !parsedToken.userId) { // in case a user has a login to BestFit Tenant but doesn't have a record in athUser.
      console.error('Invalid token: missing userId');
      return false;
    }
    this.tokenModel = { token: jwtToken, tokenParsed: parsedToken };
    sessionStorage.setItem('token', jwtToken);
    sessionStorage.setItem('token-parsed', JSON.stringify(parsedToken));
    return true;
  }
  //------------------------------------------------------------
  getTokenModel(): TokenModel | null {
    let tokenModel = {
      token: sessionStorage.getItem('token'),
      tokenParsed: JSON.parse(safeHtmlDecode(sessionStorage.getItem('token-parsed')?.toString()) || 'null')
    }
    this.tokenModel = tokenModel.token ? tokenModel : null; // Update the in-memory token model if a token is found in session storage, otherwise set it to null
    return this.tokenModel; // Return the token model from memory if available
  }
  //------------------------------------------------------------
  getTokenParsed(): TokenParsed | null {
    return this.tokenModel?.tokenParsed || JSON.parse(safeHtmlDecode(sessionStorage.getItem('token-parsed')?.toString()) || 'null');
  }
  //------------------------------------------------------------
  parseJwt(token: string): TokenParsed | null {
    const tokenPayload = token.split('.')[1];
    const payloadJson = atob(tokenPayload);
    const payload = JSON.parse(payloadJson);
    const tokenParsed = payload as TokenParsed;
    return tokenParsed;
  }
  //------------------------------------------------------------
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
