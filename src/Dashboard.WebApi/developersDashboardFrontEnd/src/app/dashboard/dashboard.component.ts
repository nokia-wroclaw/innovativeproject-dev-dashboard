import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {Panel, PanelType, PanelPositionUpdateItem} from "../panel-manager/panel";
import {AdminModeService} from "./admin-mode-service/admin-mode.service";

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit {

  constructor(private adminModeService : AdminModeService, private panelManagerService : PanelManagerService, private route : ActivatedRoute, private _router : Router) {}

  panels : Panel[];

  adminMode : boolean = false;

  private positionUpdateThrottle : any;

  private updatePositionsRequestThrottle : number = 1000;

  gridsterOptions = {
    lanes: 6,
    direction: 'vertical',
    floating: true,
    dragAndDrop: true,
    responsiveView: true,
    resizable: true,
    useCSSTransforms: true,
    widthHeightRatio: 1.4
  };

  ngOnInit() {
    this
      .panelManagerService
      .getPanels()
      .subscribe(panels => this.panels = panels);

    this
      .adminModeService
      .adminMode
      .subscribe(adminMode => this.adminMode = adminMode);
  }

  updatePositionsThrottled() {
    if(!this.positionUpdateThrottle) {
      this.positionUpdateThrottle = setTimeout( () => {this.updatePositions(); this.positionUpdateThrottle = null}, this.updatePositionsRequestThrottle);
    } 
  }

  updatePositions() {
    const panelPositions = this.panels.map<PanelPositionUpdateItem>(panel => {
      return {panelId : panel.id, position: panel.position};
    });

    this.panelManagerService.updatePanelPositions(panelPositions).subscribe(response => null, error => console.log(error));
  }

  onGridsterItemChange($event, panel : Panel) {
    this.updatePositionsThrottled();
  }

}