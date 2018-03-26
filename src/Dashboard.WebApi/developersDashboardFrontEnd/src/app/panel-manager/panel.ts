import {Project} from './../projects-manager/project';
import { PanelType } from './service/panel-type/panel-type';

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

    panelType?: PanelType;
}

export interface PanelPositionUpdateItem {
    panelId : number,
    position : PanelPosition
}