import {PanelType} from './panel-type';
import {PanelPosition} from "./panel-position";

export interface Panel {
    id : number;
    title : string;
    dynamic : boolean;
    type : PanelType;
    position : PanelPosition;
    projectId : number;

    data : any;
}