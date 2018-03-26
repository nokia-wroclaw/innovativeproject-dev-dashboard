import { Type } from "@angular/core";

export interface PanelBounds {
    minWidth: number;
    maxWidth: number;
    minHeight: number;
    maxHeight: number;
    defaultWidth: number;
    defaultHeight: number;
    // make it compatible with gridster options
}

export interface PanelType {
    discriminator: string;
    name: string;
    dynamic: boolean;
    bounds: PanelBounds;
    component: Type<any>;
    configComponent: Type<any>;
}