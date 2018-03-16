import { Component, OnInit } from '@angular/core';
import './../panel.component';

@Component({
  selector: 'app-empty-panel',
  templateUrl: './empty-panel.component.html',
  styleUrls: ['./empty-panel.component.css']
})
export class EmptyPanelComponent extends PanelComponent implements OnInit {

  loadConfiguration(config : any) {
    console.log("Loading configuration: " + config);
  }

  constructor() { super(); }

  ngOnInit() {
  }

}
