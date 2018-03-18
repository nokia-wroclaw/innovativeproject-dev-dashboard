import {Injectable} from '@angular/core';
import {Type} from "@angular/compiler/src/core";
import {PanelType} from "../../panel-type";
import {EmptyPanelComponent} from "../../../panels/empty-panel/empty-panel.component";

@Injectable()
export class PanelTypeMapperService {

    map(panelType : PanelType) : Type {
        // TODO extract as map
        if(panelType == PanelType.EmptyPanel) {
            return EmptyPanelComponent;
        }
    }

}
