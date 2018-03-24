import {Component, OnInit} from '@angular/core';
import './../panel.component';

@Component({selector: 'app-empty-panel', templateUrl: './empty-panel.component.html', styleUrls: ['./empty-panel.component.css']})
export class EmptyPanelComponent implements OnInit { //extends PanelComponent

  // static IS_DYNAMIC : boolean = false; static NAME : string = 'Empty Panel';

  loadConfiguration(config : any) {
    console.log("Loading configuration: " + config);
  }

  constructor() {
    // super();
  }

  ngOnInit() {}

}
