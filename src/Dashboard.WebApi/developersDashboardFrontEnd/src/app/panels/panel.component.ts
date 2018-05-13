import {Panel} from "../panel-manager/panel";
import {Observable} from "rxjs/Observable";

/**
 * Interface that must be implemented by component that represent concrete panel - both dynamic and static.
 */
export interface IPanelComponent < T > {
    setPanel(panel : T);
}

/**
 * Interface that must be implemented by component that represent concrete configuration panel - both for dynamic and static panels.
 */
export interface IPanelConfigComponent<T> {
    setPanel(panel : T);

    isValid() : boolean;
    postPanel(edit : boolean) : Observable<T>;
}