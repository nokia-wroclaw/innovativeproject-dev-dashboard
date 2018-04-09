import { RandomMemePanelComponent } from "../../../panels/random-meme-panel/random-meme-panel.component";
import { RandomMemePanelConfigComponent } from "../../../panels/random-meme-panel/random-meme-panel-config.component";
import { StaticBranchPanelComponent } from "../../../panels/static-branch-panel/static-branch-panel.component";
import { StaticBranchPanelConfigComponent } from "../../../panels/static-branch-panel/static-branch-panel-config.component";
import {PanelBounds, PanelType} from "./panel-type";
import { LastPipelinesPanelComponent } from "../../../panels/last-pipelines-panel/last-pipelines-panel.component";
import { LastPipelinesPanelConfigComponent } from "../../../panels/last-pipelines-panel/last-pipelines-panel-config.component";

export const panelTypes : PanelType[] = [
    {
        discriminator: "MemePanel",
        name: "Random meme",
        dynamic: false,
        bounds: {
            minWidth: 1,
            minHeight: 1,
            maxWidth: 3,
            maxHeight: 3,
            defaultWidth: 1,
            defaultHeight: 1
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
            maxWidth: 4,
            maxHeight: 1,
            defaultWidth: 2,
            defaultHeight: 1
        },
        component: StaticBranchPanelComponent,
        configComponent: StaticBranchPanelConfigComponent
    }, {
        discriminator: "DynamicPipelinesPanel",
        name: "Last pipelines",
        dynamic: true,
        bounds: {
            minWidth: 2,
            minHeight: 1,
            maxWidth: 10,
            maxHeight: 10,
            defaultWidth: 2,
            defaultHeight: 2
        },
        component: LastPipelinesPanelComponent,
        configComponent: LastPipelinesPanelConfigComponent
    }
]