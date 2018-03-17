import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {PanelType} from './../panel-manager/panel-type';
import {Panel} from "../panel-manager/panel";
import {PanelDataService} from '../panel-create/service/panel-data.service';

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit {

  constructor(private panelManagerService : PanelManagerService, private route : ActivatedRoute, private _router : Router, public panelDataService : PanelDataService) {}

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

  onclck(inputValue : number) {
    this.panelDataService.columnNumber = inputValue;
  }

}