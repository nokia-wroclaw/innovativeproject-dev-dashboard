import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PanelConfigurationComponent } from './panel-configuration.component';

describe('PanelConfigurationComponent', () => {
  let component: PanelConfigurationComponent;
  let fixture: ComponentFixture<PanelConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PanelConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PanelConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
