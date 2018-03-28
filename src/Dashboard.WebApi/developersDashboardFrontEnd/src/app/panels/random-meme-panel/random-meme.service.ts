import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class RandomMemeService {

    private baseUrl : string = "url/to/imgur/";

    constructor(private http : HttpClient) {}

    sayHelloWorld() {
        console.log("Hello world form the Random Meme Service!");
    }

    getRandomMeme() : Observable < Response > {
        // return this.http.get < Response > (this.baseUrl);
        return null;
    }

}
