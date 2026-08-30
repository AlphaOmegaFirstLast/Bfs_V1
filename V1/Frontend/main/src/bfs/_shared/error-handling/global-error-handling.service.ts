import { ErrorHandler, Injectable, Injector, NgZone, inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ErrorLoggerService } from './error-logger.service';
import { ErrorNotificationService } from './error-notification.service';
import { AppError, ErrorSeverity, FatalError } from './error.model';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  private readonly injector = inject(Injector);   // ✅ safe — no cycle

  // Lazily resolved on first error, not at construction time
  private get router() { return this.injector.get(Router); }
  private get zone() { return this.injector.get(NgZone); }
  private get logger() { return this.injector.get(ErrorLoggerService); }
  private get notifier() { return this.injector.get(ErrorNotificationService); }

  //---------------------------------------
  handleError(error: unknown): void {
   
    const appError = this.normalizeError(error);

    // Always log the error
    this.logger.log(appError);

    // Run notification + navigation inside Angular zone (errors may originate outside)
    this.zone.run(() => {
      this.notifier.notify(appError);

      if (appError.severity === ErrorSeverity.Fatal) {
        this.router.navigate(['/error'], {
          queryParams: { code: appError.statusCode ?? 500 },
        });
      }
    });

    // Re-throw in dev so DevTools still shows the stack
    if (!environment.production) {
      console.warn('[GlobalErrorHandler]', appError); //console.error causes recursive calls to handleError() in dev mode
    }
  }
  //---------------------------------------
  private normalizeError(raw: unknown): AppError {
    if (raw instanceof FatalError) {          // ✅ new branch — check first
      return {
        message: raw.message,
        stack: raw.stack,
        originalError: raw,
        severity: ErrorSeverity.Fatal,        // guaranteed redirect to /error
        timestamp: new Date(),
      };
    }

    if (raw instanceof HttpErrorResponse) {
      return this.fromHttpError(raw);
    }

    if (raw instanceof Error) {
      return this.fromClientError(raw);
    }

    return {
      message: String(raw),
      originalError: raw,
      severity: ErrorSeverity.Error,
      timestamp: new Date(),
    };
  }
  //---------------------------------------
  private fromHttpError(err: HttpErrorResponse): AppError {
    const statusCode = err.status;
    const severity =
      statusCode === 0
        ? ErrorSeverity.Fatal          // Network / CORS failure
        : statusCode >= 500
          ? ErrorSeverity.Fatal
          : statusCode === 401 || statusCode === 403
            ? ErrorSeverity.Error
            : ErrorSeverity.Warning;

    return {
      message: err.message,
      statusCode,
      url: err.url ?? undefined,
      originalError: err,
      severity,
      timestamp: new Date(),
    };
  }
  //---------------------------------------
  private fromClientError(err: Error): AppError {
    const isFatal =
      err instanceof RangeError ||
      err.message.toLowerCase().includes('chunk load error');

    return {
      message: err.message,
      stack: err.stack,
      originalError: err,
      severity: isFatal ? ErrorSeverity.Fatal : ErrorSeverity.Error,
      timestamp: new Date(),
    };
  }
}
//---------------------------------------
// Minimal env shim – replace with your real environment import
const environment = { production: false };