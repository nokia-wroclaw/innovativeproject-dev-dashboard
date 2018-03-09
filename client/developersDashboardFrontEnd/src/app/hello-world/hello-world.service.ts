import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import { Todo } from './todo';
import { TodoResponse } from './todo-response'

@Injectable()
export class HelloWorldService {

  constructor(private http: HttpClient) { }

  getTodoList() : Observable<Todo[]> {
    return this.http.get<TodoResponse>('/api/helloworld').map(response => response.toDoItems);
  }
}

