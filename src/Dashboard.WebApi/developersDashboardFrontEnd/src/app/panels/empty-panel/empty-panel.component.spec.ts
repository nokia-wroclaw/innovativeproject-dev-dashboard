import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmptyPanelComponent } from './empty-panel.component';

describe('EmptyPanelComponent', () => {
  let component: EmptyPanelComponent;
  let fixture: ComponentFixture<EmptyPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmptyPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmptyPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
