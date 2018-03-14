import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import { Todo } from './todo';

@Injectable()
export class HelloWorldService {

  constructor(private http: HttpClient) { }

  private url : string = '/api/helloworld';

  getUrl(): string {
    return this.url;
  }

  getTodoList(): Observable<Todo[]> {
    return this.http.get<Todo[]>('/api/helloworld');
  }

}

