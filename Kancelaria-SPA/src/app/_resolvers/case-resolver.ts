import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Case } from '../_models/case';
import { CaseServiceService } from '../_services/case-service.service';

@Injectable()

export class MemberListResolver implements Resolve<Case[]> {
    constructor(private userService: CaseServiceService, private router: Router, private alertify: AlertifyService){}


    resolve(route: ActivatedRouteSnapshot) : Observable<Case[]>{
        return this.userService.getCases().pipe(
            catchError(error => {
                this.alertify.error('Problem retriving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}