import {Component, OnInit} from '@angular/core';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {PanelType} from './../panel-manager/panel-type';
import {Panel} from "../panel-manager/panel";

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit {

  constructor(private panelManagerService : PanelManagerService) {}

  PanelType = PanelType;

  panelData : Panel[];

  ngOnInit() {
    this.panelData = this
      .panelManagerService
      .getPanelData();

  }

  getPanelsForColumn(column : number) : Panel[] {
    return this
      .panelData
      .filter(panel => panel.position.column == column)
      .sort((aPanel, bPanel) => aPanel.position.row - bPanel.position.row);
  }

}
