import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import { Observable } from 'rxjs';
import { SRV_URL } from 'src/app/config';
import {AuthService} from './auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class HTTPInterceptorService implements HttpInterceptor {

  constructor(private _auth: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let headers = req.headers;
      const token = localStorage.getItem('access_token');
      headers = headers.append('Authorization', `Bearer ${token}`);
    const request = req.clone({
      headers,
      url:`${SRV_URL}${req.url}`,
    });
    return next.handle(request);
  }
}

