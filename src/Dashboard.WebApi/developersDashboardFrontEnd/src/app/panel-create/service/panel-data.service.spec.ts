import { TestBed, inject } from '@angular/core/testing';

import { PanelDataService } from './panel-data.service';

describe('PanelDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PanelDataService]
    });
  });

  it('should be created', inject([PanelDataService], (service: PanelDataService) => {
    expect(service).toBeTruthy();
  }));
});
