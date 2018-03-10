import { Component } from '@angular/core';
import { HelloWorldService } from './hello-world/hello-world.service'

import { Todo } from './hello-world/todo';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'developers-dashboard';

  private todoList : Todo[]; 

  constructor(private helloWorldService: HelloWorldService) {
    helloWorldService.getTodoList().subscribe(todoList => this.todoList = todoList);
  }

}
