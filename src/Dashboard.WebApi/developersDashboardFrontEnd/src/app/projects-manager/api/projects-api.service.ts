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
export class ProjectsApiService implements OnDestroy {

    private baseUrl : string = "/api/project";

    private poolingInterval : number = 10000;
    private projects : BehaviorSubject < Project[] > = new BehaviorSubject < Project[] > ([]);
    private projectsPulling : Subscription;

    // Writting to this subject causes an immidiate fetch of projects from backend
    // API.
    private asyncPull : Subject < boolean > = new Subject();

    constructor(private http : HttpClient, private notificationService : NotificationService) {
        this.projectsPulling = Observable.merge(Observable.interval(this.poolingInterval).startWith(0), this.asyncPull.asObservable()).switchMap(() => this.http.get < Project[] > (this.baseUrl)).subscribe(projects => this.projects.next(projects), error => this.notificationService.addNotification("Couldn't update projects: " + error.statusText, NotificationType.Failure));
    }

    ngOnDestroy() : void {
        this
            .projectsPulling
            .unsubscribe();
    }

    getProjects() : Observable < Project[] > {
        return this.projects;
    }

    getProject(id : number) : Observable < Project > {
        return this
            .projects
            .map(projects => projects.find(project => project.id == id));
    }

    addProject(project : Project) : Observable < Project > {
        return this.http.post < Project > (this.baseUrl, project).flatMap(project => {
            this
                .asyncPull
                .next(true);
            return Observable.of(project);
        });
    }

    private url : string = "api/DashboardData/SupportedProviders";

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
