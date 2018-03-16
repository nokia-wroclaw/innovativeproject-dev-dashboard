import { Component, OnInit } from '@angular/core';

import { PanelManagerService } from './../panel-manager/panel-manager.service';
import { PanelType } from './../panel-manager/panel-type';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private panelManagerService : PanelManagerService) { }

  PanelType = PanelType;

  panelData: any;

  ngOnInit() {
    this.panelData = this.panelManagerService.getPanelPositionData();
  }

}
