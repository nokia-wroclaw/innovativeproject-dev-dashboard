import {Component, OnInit, Input, ViewChild} from '@angular/core';
import {HostDirective} from './../host.directive';
import {PanelManagerService} from "../../panel-manager/service/panel-manager.service";
import {Panel} from "../../panel-manager/panel";
import { DashboardConfigurationService } from '../../configuration/dashboard-configuration/dashboard-service/dashboard-configuration.service';

@Component({
  selector: 'app-static-host-panel',
  templateUrl: './static-host-panel.component.html',
  styleUrls: ['./static-host-panel.component.css', './../host-panel.shared.css']
})
export class StaticHostPanelComponent implements OnInit {

  @Input()
  adminMode : Boolean = true;

  @Input()
  panel : Panel;

  @Input()
  hidePanelTitle : boolean = false;

  @Input()
  tileTitle : String;

  @Input()
  lastUpdated : string = "Last updated...";

  @Input()
  labelInput : string = "Updated";

  @ViewChild(HostDirective)
  panelHost : HostDirective;

  constructor(private panelManagerService : PanelManagerService, private dashboardConfigurationService: DashboardConfigurationService) {}

  ngOnInit() {
    this
      .panelManagerService
      .injectPanelComponent(this.panelHost, this.panel);
    
    this.hidePanelTitle = this.dashboardConfigurationService.getPanelTitleStatus();
  }

}
