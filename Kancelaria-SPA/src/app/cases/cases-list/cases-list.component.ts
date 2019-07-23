import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CaseServiceService } from 'src/app/_services/case-service.service';
import { Case } from 'src/app/_models/case';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cases-list',
  templateUrl: './cases-list.component.html',
  styleUrls: ['./cases-list.component.css']
})

  // tslint:disable-next-line: member-ordering


export class CasesListComponent implements OnInit {


  createNewCase: boolean;
  count: number;
  model: any = {};
  cases: Case[];

  constructor(private alertify: AlertifyService, private caseService: CaseServiceService, private route: ActivatedRoute) { }


  ngOnInit() {
    this.caseService.getCases().subscribe((data) => {
      console.log(data);
      this.cases = data;
      console.log(this.cases);
    });
  }
  showCreatePopup() {
    return this.createNewCase = !this.createNewCase;
  }

  register() {
    console.log(this.model);
    this.caseService.createCase(this.model).subscribe(() => {
    this.alertify.success('Sprawa stworzona');
    },
    error => {
      this.alertify.error('Nie udało się stworzyć sprawy');
    });
  }

  }

  
