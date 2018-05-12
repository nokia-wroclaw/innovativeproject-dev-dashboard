import {Injectable} from '@angular/core';
import {HttpClient, HttpResponse, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import {Pipeline} from '../../../../projects-manager/project';
import {BehaviorSubject, Subscription, Subject} from 'rxjs';

@Injectable()
export class PipelineService {

    private baseUrl : string = "/api/DashboardData/PipelinesForPanel";

    constructor(private http : HttpClient) {}

    getPipelines(panelId : number) : Observable < Pipeline[] > {
        const options = panelId ? { params: new HttpParams().set('panelID', panelId.toString()) } : {};

        return this.http.get < Pipeline[] > (this.baseUrl, options);
    }

}