import {Component, OnInit, Input, ViewChild, OnDestroy} from '@angular/core';
import {Panel} from "../../panel-manager/panel";
import {Project} from "../../projects-manager/project";
import {HostDirective} from "../../panel-host/host.directive";
import {PanelManagerService} from "../../panel-manager/service/panel-manager.service";
import {IPanelConfigComponent} from "../../panels/panel.component";
import {ActivatedRoute, Router} from "@angular/router";
import {ProjectsApiService} from "../../projects-manager/api/projects-api.service";
import {PanelTypeService} from '../../panel-manager/service/panel-type/panel-type.service';
import {PanelType} from '../../panel-manager/service/panel-type/panel-type';
import {PanelApiService} from '../../panel-manager/service/api/panel-api.service';
import {NotificationService, NotificationType} from '../../snackbar/notification.service';
import {isDefined} from '@angular/compiler/src/util';
import {isUndefined} from 'util';

@Component({
  templateUrl: './panel-configuration.component.html',
  styleUrls: ['./panel-configuration.component.css', './../panel.shared.css']
})
export class PanelConfigurationComponent implements OnInit,
OnDestroy {

  constructor(private router : Router, private route : ActivatedRoute, private panelTypeService : PanelTypeService, private projectsApi : ProjectsApiService, private panelManager : PanelManagerService, private panelApi : PanelApiService, private notificationService : NotificationService) {}

  @ViewChild(HostDirective)
  configurationHost : HostDirective;

  editMode : boolean = false;

  projects : Project[] = [];

  panelTypes : PanelType[];

  panelSpecificConfiguration : IPanelConfigComponent < any >;

  // try null positions
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
    this.loadPossiblePanelTypes();

    this.routeParamsSubscription = this
      .route
      .params
      .subscribe(params => {
        if (params['id'] != null) {
          this.editMode = true;

          this
            .panelManager
            .getPanel(params['id'])
            .subscribe(panel => {
              this.panel = panel
              this.descriminatorSelectionChanged();
            });
        } else {
          this.editMode = false;
        }
      });
  }

  descriminatorSelectionChanged() {
    this.panel.panelType = this
      .panelTypeService
      .getPanelType(this.panel);

    this.panelSpecificConfiguration = this
      .panelManager
      .injectPanelConfiguration(this.configurationHost, this.panel);
  }

  private loadPossibleProjects() {
    this
      .projectsApi
      .getProjects()
      .subscribe(projects => this.projects = projects);
  }

  private loadPossiblePanelTypes() {
    this.panelTypes = this
      .panelTypeService
      .getPanelTypes();
  }

  // todo learn to chain promises instead of nesting??
  submitPanel() {
    if (this.panelSpecificConfiguration.isValid()) {
      this.panel.position.height = this.panel.panelType.bounds.defaultHeight;
      this.panel.position.width = this.panel.panelType.bounds.defaultWidth;

      // TODO find empty slot on grid (calculate)

      this
        .panelSpecificConfiguration
        .postPanel(this.editMode)
        .subscribe(response => {
          console.log(response);
          // TODO this.panelManager.updatePanels();
          this
            .router
            .navigate(['/']);
        }, error => {
          var message = "Couldn't save the form";
          if (error.error != undefined && error.error.errors != undefined && error.error.errors.length > 0 && error.error.errors[0].errorMessage != undefined) {
            message = error.error.errors[0].errorMessage;
          }

          this
            .notificationService
            .addNotification(message, NotificationType.Failure);
        });

    } else {
      this
        .notificationService
        .addNotification('Panel specific configuration is invalid.', NotificationType.Failure);
    }
  }

  onDelete() {
    if (this.editMode) {
      this
        .panelApi
        .deletePanel(this.panel)
        .subscribe(response => {
          console.log(response);
          this
            .router
            .navigate(['/']);
        });
    }
  }

}
