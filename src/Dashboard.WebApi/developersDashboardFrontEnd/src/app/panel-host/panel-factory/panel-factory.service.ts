import {Injectable, ComponentFactoryResolver} from '@angular/core';
import {HostDirective} from './../host.directive'

import {EmptyPanelComponent} from './../../panels/empty-panel/empty-panel.component';


@Injectable()
export class PanelFactoryService {

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  
  private typeMappings = {
    'empty-panel': EmptyPanelComponent
  }

  loadPanelComponent(host : HostDirective, panelType : string, config : any) {
    let viewContainerRef = host.viewContainerRef;
    viewContainerRef.clear();

    let componentFactory = this.componentFactoryResolver.resolveComponentFactory(EmptyPanelComponent);

    let componentRef = viewContainerRef.createComponent(componentFactory);

    // componentRef can now be casted to the common interface and then perform setConfig on it
  }

}
