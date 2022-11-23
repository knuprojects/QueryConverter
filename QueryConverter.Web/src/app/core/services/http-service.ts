import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { catchError, map, Observable, throwError} from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommandModel } from "../models/command-model";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpClient: HttpClient) { }

  public ConvertQuery(command: CommandModel, url: string): Observable<any> {
    return this.httpClient.put<any>(`${environment.baseUrl}/${url}`, command)
      .pipe(
        map((data) => {
          return data
        }),
        catchError(this.handleError)
      );
  }

  handleError(error : any) {

    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = "Query is invalid, try again!";
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
