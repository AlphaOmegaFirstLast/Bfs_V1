import { Injectable } from '@angular/core';
import { AppError, ErrorSeverity } from './error.model';

/** Replace the body of `sendToServer` with your real logging endpoint. */
@Injectable({ providedIn: 'root' })
export class ErrorLoggerService {

  log(error: AppError): void {
    this.logToConsole(error);
    this.sendToServer(error);
  }

  // ─── Console ────────────────────────────────────────────────────────────────

  private logToConsole(error: AppError): void {
    const style = this.severityStyle(error.severity);
    console.group(`%c[${error.severity.toUpperCase()}] ${error.message}`, style);
    console.log('Timestamp:', error.timestamp.toISOString());
    if (error.statusCode) console.log('Status:', error.statusCode);
    if (error.url)        console.log('URL:', error.url);
    if (error.stack)      console.log('Stack:', error.stack);
    console.groupEnd();
  }

  private severityStyle(severity: ErrorSeverity): string {
    const base = 'font-weight:bold;padding:2px 6px;border-radius:3px;';
    switch (severity) {
      case ErrorSeverity.Warning: return `${base}background:#f59e0b;color:#000`;
      case ErrorSeverity.Error:   return `${base}background:#ef4444;color:#fff`;
      case ErrorSeverity.Fatal:   return `${base}background:#7c3aed;color:#fff`;
    }
  }

  // ─── Remote logging ─────────────────────────────────────────────────────────

  private sendToServer(error: AppError): void {
    // Example: replace with Sentry, Datadog, or your own endpoint
    //
    // fetch('/api/errors', {
    //   method: 'POST',
    //   headers: { 'Content-Type': 'application/json' },
    //   body: JSON.stringify({
    //     message:    error.message,
    //     severity:   error.severity,
    //     statusCode: error.statusCode,
    //     url:        error.url,
    //     stack:      error.stack,
    //     timestamp:  error.timestamp,
    //   }),
    // }).catch(() => { /* swallow logging failures */ });
  }
}