import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { RandomMemePanel } from './random-meme-panel';

@Injectable()
export class RandomMemeService {

    private baseUrl : string = "/api/panel";

    constructor(private http : HttpClient) {}

    refreshPanel(id : number) : Observable < RandomMemePanel > {
        return this.http.get < RandomMemePanel > (this.baseUrl + "/" + id);
    }

}
