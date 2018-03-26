import {Injectable, ComponentFactoryResolver} from '@angular/core';
import {HostDirective} from './../../panel-host/host.directive';

import {Panel, PanelPositionUpdateItem} from "../panel";
import {Observable} from "rxjs/Observable";
import {PanelApiService} from "./api/panel-api.service";
import {PanelTypeMapperService} from "./panel-type-mapper/panel-type-mapper.service";

import 'rxjs/add/operator/mergeMap';
import "rxjs/add/observable/of";
import {IPanelComponent, IPanelConfigComponent} from "../../panels/panel.component";

@Injectable()
export class PanelManagerService {

  constructor(private panelApi : PanelApiService, private componentFactoryResolver : ComponentFactoryResolver, private panelTypeMapper : PanelTypeMapperService) {}

  /**
   * Gets panels data from backend endpoint.
   */
  getPanels() : Observable < Panel[] > {
    return this
      .panelApi
      .getPanels();
  }

  /**
   * Gets panel data from backend endpoint for panel of specified id.
   *  
   * @param id id of panel to get data  of
   */
  getPanel(id : number) : Observable < Panel > {
    return this
      .getPanels()
      .map(panels => panels.find(panel => panel.id == id));
  }

  /**
   * Injects a panel component of proper type depending on given panel
   * 
   * @param host host reference, place where the component should be injected
   * @param panel panel that corresponding component of should be injected
   */
  injectPanelComponent(host : HostDirective, panel : Panel) {
    const viewContainerRef = host.viewContainerRef;
    viewContainerRef.clear();

    const panelComponentType = this
      .panelTypeMapper
      .map(panel.discriminator);

    const componentFactory = this
      .componentFactoryResolver
      .resolveComponentFactory(panelComponentType);

    const componentRef = viewContainerRef.createComponent < IPanelComponent < any >> (componentFactory);
    componentRef
      .instance
      .setPanel(panel);

  }

  /**
   * Injects a panel configuration component of proper type depending on given panel
   * 
   * @param host host reference, place where the component should be injected
   * @param panel panel that corresponding configuration component of should be injected
   */
  injectPanelConfiguration(host : HostDirective, panel : Panel) : IPanelConfigComponent < any > {
    const viewContainerRef = host.viewContainerRef;
    viewContainerRef.clear();

    const panelComponentType = this
      .panelTypeMapper
      .mapConfiguration(panel.discriminator);

    const componentFactory = this
      .componentFactoryResolver
      .resolveComponentFactory(panelComponentType);

    const componentRef = viewContainerRef.createComponent < IPanelConfigComponent < any >> (componentFactory);
    componentRef
      .instance
      .setPanel(panel);

    return componentRef.instance;
  }

  /**
   * Cals API to update panel positions on dashboard.
   * 
   * @param panelPositions array of objects that represent current positions of panels on dashboard.
   */
  updatePanelPositions(panelPositions : PanelPositionUpdateItem[]) : Observable < PanelPositionUpdateItem[] > {
    return this
      .panelApi
      .updatePanelPositions(panelPositions);
  }
}
