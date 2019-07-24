/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CaseServiceService } from './case-service.service';

describe('Service: CaseService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CaseServiceService]
    });
  });

  it('should ...', inject([CaseServiceService], (service: CaseServiceService) => {
    expect(service).toBeTruthy();
  }));
});
