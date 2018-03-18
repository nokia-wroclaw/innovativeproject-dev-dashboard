import {Injectable, ComponentFactoryResolver} from '@angular/core';
import {HostDirective} from './../../panel-host/host.directive';

import {PanelType} from './../panel-type';
import {Panel} from "../panel";
import {Observable} from "rxjs/Observable";
import {PanelApiService} from "./api/panel-api.service";
import {PanelTypeMapperService} from "./panel-type-mapper/panel-type-mapper.service";

@Injectable()
export class PanelManagerService {

  // private panelTypeMap : any = {   'empty-panel': EmptyPanelComponent };
  // private configTypeMap : any = {   'empty-panel': EmptyPanelConfigComponent };

  // TODO create service which maps PanelType to referenced component classes and
  // configuration component classes

  private panelsCache : Panel[];

  constructor(private panelApi : PanelApiService, private componentFactoryResolver : ComponentFactoryResolver, private panelTypeMapper : PanelTypeMapperService) {}

  getPanelData() : Observable < Panel[] > {

    // TODO save loaded data in panelsCache and return it upon next calls, remember
    // to keep it up to date after any changes

    return this
      .panelApi
      .getPanels();
  }

  /**
   * Adds panel both to cached panels object and persists it to backend.
   *
   * @param panel panel to add
   */
  addPanel(panel : Panel) {}

  injectPanelComponent(host : HostDirective, panelId : number) {
    console.log("injectPanelComponent");

    // refactor to use panelsCache if its there
    this
      .getPanelData()
      .subscribe(panelsData => {

        let panelToLoad = panelsData.find(panel => panel.id == panelId);
        let viewContainerRef = host.viewContainerRef;
        viewContainerRef.clear();

        let panelComponentType = this
          .panelTypeMapper
          .map(panelToLoad.type);

        let componentFactory = this
          .componentFactoryResolver
          .resolveComponentFactory(panelComponentType);

        let componentRef = viewContainerRef.createComponent(componentFactory);

      });

  }

  injectPanelConfiguration(host : HostDirective, panelId : number) {
    console.log("injectPanelConfiguration");
  }

  updateConfiguration(panelId : number, configuration : any) {
    console.log("updateConfiguration");
  }

}
