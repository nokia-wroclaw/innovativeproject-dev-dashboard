export enum PanelType {
    EmptyPanel
}

export interface PanelPosition {
    column : number;
    row : number;
}

export interface Panel {
    id : number;
    title : string;
    dynamic : boolean;
    type : PanelType;
    position : PanelPosition;
    projectId : number;

    data : any;
}