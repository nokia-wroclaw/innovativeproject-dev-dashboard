import {Component, OnInit, Input} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {StaticBranchPanel} from "./static-branch";

@Component({templateUrl: './static-branch-panel.component.html'})
export class StaticBranchPanelComponent implements OnInit, IPanelComponent<StaticBranchPanel> {

  panel : StaticBranchPanel;

  setPanel(panel : StaticBranchPanel) {
    this.panel = panel;
  }
  
  constructor() {}

  ngOnInit() {
    console.log("StaticBranchPanelComponent onInit()");
  }

}
