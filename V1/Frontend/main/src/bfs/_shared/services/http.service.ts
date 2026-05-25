import { signal, inject, Injectable, ErrorHandler } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { firstValueFrom, lastValueFrom, from, Observable, throwError } from 'rxjs';
import { map, catchError, switchMap } from 'rxjs/operators';
import { TokenService } from '../security/token.service';
import { environment } from '@environment/environment';
import { FatalError } from '../error-handling/error.model';

@Injectable()
export class HttpService {
  http: HttpClient;
  tokenService: TokenService;
  private errorHandler: ErrorHandler;
  origin: string = "";
  //-------------------------------------
  constructor() {
    this.tokenService = inject(TokenService);
    this.http = inject(HttpClient);
    this.errorHandler = inject(ErrorHandler); // ✅ inject global handler
  }
  //-------------------------------------
  async get(url: string, opts = {}) {
    var target = this.origin + url;
    opts = await this.getOptions();

    return this.http.get(target, opts).pipe(
      map((res: any) => res),
      catchError((error: any) => {
        this.handleError(error);
        let bfsError = this.getMessage(error);
        return throwError(() => bfsError);
      })
    );
  }
  //-------------------------------------
  async post(url: string, data: any, opts = {}) {
    var target = this.origin + url;
    opts = await this.getOptions();

    return this.http.post(target, data, opts).pipe(
      map((res: any) => res),
      catchError((error: any) => {
        this.handleError(error);
        let bfsError = this.getMessage(error);
        return throwError(() => bfsError);
      })
    );
  }
  //-------------------------------------
  async put(url: string, data: any, opts = {}) {
    var target = this.origin + url;
    opts = await this.getOptions();

    return this.http.put(target, data, opts).pipe(
      map((res: any) => res),
      catchError((error: any) => {
        this.handleError(error);
        let bfsError = this.getMessage(error);
        return throwError(() => bfsError);
      })
    );
  }
  //-------------------------------------
  async delete(url: string, opts = {}) {
    var target = this.origin + url;
    opts = await this.getOptions();

    return this.http.delete(target, opts).pipe(
      map((res: any) => res),
      catchError((error: any) => {
        this.handleError(error);
        let bfsError = this.getMessage(error);
        return throwError(() => bfsError);
      })
    );
  }
  //-------------------------------------

  async postAutoComplete(url: string, data: any, opts = {}) {
    const target = this.origin + url;
    const headers = await this.getOptions();

    // Combine your custom headers with any passed-in options
    const finalOptions = { ...headers, ...opts };

    // 1. Create the observable
    const request$ = this.http.post(target, data, finalOptions).pipe(
      catchError((error: any) => {
        this.handleError(error);
        let bfsError = this.getMessage(error);
        return throwError(() => bfsError);
      })
    );

    // 2. Convert to Promise and await it so the method returns the data directly
    return await lastValueFrom(request$);
  }
  //-------------------------------------

  async getItems<T>(url: string, data: any, opts = {}): Promise<T> {
    const target = this.origin + url;
    try {
      opts = await this.getOptions();
      return await firstValueFrom(
        this.http.post<T>(target, data, opts)
      );
  } catch (error: any) {
      // later we want to centralize error handling in the global error handler, and we can still log the error there
      this.handleError(error);
      throw this.getMessage(error);  

    // const appError = error instanceof FatalError
    //   ? error                                          // ✅ preserve fatal as-is
    //   : new Error(this.getMessage(error).message);     // wrap regular errors
    // this.errorHandler.handleError(appError);
    // throw appError; // stops further execution in the calling component
  }
}
  //-------------------------------------

  async downloadJson(url: string, data: any, opts = {}, fileName: string) {
    const target = this.origin + url;
    opts = await this.getOptions();

    return this.http.post(target, data, opts).pipe(
      map((res: any) => {
        // Convert response to JSON string
        const jsonStr = JSON.stringify(res.items, null, 2);

        // Create a Blob with JSON MIME type
        const blob = new Blob([jsonStr], { type: 'application/json' });

        // Create a temporary download link
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = `${fileName}.json`; // filename
        link.click();

        // Cleanup
        window.URL.revokeObjectURL(link.href);

        return res; // still return response if caller needs it
      }),
      catchError((error: any) => {
        this.handleError(error);
        const bfsError = this.getMessage(error);
        return throwError(() => bfsError);
      })
    );
  }
  //-------------------------------------

  handleError(error: any): void {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    // log error to console
    console.error(errorMessage);
  }
  //-------------------------------------
  getMessage(errorResponse: any): any {
    let errorMessage = 'Something went wrong.';
    if (errorResponse.status === 0) {
      errorMessage = 'Network error or server unreachable.';

    } else if (errorResponse.status === 400) {
      errorMessage = 'Bad request. Please check the submitted data.';
      if (errorResponse.error.detail) {  // json serialization error
        errorMessage += `${errorResponse.error.detail}`;
      }
      if (errorResponse.errors) {  // business validation error
        errorMessage += errorResponse.errors.map((error: any) => ` ${error}`).join(', ');
      }
      if (errorResponse.error.errors) {  // business validation error
        let inputErrors = errorResponse.error.errors;
        if (!(inputErrors === null || inputErrors === undefined)){
           inputErrors = Array.isArray(inputErrors) ? inputErrors : [inputErrors];
        }
        errorMessage += inputErrors.map((error: any) => ` ${error}`).join(', ');
      }
    } else if (errorResponse.status === 401) {
      errorMessage = 'Unauthorized access. Please log in again.';
    } else if (errorResponse.status === 403) {
      errorMessage = 'You do not have permission to perform this action.';
    } else if (errorResponse.status === 404) {
      errorMessage = 'The requested resource was not found.';
    } else if (errorResponse.status === 500) {
      errorMessage = 'Server error. Please try again later. [' + errorResponse.error.detail + ']';
    }
    var err = { message: errorMessage + ' ' + errorResponse.message };
    return err;
    // this.notificationService.showError(message);
  }
  //-------------------------------------
  private async getOptions(): Promise<{ headers: HttpHeaders; withCredentials: boolean }> {
    const headers = await this.getHeaders();
    //withCredentials is false because we are using token-based auth, not cookie-based auth
    return { headers: headers, withCredentials: false };
  }
  //-------------------------------------
  private async getHeaders(): Promise<HttpHeaders> {

    var headers = new HttpHeaders().set('Content-Type', 'application/json');

    const jwtToken = await this.tokenService.getToken();
    if (jwtToken) {
      headers = headers.set('Authorization', 'Bearer ' + jwtToken);
    } else {
      if (environment.isSecurityEnabled) {
        throw new FatalError('No token available'); // ✅ marked as fatal at the source
      }
    }

    return headers;
  }
  //-------------------------------------
}