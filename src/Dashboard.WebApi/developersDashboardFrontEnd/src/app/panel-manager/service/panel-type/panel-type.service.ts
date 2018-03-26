import {Injectable} from '@angular/core';
import {Type} from "@angular/compiler/src/core";
import {panelTypes} from "./panel-types";
import {RandomMemePanelComponent} from "../../../panels/random-meme-panel/random-meme-panel.component";
import {RandomMemePanelConfigComponent} from "../../../panels/random-meme-panel/random-meme-panel-config.component";
import {StaticBranchPanelComponent} from "../../../panels/static-branch-panel/static-branch-panel.component";
import {StaticBranchPanelConfigComponent} from "../../../panels/static-branch-panel/static-branch-panel-config.component";
import { PanelType } from './panel-type';

@Injectable()
export class PanelTypeService {

    map(discriminator : string) : Type {
        return panelTypes.find(panelType => panelType.discriminator == discriminator).component;
    }

    mapConfiguration(discriminator : string) : Type {
        return panelTypes.find(panelType => panelType.discriminator == discriminator).configComponent;
    }

    getPanelTypes() : PanelType[] {
        return panelTypes;
    }

}
