import { Injectable } from '@angular/core';
import {HttpClientModule, HttpClient, HttpResponse} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Md5} from 'ts-md5';
import {catchError, map} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {

  path:string;
  newbuffer:IUtility;
  constructor(private _http: HttpClient) {
  }

  public addutility(data: { name: string, price:number, isfavourite: boolean }): Observable<number> {
    return this._http.post<any>("/utilities/", data);
  }

  public pututility(data: {id:number, name: string, price:number,isfavourite: boolean }): Observable<number> {
    this.path="/utilities/"+data.id;
    this.newbuffer.isfavourite=data.isfavourite;
    this.newbuffer.name=data.name;
    this.newbuffer.price=data.price;
    return this._http.put<any>(this.path, this.newbuffer);
  }

  public deleteutility(data:{id:number}):Observable<number>{
    this.path="/utilities/"+data.id;
    return this._http.delete<any>(this.path);
  }

  public getUtility():Observable<IUtility[]>{
    return this._http.get<IUtility[]>("/utilities/");
  }

  public getUtilityStat():Observable<number[]>{
    return this._http.get<number[]>("/subscribers/stat");
  }
}

export interface IUtility
{
  price:number;
  name:string;
  isfavourite:boolean;
}


