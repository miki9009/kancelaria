import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { Case } from '../_models/case';

@Injectable({
  providedIn: 'root'
})
export class CaseServiceService {

constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

baseUrl =  environment.apiUrl  + 'cases';

createCase(model: any) {
  // console.log(model);
  const token = localStorage.getItem('token');
  const decodeToken = this.jwtHelper.decodeToken(token);
  model.userID = decodeToken.nameid;
  // console.log(model);
  return this.http.post(this.baseUrl, model);
}



getCases(): Observable<Case[]> {
  // console.log(this.http.get(this.baseUrl));
  return this.http.get<Case[]>(this.baseUrl);
}

deleteCase(id: number) {

  // const options = {
  //   headers: new HttpHeaders({
  //     'Content-Type': 'application/json',
  //   }),
  // };
  
  return this.http.delete(this.baseUrl + '/' + id);

}

}



