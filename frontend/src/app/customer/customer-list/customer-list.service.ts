import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import Customer from "./customer";

@Injectable({
  providedIn: "root"
})
export class CustomerListService {
  constructor(private _http: HttpClient) {}

  public getCustomers(): Observable<Customer[]> {
    return this._http.get<Customer[]>("/customer");
  }
}
