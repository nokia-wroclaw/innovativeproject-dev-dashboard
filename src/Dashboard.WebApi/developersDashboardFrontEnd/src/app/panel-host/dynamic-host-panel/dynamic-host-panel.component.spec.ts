import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicHostPanelComponent } from './dynamic-host-panel.component';

describe('DynamicHostPanelComponent', () => {
  let component: DynamicHostPanelComponent;
  let fixture: ComponentFixture<DynamicHostPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DynamicHostPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicHostPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
