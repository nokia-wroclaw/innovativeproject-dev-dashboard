import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {Panel, PanelPositionUpdateItem} from "../panel-manager/panel";
import {AdminModeService} from "./admin-mode-service/admin-mode.service";
import {NotificationService, NotificationType} from '../snackbar/notification.service';
import { FailureMessage, failureMessages } from '../snackbar/notification-messages';

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit,
OnDestroy {

  constructor(private adminModeService : AdminModeService, private panelManagerService : PanelManagerService, private route : ActivatedRoute, private _router : Router, private notificationService : NotificationService) {}

  panels : Panel[] = null;

  adminMode : boolean = false;

  private positionUpdateThrottle : any;

  private updatePositionsRequestThrottle : number = 1000;

  gridsterOptions = {
    lanes: 16,
    direction: 'vertical',
    floating: true,
    dragAndDrop: true,
    responsiveView: true,
    resizable: true,
    useCSSTransforms: true,
    cellHeight: 175
  };

  ngOnInit() {
    this
      .panelManagerService
      .getPanels()
      .subscribe(panels => this.panels = panels, 
        error => this.notificationService.addNotification(failureMessages.get(FailureMessage.FETCH_PANELS_FAILED) + ": " + error.statusText, NotificationType.Failure));

    this
      .adminModeService
      .adminMode
      .subscribe(adminMode => this.adminMode = adminMode);
  }

  ngOnDestroy() : void {
    if(this.positionUpdateThrottle) {
      clearTimeout(this.positionUpdateThrottle);
      this.updatePositions();
    }
  }

  updatePositionsThrottled() {
    if (!this.positionUpdateThrottle) {
      this.positionUpdateThrottle = setTimeout(() => {
        this.updatePositions();
        this.positionUpdateThrottle = null
      }, this.updatePositionsRequestThrottle);
    }
  }

  updatePositions() {
    const panelPositions = this.panels.map < PanelPositionUpdateItem > (panel => {
      return {panelId: panel.id, position: panel.position};
    });

    this
      .panelManagerService
      .updatePanelPositions(panelPositions)
      .subscribe(response => null, error => console.log(error));
  }

  onGridsterItemChange($event, panel : Panel) {
    this.updatePositionsThrottled();
  }

}