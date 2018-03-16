import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticHostPanelComponent } from './static-host-panel.component';

describe('StaticHostPanelComponent', () => {
  let component: StaticHostPanelComponent;
  let fixture: ComponentFixture<StaticHostPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StaticHostPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StaticHostPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
