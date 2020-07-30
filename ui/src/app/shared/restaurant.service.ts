import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Filters, Restaurant} from './restaurant';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class RestaurantService {

  // Base url
  baseurl = 'https://localhost:5001/api/v1.0/restaurantlisting';

  constructor(private http: HttpClient) { }

  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  }

  // GET
  GetAllRestaurants(filters?: Filters): Observable<any> {
    let url = this.baseurl;
    if (filters) {
      url = `${url}?${Object.keys(filters).map(key => key + '=' + filters[key]).join('&')}`
    }
    return this.http.get<any[]>(url , this.httpOptions )
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      )
  }

  // POST
  CreateRestaurant(data): Observable<any> {
    return this.http.post<Restaurant>(this.baseurl , JSON.stringify(data), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      )
  }

  // PUT
  UpdateRestaurant(data): Observable<any> {
    return this.http.put<Restaurant>(this.baseurl + '/' + data.id, JSON.stringify(data), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      )
  }

  // DELETE
  DeleteRestaurant(id): Observable<any> {
    return this.http.delete<Restaurant>(this.baseurl + '/' + id, this.httpOptions)
      .pipe(
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
