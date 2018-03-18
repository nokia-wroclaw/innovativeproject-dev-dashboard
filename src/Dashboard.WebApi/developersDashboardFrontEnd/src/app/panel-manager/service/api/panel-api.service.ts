import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import {PanelType} from './../../panel-type';
import {Panel} from "../../panel";

@Injectable()
export class PanelApiService {

    private baseUrl : string = "/api/panel";

    constructor(private http : HttpClient) {}

    getPanels() : Observable < Panel[] > {
        return this.http.get < Panel[] > (this.baseUrl);
    }

}
