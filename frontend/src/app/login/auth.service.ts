import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthResponse } from './auth-response';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient) {

  }

  login(email: string, password: string ) {
      return this.http.post<AuthResponse>('/api/authenticate', {email, password})
        .subscribe(this.setSession);
  }

  private setSession(authResult: AuthResponse) {

      localStorage.setItem('token', authResult.token);
  }

  logout() {
      localStorage.removeItem("token");
  }


}
