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
        name: "Meme",
        dynamic: false,
        bounds: {
            minWidth: 2,
            minHeight: 1,
            maxWidth: 16,
            maxHeight: 4,
            defaultWidth: 2,
            defaultHeight: 1
        },
        component: RandomMemePanelComponent,
        configComponent: RandomMemePanelConfigComponent,
        apiTypeNameSave: 'CreateMemePanel',
        apiTypeNameUpdate: 'UpdateMemePanel'
    }, {
        discriminator: "StaticBranchPanel",
        name: "Latest pipeline for static branch",
        dynamic: false,
        bounds: {
            minWidth: 3,
            minHeight: 1,
            maxWidth: 16,
            maxHeight: 1,
            defaultWidth: 3,
            defaultHeight: 1
        },
        component: StaticBranchPanelComponent,
        configComponent: StaticBranchPanelConfigComponent,
        apiTypeNameSave: 'CreateStaticBranchPanel',
        apiTypeNameUpdate: 'UpdateStaticBranchPanel'
    }, {
        discriminator: "DynamicPipelinesPanel",
        name: "Pipelines for dynamic branches",
        dynamic: true,
        bounds: {
            minWidth: 3,
            minHeight: 1,
            maxWidth: 16,
            maxHeight: 10,
            defaultWidth: 3,
            defaultHeight: 2
        },
        component: LastPipelinesPanelComponent,
        configComponent: LastPipelinesPanelConfigComponent,
        apiTypeNameSave: 'CreateDynamicPipelinePanel',
        apiTypeNameUpdate: 'UpdateDynamicPipelinePanel'
    }
]