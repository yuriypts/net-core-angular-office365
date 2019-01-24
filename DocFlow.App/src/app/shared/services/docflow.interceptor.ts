import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/do';

import { StorageService } from './storage.service';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { AlertService } from './alert.service';

@Injectable()
export class DocFlowIntercaptor implements HttpInterceptor {
    constructor(public storageService: StorageService, public router: Router, private alertService: AlertService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const token = this.storageService.getItem('docflow_token');

        req = this.setContentType(req);
        req = this.setToken(token, req);

        return next.handle(req).do((event: HttpEvent<any>) => { }, (err: any) => {
            if (err instanceof HttpErrorResponse) {
                if (err.status === 401) {
                    this.storageService.clearStorage();
                    this.router.navigate(['/login'], {
                        queryParams: {
                            accessDenied: true
                        }
                    });
                } else {
                    this.alertService.error(err.url + ' ' + err.statusText);
                    return throwError(err);
                }
            }
        });
    }

    private setContentType(req: HttpRequest<any>): HttpRequest<any> {
        if (!req.headers.has('Content-Type')) {
            return req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
        }

        return req;
    }

    private setToken(token: string, req: HttpRequest<any>): HttpRequest<any> {
        if (token != null) {
            return req.clone({
                headers: req.headers.set('Authorization', `Bearer ${token}`)
            });
        }

        return req;
    }
}
