import { Component, OnInit } from "@angular/core";
import { AuthService } from "../_services/auth.service";
import { AlertifyService } from "../_services/alertify.service";
import { Router } from '@angular/router';

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {}

  login() {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
        this.alertify.success("Zalogowano pomyślnie");
      },
      error => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/cases']);
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.authService.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem("token");
    this.alertify.message("Wylogowano");
    this.router.navigate(['/home']);
  }
}
