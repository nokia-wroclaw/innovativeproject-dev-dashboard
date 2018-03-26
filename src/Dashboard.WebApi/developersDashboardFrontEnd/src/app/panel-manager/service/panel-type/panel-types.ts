import { RandomMemePanelComponent } from "../../../panels/random-meme-panel/random-meme-panel.component";
import { RandomMemePanelConfigComponent } from "../../../panels/random-meme-panel/random-meme-panel-config.component";
import { StaticBranchPanelComponent } from "../../../panels/static-branch-panel/static-branch-panel.component";
import { StaticBranchPanelConfigComponent } from "../../../panels/static-branch-panel/static-branch-panel-config.component";
import {PanelBounds, PanelType} from "./panel-type";

export const panelTypes : PanelType[] = [
    {
        discriminator: "MemePanel",
        name: "Random meme",
        dynamic: false,
        bounds: {
            minWidth: 2,
            minHeight: 2,
            maxWidth: 3,
            maxHeight: 3
        },
        component: RandomMemePanelComponent,
        configComponent: RandomMemePanelConfigComponent
    }, {
        discriminator: "StaticBranchPanel",
        name: "Static branch pipelines",
        dynamic: false,
        bounds: {
            minWidth: 2,
            minHeight: 1,
            maxWidth: 2,
            maxHeight: 1
        },
        component: StaticBranchPanelComponent,
        configComponent: StaticBranchPanelConfigComponent
    }
]