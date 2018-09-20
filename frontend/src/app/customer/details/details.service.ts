import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Customer from '../customer-list/customer';

@Injectable({
  providedIn: 'root'
})
export class DetailsService {
  constructor(private _http: HttpClient) {}

  public getCustomer(id: number): Observable<Customer> {
    return this._http.get<Customer>(`/customer/${id}`);
  }
}
