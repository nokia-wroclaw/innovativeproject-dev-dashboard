import { TestBed, inject } from '@angular/core/testing';

import { PanelManagerService } from './panel-manager.service';

describe('PanelManagerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PanelManagerService]
    });
  });

  it('should be created', inject([PanelManagerService], (service: PanelManagerService) => {
    expect(service).toBeTruthy();
  }));
});
