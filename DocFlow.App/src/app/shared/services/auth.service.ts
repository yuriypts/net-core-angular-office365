import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { StorageService } from './storage.service';


@Injectable()
export class AuthService {
    constructor(public http: HttpClient, public router: Router, public storageService: StorageService) {
    }

    login(login: any): Observable<any> {
        return this.http.post(`/api/account/login`, login); // call api here
    }

    logout() {
        this.storageService.clearStorage();
    }

    isLoggetIn(): boolean {
         const res = this.storageService.getItem('docflow_token');
         return res != null ? true : false;
    }
}
