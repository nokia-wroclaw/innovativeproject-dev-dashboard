import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import {Project, Pipeline} from "./../project";

@Injectable()
export class ProjectsApiService {

    private baseUrl : string = "/api/project";

    constructor(private http : HttpClient) {}

    getProjects() : Observable < Project[] > {
        return this.http.get < Project[] > (this.baseUrl);
    }

}
