import {HttpClientModule} from '@angular/common/http'
import {HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';

import {TestBed, inject} from '@angular/core/testing';

import {HelloWorldService} from './hello-world.service';
import {Todo} from './todo';

describe('HelloWorldService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule, HttpClientTestingModule
      ],
      providers: [HelloWorldService]
    });
  });

  it('should be created', inject([HelloWorldService], (service : HelloWorldService) => {
    expect(service).toBeTruthy();
  }));

  it('should get todo list', inject([
    HttpTestingController, HelloWorldService
  ], (httpMock : HttpTestingController, service : HelloWorldService) => {

    const mockResponse : Todo[] = [ { id: 0, text: "Todo1" }, {id: 0, text:"Todo2"} ];

    service.getTodoList().subscribe(todoList => expect(todoList).toEqual(mockResponse));

    const mockReq = httpMock.expectOne(service.getUrl());

    expect(mockReq.cancelled).toBeFalsy();
    expect(mockReq.request.responseType).toEqual('json');
    mockReq.flush(mockResponse);

    httpMock.verify();
  }))

});
