import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PanelProjectsComponent } from './panel-projects.component';

describe('PanelProjectsComponent', () => {
  let component: PanelProjectsComponent;
  let fixture: ComponentFixture<PanelProjectsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PanelProjectsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PanelProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
