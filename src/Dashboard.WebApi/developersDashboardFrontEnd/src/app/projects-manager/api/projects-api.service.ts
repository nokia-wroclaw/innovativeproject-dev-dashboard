import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';

import {Project, Pipeline} from "./../project";
import {BehaviorSubject} from 'rxjs/BehaviorSubject';

import 'rxjs/Rx';
import {Subscription} from 'rxjs/Rx';
import {Subject} from 'rxjs/Subject';
import {NotificationService, NotificationType} from '../../snackbar/notification.service';

@Injectable()
export class ProjectsApiService {

    private baseUrl : string = "/api/project";

    constructor(private http: HttpClient) {
        
    }

    getProjects(): Observable<Project[]> {
        return this.http.get<Project[]>(this.baseUrl);
    }

    getProject(id: number): Observable<Project> {
        return this.http.get<Project>(this.baseUrl + '/' + id);
    }

    saveOrUpdate (update : boolean, projectData: Project) : Observable < Project > {
        if(update) {
            return this.updateProject(projectData);
        } else {
            return this.addProject(projectData);
        }
    }
    addProject(projectData: Project): Observable<Project> {
        return this.http.post<Project>(this.baseUrl, projectData).flatMap(project => {
            return Observable.of(project);
        });
    }

    updateProject(projectData: Project): Observable<Project> {
        return this.http.put<Project>(this.baseUrl + '/' + projectData.id, projectData);
    }

    deleteProject(project : Project) : Observable<any> {
        return this.http.delete < any > (this.baseUrl + '/' + project.id);
    }

    private url: string = "api/DashboardData/SupportedProviders";

    getSupportedProvidersForProjects() : Observable < string[] > {
        return this.http.get < string[] > (this.url);
    }

    getMatchingBranches(projectId : number, searchValue : string) : Observable < string[] > {

        const options = projectId && searchValue
            ? {
                params: new HttpParams()
                    .set('projectId', projectId.toString())
                    .set('searchValue', searchValue)
            }
            : {};

        return this.http.get < string[] > ('/api/DashboardData/SearchForBranch', options);
    }
}
