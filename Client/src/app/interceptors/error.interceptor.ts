import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NavigationExtras, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private snackBar: MatSnackBar) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error) => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat();
              } else if (typeof error.error === 'object') {
                this.snackBar.open(error.error.title, error.status);
              } else {
                this.snackBar.open(error.error, error.status);
              }
              break;
            case 401:
              this.snackBar.open(
                error.error === null ? 'Unauthorised' : error.error,
                error.status
              );
              break;
            case 404:
              this.router.navigateByUrl('error/not-found');
              break;
            case 500:
              console.log('500');
              const navigationExtras: NavigationExtras = {
                state: { error: error.error },
              };
              this.router.navigateByUrl('error/server', navigationExtras);
              break;
            default:
              this.snackBar.open('Something unexpected went wrong', 'x');
              console.log(error);
              break;
          }
        }
        return throwError(() => new Error(error));
      })
    );
  }
}
