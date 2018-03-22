import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import {PanelManagerService} from './../panel-manager/service/panel-manager.service';
import {Panel, PanelType} from "../panel-manager/panel";
import {PanelDataService} from '../panel-create/service/panel-data.service';

@Component({selector: 'app-dashboard', templateUrl: './dashboard.component.html', styleUrls: ['./dashboard.component.css']})
export class DashboardComponent implements OnInit {

  constructor(private panelManagerService : PanelManagerService, private route : ActivatedRoute, private _router : Router, public panelDataService : PanelDataService) {}

  panelData : Panel[];

  adminMode : boolean = false;

  gridsterOptions = {
    lanes: 6, // how many lines (grid cells) dashboard has
    direction: 'vertical', // items floating direction: vertical/horizontal/none
    floating: true, // default=true - prevents items to float according to the direction (gravity)
    dragAndDrop: true,
    resizable: true, // possible to resize items by drag n drop by item edge/corner
    useCSSTransforms: true // Uses CSS3 translate() instead of position top/left - significant performance boost.
  };

  ngOnInit() {
    this
      .panelManagerService
      .getPanelData()
      .subscribe(panelData => this.panelData = panelData);

    this.adminMode = this.route.snapshot.data.adminMode;
  }

  getPanelsForColumn(column : number) : Panel[] {
    if (this.panelData == undefined) {
      return [];
    } else {
      return this
        .panelData
        .filter(panel => panel.position.column == column)
        .sort((aPanel, bPanel) => aPanel.position.row - bPanel.position.row);
    }
  }

  onAddCardClick(inputValue : number) {
    this.panelDataService.columnNumber = inputValue;
  }

}