import {Component, OnInit, Input, ViewChild} from '@angular/core';
import {HostDirective} from './../host.directive';
import {PanelManagerService} from "../../panel-manager/service/panel-manager.service";

@Component({selector: 'app-dynamic-host-panel', templateUrl: './dynamic-host-panel.component.html', styleUrls: ['./dynamic-host-panel.component.css']})
export class DynamicHostPanelComponent implements OnInit {

  @Input()
  adminMode : Boolean = true;

  @Input()
  panelId : number;

  @Input()
  tileTitle : string;

  @ViewChild(HostDirective)
  panelHost : HostDirective;

  constructor(private panelManagerService : PanelManagerService) {}

  ngOnInit() {
    this
      .panelManagerService
      .injectPanelComponent(this.panelHost, this.panelId);
  }

}
