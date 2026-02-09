import { ChangeDetectionStrategy, Component, signal, inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { TokenService } from './token.service';

@Injectable()
export class HttpService {
  http: HttpClient;
  tokenService: TokenService;
  origin: string = "";
  //-------------------------------------
  constructor() { 
    this.tokenService = inject(TokenService);
    this.http = inject(HttpClient);
  }
  //-------------------------------------
  async get(url: string, opts = {}) {
    var target = this.origin + url;
    opts = await this.getHeaders();

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
    opts = await this.getHeaders();

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
    opts = await this.getHeaders();

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
    opts = await this.getHeaders();

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
async downloadJson(url: string, data: any, opts = {}, fileName:string) {
  const target = this.origin + url;
  opts = await this.getHeaders();

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
        errorMessage += errorResponse.error.errors.map((error: any) => ` ${error}`).join(', ');
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
    var err = {message: errorMessage + ' ' + errorResponse.message} ;
    return err;

    // this.notificationService.showError(message);
  }
  //-------------------------------------
  private async getHeaders(): Promise<{ headers: HttpHeaders; withCredentials: boolean }> {

    var headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    const accessToken = await this.tokenService.getAccessToken();
    if (accessToken) {
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    return { headers: headers, withCredentials: false };
  }
  //-------------------------------------
}