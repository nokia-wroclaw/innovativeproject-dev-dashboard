import {Injectable} from '@angular/core';
import {Type} from "@angular/compiler/src/core";
import {panelTypes} from "./panel-types";
import {RandomMemePanelComponent} from "../../../panels/random-meme-panel/random-meme-panel.component";
import {RandomMemePanelConfigComponent} from "../../../panels/random-meme-panel/random-meme-panel-config.component";
import {StaticBranchPanelComponent} from "../../../panels/static-branch-panel/static-branch-panel.component";
import {StaticBranchPanelConfigComponent} from "../../../panels/static-branch-panel/static-branch-panel-config.component";
import { PanelType } from './panel-type';
import { Panel } from '../../panel';

@Injectable()
export class PanelTypeService {
    
    getPanelTypes() : PanelType[] {
        return panelTypes;
    }

    getPanelType(panel: Panel) : PanelType {
        return panelTypes.find(panelType => panelType.discriminator == panel.discriminator);
    }

}
