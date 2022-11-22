import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { ResultModel } from "../models/resultModel";
import { environment } from "../../environments/environment";
import {CommandModel} from "../models/commandModel";

@Injectable({
  providedIn: 'root'
})
export class QueryService {

  constructor(private httpClient: HttpClient) { }

  public ConvertSelectQuery(command: CommandModel): Observable<ResultModel> {
    return this.httpClient.put<ResultModel>(`${environment.baseUrl}/select`, command)
      .pipe(map((data) => {
        return data
      }));
  }

  public ConvertOrderByQuery(command: CommandModel): Observable<ResultModel> {
    return this.httpClient.put<ResultModel>(`${environment.baseUrl}/orderBy`, command)
      .pipe(map((data) => {
        return data
      }));
  }

  public ConvertGroupByQuery(command: CommandModel): Observable<ResultModel> {
    return this.httpClient.put<ResultModel>(`${environment.baseUrl}/groupBy`, command)
      .pipe(map((data) => {
        return data
      }));
  }
}
