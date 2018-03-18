import {Injectable, ComponentFactoryResolver} from '@angular/core';

import {Project} from "./project";
import {Observable} from "rxjs/Observable";
import {ProjectsApiService} from "./api/projects-api.service";

import 'rxjs/add/operator/mergeMap';
import "rxjs/add/observable/of";

@Injectable()
export class ProjectsManagerService {

    private projectsCache : Project[];

    constructor(private projectsApi : ProjectsApiService) {}

    getProjects() : Observable < Project[] > {
        if(this.projectsCache != undefined) {
            console.log("Projects cache hit");
            return Observable.of(this.projectsCache);
        } else {
            console.log("Projects cache miss");
            return this
                .projectsApi
                .getProjects()
                .map(projectsData => {
                    this.projectsCache = projectsData;
                    return projectsData;
                });
        }
    }

    getProject(id : number) : Observable < Project > {
        return this
            .getProjects()
            .map(projects => projects.find(project => project.id == id));
    }

    updateProject(project : Project) {
        console.log("Call updateProject()");
        console.log(project);
    }

    addProject(project : Project) {
        console.log("Call addProject()");
        console.log(project);
    }
}
