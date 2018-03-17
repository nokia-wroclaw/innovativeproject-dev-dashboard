import {Component, OnInit, Input} from '@angular/core';
import {PanelDataService} from './service/panel-data.service';

@Component({selector: 'app-panel-create', templateUrl: './panel-create.component.html', styleUrls: ['./panel-create.component.css']})
export class PanelCreateComponent implements OnInit {

  constructor(public panelDataService : PanelDataService) {
    this.selectedColumns = this.panelDataService.columnNumber;
  }

  projects = [
    {
      value: 'plugin-0',
      viewValue: 'project1'
    }, {
      value: 'plugin-1',
      viewValue: 'project2'
    }, {
      value: 'plugin-2',
      viewValue: 'project3'
    }
  ];

  columns = [0, 1, 2];

  types = [
    {
      value: 'type-1',
      viewValue: 'panel-1'
    }, {
      value: 'type-2',
      viewValue: 'panel-2'
    }
  ];

  selectedPlugins : any;
  selectedColumns : number;
  selectedPanels : any;

  ngOnInit() {}

}
