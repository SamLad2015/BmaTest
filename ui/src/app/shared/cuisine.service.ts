import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Cuisine, Restaurant} from './restaurant';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class CuisineService {
  // Base url
  baseurl = 'https://localhost:5001/api/v1.0/cuisine';

  constructor(private http: HttpClient) { }

  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  }

  // GET
  GetAllCuisines(): Observable<Cuisine[]> {
    return this.http.get<Cuisine[]>(this.baseurl , this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      )
  }

  // Error handling
  errorHandler(error) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
