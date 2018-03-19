import {Injectable} from '@angular/core';
import {Type} from "@angular/compiler/src/core";
import {PanelType} from "../../panel";
import {EmptyPanelComponent} from "../../../panels/empty-panel/empty-panel.component";

@Injectable()
export class PanelTypeMapperService {

    map(panelType : PanelType) : Type {
        // TODO extract as map
        if(panelType == PanelType.EmptyPanel) {
            return EmptyPanelComponent;
        } else {
            console.log("Error: Mapping not specified in panel-type-mapper");
        }
    }

    mapConfiguration(panelType : PanelType) : Type {
        // TODO extract as map
        if(panelType == PanelType.EmptyPanel) {
            // return EmptyPanelConfigurationComponent;
            return null;
        } else {
            console.log("Error: Mapping not specified in panel-type-mapper");
        }
    }

    getName(panelType : PanelType) : String {
        if(panelType == PanelType.EmptyPanel) {
            return "Empty Panel";
        } else {
            return "Name not specified in panel-type-mapper";
        }
    }

}
