import {Injectable} from '@angular/core';
import {Type} from "@angular/compiler/src/core";
import {PanelType} from "../../panel";
import {RandomMemePanelComponent} from "../../../panels/random-meme-panel/random-meme-panel.component";
import {RandomMemePanelConfigComponent} from "../../../panels/random-meme-panel/random-meme-panel-config.component";
import {StaticBranchPanelComponent} from "../../../panels/static-branch-panel/static-branch-panel.component";
import {StaticBranchPanelConfigComponent} from "../../../panels/static-branch-panel/static-branch-panel-config.component";

@Injectable()
export class PanelTypeMapperService {

    map(discriminator : string) : Type {
        // TODO extract as map
        if(discriminator === 'MemePanel') {
            return RandomMemePanelComponent;
        } else if(discriminator === 'StaticBranchPanel') {
            return StaticBranchPanelComponent;
        } else {
            console.log("Error: Mapping not specified in panel-type-mapper");
        }
    }

    mapConfiguration(discriminator : string) : Type {
        // TODO extract as map
        if(discriminator === 'MemePanel') {
            return RandomMemePanelConfigComponent;
        } else if(discriminator === 'StaticBranchPanel') {
            return StaticBranchPanelConfigComponent;
        } else {
            console.log("Error: Mapping not specified in panel-type-mapper");
        }
    }

    getName(panelType : PanelType) : String {
        if(panelType == PanelType.EmptyPanel) {
            return "Empty Panel";
        } else if (panelType == PanelType.RandomMemePanel) {
            return "Random Meme Panel";
        } else {
            return "Name not specified in panel-type-mapper";
        }
    }

    isDynamic(panelType : PanelType) : boolean {
        if(panelType == PanelType.EmptyPanel) {
            return false;
        } else if (panelType == PanelType.RandomMemePanel) {
            return false;
        } else {
            console.log("Error: Mapping not specified in panel-type-mapper");
            return false;
        }
    }

}
