export enum ErrorSeverity {
  /** User-visible hint, no action needed */
  Warning = 'warning',
  /** Recoverable error – show toast / message */
  Error = 'error',
  /** Unrecoverable – redirect to error page */
  Fatal = 'fatal',
}

export interface AppError {
  message: string;
  statusCode?: number;
  url?: string;
  stack?: string;
  originalError: unknown;
  severity: ErrorSeverity;
  timestamp: Date;
}

// error.model.ts
export class FatalError extends Error {
  readonly severity = ErrorSeverity.Fatal;

  constructor(message: string) {
    super(message);
    this.name = 'FatalError';
    Object.setPrototypeOf(this, FatalError.prototype);
  }
}