import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ErrorNotificationService } from './error-notification.service';
import { AppError, ErrorSeverity } from './error.model';

/**
 * Functional HTTP interceptor that catches errors from every HttpClient call
 * and routes them through ErrorNotificationService.
 *
 * Register in app.config.ts:
 *   provideHttpClient(withInterceptors([httpErrorInterceptor]))
 */
export const httpErrorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
) => {
  const notifier = inject(ErrorNotificationService);

  return next(req).pipe(
    catchError(err => {
      const appError: AppError = {
        message: err?.message ?? 'HTTP request failed',
        statusCode: err?.status,
        url: req.urlWithParams,
        originalError: err,
        severity: err?.status >= 500 ? ErrorSeverity.Fatal : ErrorSeverity.Error,
        timestamp: new Date(),
      };

      notifier.notify(appError);

      // Re-throw so components can still subscribe to error state if needed
      return throwError(() => appError);
    }),
  );
};