import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NavComponent } from '../nav/nav.component';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  values: any;
  
  @Input() navComponent: NavComponent;

  constructor(private http: HttpClient, private authService: AuthService) { }

  ngOnInit() {
 
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  registerToggle(){
    this.registerMode = true;
  }



  cancelRegisterMode(registerMode: boolean){
    this.registerMode = registerMode;
  }

}
