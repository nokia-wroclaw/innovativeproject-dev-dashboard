import {HttpClientModule} from '@angular/common/http'
import {HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';

import {TestBed, inject} from '@angular/core/testing';
import {PanelsConfigApiService} from "./panels-config-api.service";

describe('PanelsConfigApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule, HttpClientTestingModule
      ],
      providers: [PanelsConfigApiService]
    });
  });

  it('should be created', inject([PanelsConfigApiService], (service : PanelsConfigApiService) => {
    expect(service).toBeTruthy();
  }));

  it('should make proper post to specified url', inject([
    HttpTestingController, PanelsConfigApiService
  ], (httpMock : HttpTestingController, service : PanelsConfigApiService) => {

    const url : string = '/api/Panel/CreateConcretePanel';

    service.savePanel(url, null).subscribe();

    const mockReq = httpMock.expectOne(url);

    expect(mockReq.cancelled).toBeFalsy();
    expect(mockReq.request.method).toEqual('POST');

    httpMock.verify();
  }))

});