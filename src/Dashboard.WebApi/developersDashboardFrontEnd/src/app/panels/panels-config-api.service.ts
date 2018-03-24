import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {ErrorObservable} from 'rxjs/observable/ErrorObservable';
import {catchError, retry} from 'rxjs/operators';

@Injectable()
export class PanelsConfigApiService {

    constructor(private http : HttpClient) {}

    savePanel<T>(url : string, panelData : T) : Observable<T> {
        return this.http.post<T>(url, panelData);
    }
}
