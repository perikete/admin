import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { AuthResponse } from "./auth-response";
import { removeToken } from "../core/local-storage.util";
import { Observable } from "rxjs";

@Injectable()
export class AuthService {
  constructor(private _http: HttpClient) {}

  public login(username: string, password: string): Observable<AuthResponse> {
    return this._http.post<AuthResponse>("/account/authenticate", {
      username,
      password
    });
  }  

  public logout(): void {
    removeToken();
  }
}
