import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {Panel, PanelType} from "../panel-manager/panel";
import {AdminModeService} from "./admin-mode-service/admin-mode.service";

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit {

  constructor(private adminModeService : AdminModeService, private panelManagerService : PanelManagerService, private route : ActivatedRoute, private _router : Router) {}

  panels : Panel[];

  adminMode : boolean = false;

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

}