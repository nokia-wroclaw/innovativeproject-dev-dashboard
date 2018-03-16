import {Injectable} from '@angular/core';
import {HostDirective} from './../../panel-host/host.directive';

import {PanelType} from './../panel-type';
import {Panel} from "../panel";

@Injectable()
export class PanelManagerService {

  // private panelTypeMap : any = {   'empty-panel': EmptyPanelComponent };
  // private configTypeMap : any = {   'empty-panel': EmptyPanelConfigComponent };

  // TODO create service which maps PanelType to referenced component classes and
  // configuration component classes

  private panelsData : any;

  constructor() {
    // TODO load panelsData from backendAPI temp mock
    this.panelsData = {}
  }

  getPanelData() : Panel[] {
    let mockPanelData : Panel[] = [
      {
        id: 0,
        title: "Mock panel 0",
        dynamic: false,
        type: PanelType.EmptyPanel,
        position: {
          column: 0,
          row: 0
        },
        projectId: 0,
        data: null
      }, {
        id: 1,
        title: "Mock panel 1",
        dynamic: false,
        type: PanelType.EmptyPanel,
        position: {
          column: 0,
          row: 1
        },
        projectId: 0,
        data: null
      }, {
        id: 2,
        title: "Mock panel 2",
        dynamic: false,
        type: PanelType.EmptyPanel,
        position: {
          column: 1,
          row: 0
        },
        projectId: 0,
        data: null
      }, {
        id: 3,
        title: "Mock panel 3",
        dynamic: true,
        type: PanelType.EmptyPanel,
        position: {
          column: 2,
          row: 0
        },
        projectId: 0,
        data: null
      }
    ];

    return mockPanelData;
  }

  injectPanelComponent(host : HostDirective, panelId : number) {
    console.log("injectPanelComponent");

    //  let viewContainerRef = host.viewContainerRef;   viewContainerRef.clear();
    // let componentFactory =
    // this.componentFactoryResolver.resolveComponentFactory(EmptyPanelComponent);
    // let componentRef = viewContainerRef.createComponent(componentFactory);
  }

  injectPanelConfiguration(host : HostDirective, panelId : number) {
    console.log("injectPanelConfiguration");
  }

  updateConfiguration(panelId : number, configuration : any) {
    console.log("updateConfiguration");
  }

}
