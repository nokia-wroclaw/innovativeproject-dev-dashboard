import {Injectable} from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import {Panel, PanelPositionUpdateItem} from "../../panel";

@Injectable()
export class PanelApiService {

    private baseUrl : string = "/api/panel";

    private updatePositionsUrl : string = "/api/panel/positions";

    constructor(private http : HttpClient) {}

    getPanels() : Observable < Panel[] > {
        return this.http.get < Panel[] > (this.baseUrl);
    }

    updatePanelPositions(panelPositions : PanelPositionUpdateItem[]) : Observable < PanelPositionUpdateItem[] > {
        const wrappedObject: any = {
            updatedPanelPositions: panelPositions
        };
        return this.http.post < any > (this.updatePositionsUrl, wrappedObject)
            .filter(result => result != undefined)
            .map(wrapped => wrapped.updatedPanelPositions);
    }

    saveOrUpdate < T extends Panel > (update : boolean, panelData : T) : Observable < T > {
        if(update) {
            return this.updatePanel(panelData);
        } else {
            return this.savePanel(panelData);
        }
    }

    savePanel < T extends Panel > (panelData : T) : Observable < T > {
        panelData.typeName = panelData.panelType.apiTypeNameSave;

        return this.http.post < T > (this.baseUrl, panelData);
    }

    updatePanel < T extends Panel > (panelData : T) : Observable < T > {
        panelData.typeName = panelData.panelType.apiTypeNameUpdate;

        return this.http.put < T > (this.baseUrl + '/' + panelData.id, panelData);
    }

    deletePanel(panel : Panel) : Observable<any> {
        return this.http.delete < any > (this.baseUrl + '/' + panel.id);
    }
}
