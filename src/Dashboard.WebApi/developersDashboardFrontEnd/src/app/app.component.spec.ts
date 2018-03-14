import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { MatButtonModule, MatIconModule, MatCardModule } from '@angular/material';
import { HelloWorldService } from './hello-world/hello-world.service'
import { HttpModule }    from '@angular/http';

import { Todo } from './hello-world/todo';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

describe('AppComponent', () => {

  let helloWorldServiceStub = {
    getTodoList: function(){
      let list : Todo[] = [{id: 0, text: "Todo1"}];
      return Observable.of(list);
    }
  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
      ],
      imports: [
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        HttpModule
      ],
      providers: [
        {provide: HelloWorldService, useValue: helloWorldServiceStub}
      ]
    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
  it(`should have as title 'developers-dashboard'`, async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('developers-dashboard');
  }));
  it('should render title in a h1 tag', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Welcome to developers-dashboard!');
  }));
});
