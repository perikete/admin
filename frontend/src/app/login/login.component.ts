import { Component, OnInit } from "@angular/core";
import { AuthService } from "./auth.service";
import { setToken } from "../core/local-storage.util";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  public username: string;
  public password: string;
  public errors: string;

  constructor(private _authService: AuthService, private _router: Router) {}

  ngOnInit() {}

  public login() {
    this.errors = "";

    if (this.username && this.password) {
      this._authService.login(this.username, this.password).subscribe(
        res => {
          setToken(res.token);

          this._router.navigate(["/home"]);
        },
        error => {          
          this.errors = Object.values(error.error).join(',')
        }
      );
    } else {
      this.errors = "Username/Password are required.";
    }
  }
}
