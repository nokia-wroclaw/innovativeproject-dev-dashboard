import {Panel} from "../../panel-manager/panel";

export interface LastPipelinesPanel extends Panel {
    howManyLastPipelinesToRead : number;
    panelRegex : string;
}