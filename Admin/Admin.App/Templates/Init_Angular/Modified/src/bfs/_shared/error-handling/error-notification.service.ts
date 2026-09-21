import { Injectable, signal } from '@angular/core';
import { AppError, ErrorSeverity } from './error.model';

export interface ErrorToast {
  id: number;
  message: string;
  severity: ErrorSeverity;
}

/**
 * Exposes a reactive `toasts` signal consumed by your toast/snackbar component.
 * Swap `pushToast` for your preferred UI library (Angular Material, PrimeNG, etc.).
 */
@Injectable({ providedIn: 'root' })
export class ErrorNotificationService {
  readonly toasts = signal<ErrorToast[]>([]);

  private nextId = 0;

  notify(error: AppError): void {
    // Warnings & errors surface as toasts; fatals are handled via navigation
    if (error.severity === ErrorSeverity.Fatal) return;

    const toast: ErrorToast = {
      id: ++this.nextId,
      message: this.userMessage(error),
      severity: error.severity,
    };

    this.pushToast(toast);
  }

  dismiss(id: number): void {
    this.toasts.update(list => list.filter(t => t.id !== id));
  }

  // ─── Helpers ────────────────────────────────────────────────────────────────

  private pushToast(toast: ErrorToast, durationMs = 5_000): void {
    this.toasts.update(list => [...list, toast]);
    setTimeout(() => this.dismiss(toast.id), durationMs);
  }

  private userMessage(error: AppError): string {
    switch (error.statusCode) {
      case 0:   return 'No network connection. Please check your internet.';
      case 401: return 'Your session has expired. Please sign in again.';
      case 403: return 'You don\'t have permission to perform this action.';
      case 404: return 'The requested resource was not found.';
      case 422: return 'The submitted data is invalid. Please check and retry.';
      case 429: return 'Too many requests – please wait a moment and retry.';
      case 503: return 'Service temporarily unavailable. Please try later.';
    }

    if (error.statusCode && error.statusCode >= 500) {
      return 'A server error occurred. Our team has been notified.';
    }

    return error.message || 'An unexpected error occurred.';
  }
}