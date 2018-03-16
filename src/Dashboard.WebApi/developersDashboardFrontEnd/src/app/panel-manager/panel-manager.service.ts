import {Injectable} from '@angular/core';
import {HostDirective} from './../panel-host/host.directive';

import {PanelType} from './panel-type';

@Injectable()
export class PanelManagerService {

  // private panelTypeMap : any = {   'empty-panel': EmptyPanelComponent };
  // private configTypeMap : any = {   'empty-panel': EmptyPanelConfigComponent };

  private panelsData : any;

  constructor() {
    // TODO load panelsData from backendAPI temp mock
    this.panelsData = {}
  }

  getPanelPositionData() : any {
    // TODO extract interface
    let panelPositionDataMock: any = {
      column1: [
        {
          id: 0,
          title: "Title 0",
          type: PanelType.Static
        }, {
          id: 2,
          title: "Title 2",
          type: PanelType.Dynamic
        }
      ],
      column2: [
        {
          id: 1,
          title: "Title 1",
          type: PanelType.Static
        }
      ],
      column3: []
    };

    return panelPositionDataMock;
  }

  injectPanelComponent(host : HostDirective, panelId : number) {
    console.log("injectPanelComponent");
  }

  injectPanelConfiguration(host : HostDirective, panelId : number) {
    console.log("injectPanelConfiguration");
  }

  updateConfiguration(panelId : number, configuration : any) {
    console.log("updateConfiguration");
  }

}
