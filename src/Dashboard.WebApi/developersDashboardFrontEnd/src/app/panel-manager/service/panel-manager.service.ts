import {Injectable, ComponentFactoryResolver} from '@angular/core';
import {HostDirective} from './../../panel-host/host.directive';

import {Panel} from "../panel";
import {Observable} from "rxjs/Observable";
import {PanelApiService} from "./api/panel-api.service";
import {PanelTypeMapperService} from "./panel-type-mapper/panel-type-mapper.service";

import 'rxjs/add/operator/mergeMap';
import "rxjs/add/observable/of";
import {IPanelComponent, IPanelConfigComponent} from "../../panels/panel.component";

@Injectable()
export class PanelManagerService {

  private panelsCache : Panel[];

  constructor(private panelApi : PanelApiService, private componentFactoryResolver : ComponentFactoryResolver, private panelTypeMapper : PanelTypeMapperService) {}

  getPanels() : Observable < Panel[] > {
    if(this.panelsCache != undefined) {
      console.log("Panels cache hit");
      return Observable.of(this.panelsCache);
    } else {
      console.log("Panels cache miss");
      return this
        .panelApi
        .getPanels()
        .map(panelsData => {
          this.panelsCache = panelsData;
          return panelsData;
        });
    }
  }
  getPanel(id : number) : Observable<Panel> {
    return this.getPanels().map(panels => panels.find(panel => panel.id == id));
  } 

  updatePanels() {
     return this
        .panelApi
        .getPanels()
        .subscribe(panelsData => 
          this.panelsCache = panelsData
        );
  }

  updatePanel(id : number, panel : Panel) {
    console.log("Call updatePanel()");
    console.log(panel);

    // 1. Post panel through panelApiService
    // 2. If succeded - update object in cache
    // 3. Cache Return some error otherwise
    // - Return promise with success/failure information
  }

  /**
   * Adds panel both to cached panels object and persists it to backend.
   *
   * @param panel panel to add
   */
  addPanel(panel : Panel) {
    console.log("Call addPanel()");
    console.log(panel);

    // 1. Post panel through panelApiService
    // 2. If succeded - add object to panels
    // 3. Cache Return some error otherwise
    // - Return promise with success/failure information
  }

  injectPanelComponent(host : HostDirective, panel : Panel) {
    this
      .getPanels()
      .subscribe(panelsData => {

        const viewContainerRef = host.viewContainerRef;
        viewContainerRef.clear();

        const panelComponentType = this
          .panelTypeMapper
          .map(panel.discriminator);

        const componentFactory = this
          .componentFactoryResolver
          .resolveComponentFactory(panelComponentType);

        const componentRef = viewContainerRef.createComponent<IPanelComponent<any>>(componentFactory);
        componentRef.instance.setPanel(panel);
      });

  }

  injectCreatePanelConfiguration(host : HostDirective, panel: Panel) : IPanelConfigComponent<any> {
    let viewContainerRef = host.viewContainerRef;
        viewContainerRef.clear();

        let panelComponentType = this
          .panelTypeMapper
          .mapConfiguration(panel.discriminator);

        let componentFactory = this
          .componentFactoryResolver
          .resolveComponentFactory(panelComponentType);

        let componentRef =viewContainerRef.createComponent<IPanelConfigComponent<any>>(componentFactory);
        componentRef.instance.setPanel(panel);

        return componentRef.instance;
  }

  injectPanelConfiguration(host : HostDirective, panelId : number) {
    console.log("injectPanelConfiguration");
  }

}
