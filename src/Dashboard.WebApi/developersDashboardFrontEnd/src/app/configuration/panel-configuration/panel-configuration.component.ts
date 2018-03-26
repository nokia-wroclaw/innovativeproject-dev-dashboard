import {Component, OnInit, Input, ViewChild, OnDestroy} from '@angular/core';
import {PanelType, Panel} from "../../panel-manager/panel";
import {PanelTypeMapperService} from "../../panel-manager/service/panel-type-mapper/panel-type-mapper.service";
import {ProjectsManagerService} from "../../projects-manager/projects-manager.service";
import {Project} from "../../projects-manager/project";
import {HostDirective} from "../../panel-host/host.directive";
import {PanelManagerService} from "../../panel-manager/service/panel-manager.service";
import {IPanelConfigComponent} from "../../panels/panel.component";
import {ActivatedRoute} from "@angular/router";

@Component({
  templateUrl: './panel-configuration.component.html',
  styleUrls: ['./panel-configuration.component.css', './../panel.shared.css']
})
export class PanelConfigurationComponent implements OnInit,
OnDestroy {

  constructor(private route : ActivatedRoute, private panelTypeMapper : PanelTypeMapperService, private projectsManager : ProjectsManagerService, private panelManager : PanelManagerService) {}

  @ViewChild(HostDirective)
  configurationHost : HostDirective;

  projects : Project[] = [];

  // Temporarily ugly hardcoded TODO
  types = ["MemePanel", "StaticBranchPanel"];

  panelSpecificConfiguration : IPanelConfigComponent < any >;

  panel : Panel = {
    title: null,
    discriminator: null,
    projectId: null,
    position: {
      width: 1,
      height: 1,
      column: 1,
      row: 1
    }
  };

  panelId : number;

  private routeParamsSubscription;

  ngOnDestroy() {
    this
      .routeParamsSubscription
      .unsubscribe();
  }

  ngOnInit() {
    this.loadPossibleProjects();

    this.routeParamsSubscription = this
      .route
      .params
      .subscribe(params => {
        if (params['id'] != null) {
          this
            .panelManager
            .getPanel(params['id'])
            .subscribe(panel => {
              this.panel = panel
              this.descriminatorSelectionChanged();
            });
        } else {
          console.log("id was null");
        }
      });
  }

  descriminatorSelectionChanged() {
    this.panelSpecificConfiguration = this
      .panelManager
      .injectCreatePanelConfiguration(this.configurationHost, this.panel);
  }

  private loadPossibleProjects() {
    this
      .projectsManager
      .getProjects()
      .subscribe(projects => this.projects = projects);
  }

  // todo learn to chain promises instead of nesting??
  submitPanel() {
    if (this.panelSpecificConfiguration.isValid()) {
      this
        .panelSpecificConfiguration
        .postPanel()
        .subscribe(response => {
          console.log(response);
          // TODO 
          this.panelManager.updatePanels();
        }
      );
    } else {
      console.log("Panel type specific configuration is invalid!")
    }
  }

}
