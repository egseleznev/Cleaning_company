import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaderResponse, HttpHeaders, HttpResponse} from '@angular/common/http';
import { SRV_URL } from 'src/app/config';
import {Md5} from 'ts-md5/dist/md5';
import { Observable, of } from 'rxjs';
import { map,catchError } from 'rxjs/operators';
import {IUtility} from '../utilities/utilities.service';
import {ISubscriber} from '../clients/clients.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _http: HttpClient) { }
  salt = 252341245e6345124;
  public login(info: { login: string, password: string }):Observable<number> {

    info.password = Md5.hashStr(info.password+this.salt) as string
    return this._http.post<any>("/account/token/", info, { observe: 'response'})
      .pipe(
        map(res=>
        {
          if (res.status == 200) {
           localStorage.setItem('access_token', res.body.access_token);
            return res.status;
          }
        }),
        catchError(error => {
            return of((error as HttpResponse<any>).status);
          }
        )
      )
  }

  public getRole(): Observable<any>{
     return this._http.get<any>('/subscribers/getrole',{observe:'response'}).pipe(
       map( res=>
       {
         if(res.status==201){
           return 'admin';
         }
       })
     );


  }

  public sendTestRequest(){
    this._http.get('/values/getrole').subscribe(res=> alert(res));
  }
}

