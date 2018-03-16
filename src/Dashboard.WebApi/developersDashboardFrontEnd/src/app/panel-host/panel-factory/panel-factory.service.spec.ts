import { TestBed, inject } from '@angular/core/testing';

import { PanelFactoryService } from './panel-factory.service';

describe('PanelFactoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PanelFactoryService]
    });
  });

  it('should be created', inject([PanelFactoryService], (service: PanelFactoryService) => {
    expect(service).toBeTruthy();
  }));
});
