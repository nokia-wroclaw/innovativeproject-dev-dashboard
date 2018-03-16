import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {PanelType} from './../panel-manager/panel-type';
import {Panel} from "../panel-manager/panel";

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit {

  constructor(private panelManagerService : PanelManagerService, private route : ActivatedRoute) {}

  PanelType = PanelType;

  panelData : Panel[];

  adminMode : boolean = false;

  ngOnInit() {
    this.panelData = this
      .panelManagerService
      .getPanelData();

    this.adminMode = this.route.snapshot.data.adminMode;
  }

  getPanelsForColumn(column : number) : Panel[] {
    return this
      .panelData
      .filter(panel => panel.position.column == column)
      .sort((aPanel, bPanel) => aPanel.position.row - bPanel.position.row);
  }

}