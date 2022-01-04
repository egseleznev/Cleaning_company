import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import {IUtility} from '../utilities/utilities.service';


@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  path:string;
  constructor(private _http: HttpClient) {
  }

  public getClient():Observable<ISubscriber[]>{
    return this._http.get<ISubscriber[]>("/subscribers/");
  }

  public addutilitytoclient(data: { id:number,ids:number }): Observable<number> {
    this.path="/subscribers/addutility/"+data.id+"/"+data.ids;
    return this._http.post<any>(this.path,{ });
  }

  public deleteutilityfromclient(data: { id:number,ids:number }): Observable<number> {
    this.path="/subscribers/deleteutility/"+data.id+"/"+data.ids;
    return this._http.delete<any>(this.path);
  }
}

export interface ISubscriber
{
    fullname: string;
    passportdata: string;
    telephonenumber: string;
    balance: number;
    utilities: IGetUtility[];
}

export interface IGetUtility
{
  utility: IUtility,
  utilityid: number,
  subscriberid: number;
  duedate: number
}

