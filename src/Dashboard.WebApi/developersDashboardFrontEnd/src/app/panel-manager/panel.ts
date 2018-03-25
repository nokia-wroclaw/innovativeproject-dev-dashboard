import {Project} from './../projects-manager/project';

export enum PanelType {
    EmptyPanel,
    RandomMemePanel
}

/**
 * Hopefully temporary workaround, it is not so easy to iterate enum values in Typescript.
 */
export namespace PanelType {
    export function getValues() : PanelType[] {
        return [PanelType.EmptyPanel, PanelType.RandomMemePanel];
    }
}

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