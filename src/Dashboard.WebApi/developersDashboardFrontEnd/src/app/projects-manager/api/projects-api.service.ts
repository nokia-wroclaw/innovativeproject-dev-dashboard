import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { Project, Pipeline } from "./../project";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

import 'rxjs/Rx';
import { Subscription } from 'rxjs/Rx';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ProjectsApiService implements OnDestroy {

    private baseUrl: string = "/api/project";

    private poolingInterval: number = 10000;

    private projects: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);
    private projectsPulling: Subscription;

    // Writting to this subject causes an immidiate fetch of projects from backend API.
    private asyncPull: Subject<boolean> = new Subject();

    constructor(private http: HttpClient) {
        // temp interval poking backend to update data TODO get rid of in the future
        setInterval(() => {
            http.post("/api/DashboardData/UpdateCiDataForProject/1", null).subscribe(() => console.log('temp pooling'));
        }, 30000)

        this.projectsPulling = Observable.merge(
            Observable.interval(this.poolingInterval).startWith(0),
            this.asyncPull.asObservable()).switchMap(() => this.http.get<Project[]>(this.baseUrl))
            .subscribe(projects => this.projects.next(projects));
    }

    ngOnDestroy(): void {
        this.projectsPulling.unsubscribe();
    }

    getProjects(): Observable<Project[]> {
        return this.projects;
    }

    getProject(id: number): Observable<Project> {
        return this.projects.map(projects => projects.find(project => project.id == id));
    }

    addProject(project: Project): Observable<Project> {
        return this.http.post<Project>(this.baseUrl, project).flatMap(project => {
            this.asyncPull.next(true);
            return Observable.of(project);
        });
    }
}