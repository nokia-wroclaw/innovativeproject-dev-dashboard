import {Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import {Panel, PanelType, PanelPositionUpdateItem} from "../../panel";

@Injectable()
export class PanelApiService {

    private baseUrl : string = "/api/panel";

    private updatePositionsUrl : string = "/api/panel/positions";

    constructor(private http : HttpClient) {}

    getPanels() : Observable < Panel[] > {
        return this.http.get < Panel[] > (this.baseUrl);
    }

    updatePanelPositions(panelPositions : PanelPositionUpdateItem[]) : Observable<PanelPositionUpdateItem[]> {
        const wrappedObject : any = { 
            updatedPanelPositions: panelPositions
        };
        return this.http.post<any>(this.updatePositionsUrl, wrappedObject).map(wrapped => wrapped.updatedPanelPositions);
    }

}
