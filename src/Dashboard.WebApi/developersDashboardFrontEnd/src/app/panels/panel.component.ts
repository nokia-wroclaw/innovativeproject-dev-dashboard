import {Panel} from "../panel-manager/panel";
import {Observable} from "rxjs/Observable";

export interface IPanelComponent < T > {
    setPanel(panel : T);
}

export interface IPanelConfigComponent<T> {
    createPanelUrl : string;
    setPanel(panel : T);

    isValid() : boolean;
    postPanel() : Observable<T>;
}