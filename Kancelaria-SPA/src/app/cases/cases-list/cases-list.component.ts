import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CaseServiceService } from 'src/app/_services/case-service.service';
import { Case } from 'src/app/_models/case';

@Component({
  selector: 'app-cases-list',
  templateUrl: './cases-list.component.html',
  styleUrls: ['./cases-list.component.css']
})

  // tslint:disable-next-line: member-ordering


export class CasesListComponent implements OnInit {


  createNewCase: boolean;
  delete: boolean;
  count: number;
  model: any = {};
  cases: Case[];
  caseIndex: number;

  constructor(private alertify: AlertifyService, private caseService: CaseServiceService) { }


  ngOnInit() {
    this.caseService.getCases().subscribe((data) => {

      this.cases = data;

      this.cases.forEach(element => {
        element.dateAdded = new Date(element.dateAdded);
      });

      // console.log(this.cases);
    });
  }
  showCreatePopup() {
    return this.createNewCase = !this.createNewCase;
  }

  createCase() {
    console.log(this.model);
    this.caseService.createCase(this.model).subscribe(() => {
    this.alertify.success('Sprawa stworzona');
    this.showCreatePopup();
    this.ngOnInit();
    },
    error => {
      this.alertify.error('Nie udało się stworzyć sprawy');
    });
  }

  refresh(): void {
    window.location.reload();
}

  deleteCase(id: number) {
    this.caseService.deleteCase(id).subscribe(() => {
      this.alertify.success('Sprawa usunięta');
      this.ngOnInit();
      },
      error => {
        this.alertify.error('Nie udało się usunąć sprawy');
      });
  }

  deleteCaseCurrent() {
    this.deleteCase(this.caseIndex);
    this.delete = false;
  }

  showDeletePopup(id: number) {
    console.log('caseID: ' + id);
    this.caseIndex = id;
    this.delete = !this.delete;
  }

  }

  
