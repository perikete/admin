import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthResponse } from "./auth-response";
import { setToken, removeToken } from "../core/local-storage.util";
import { Subscription, Observable } from "rxjs";

@Injectable()
export class AuthService {
  constructor(private _http: HttpClient) {}

  public login(username: string, password: string): Observable<AuthResponse> {
    return this._http.post<AuthResponse>("/api/account/authenticate", {
      username,
      password
    });
  }  

  public logout(): void {
    removeToken();
  }
}
