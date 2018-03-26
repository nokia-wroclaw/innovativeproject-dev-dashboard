import {Project} from './../projects-manager/project';

export interface PanelPosition {
    column : number;
    row : number;
    width : number;
    height : number;
}

export interface Panel {
    id? : number;
    title : string;
    isDynamic? : boolean;
    discriminator : string;
    position? : PanelPosition;
    projectId : number;
}

export interface PanelPositionUpdateItem {
    panelId : number,
    position : PanelPosition
}