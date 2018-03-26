import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';;

import {Project, Pipeline} from "./../project";

@Injectable()
export class ProjectsApiService {

    private baseUrl : string = "/api/project";

    constructor(private http : HttpClient) {}

    getProjects() : Observable < Project[] > {
        return this.http.get < Project[] > (this.baseUrl);
    }

    // it is called individualy from every panel. could be opt with ReplaySubject TODO
    getProject(id : number) : Observable < Project > {
        return this.http.get < Project > (this.baseUrl + "/" + id);
    }

    addProject(project : Project) : Observable < Project > {
        return this.http.post < Project > (this.baseUrl, project);
    }

}