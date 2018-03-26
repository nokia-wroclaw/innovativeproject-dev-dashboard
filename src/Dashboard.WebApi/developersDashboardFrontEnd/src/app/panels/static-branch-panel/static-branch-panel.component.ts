import {Component, OnInit, Input} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {StaticBranchPanel} from "./static-branch";
import { ProjectsApiService } from '../../projects-manager/api/projects-api.service';
import { Pipeline } from '../../projects-manager/project';

@Component({templateUrl: './static-branch-panel.component.html'})
export class StaticBranchPanelComponent implements OnInit, IPanelComponent<StaticBranchPanel> {

  panel : StaticBranchPanel;

  /**
   * A pipeline to visualize
   */
  pipeline : Pipeline;

  stages : any[] = [{name: "pre-test", color:"#5cb85c"}, {name: "test", color:"#d9534f"}, {name: "deploy", color:"white"}, {name: "xd", color:"white"}];

  setPanel(panel : StaticBranchPanel) {
    this.panel = panel;
  }

  updateData() {
    this.projectsApi.getProject(this.panel.projectId).subscribe(project => {

    })
  }
  
  constructor(private projectsApi : ProjectsApiService) {
    this.updateData();
  }

  ngOnInit() {
    console.log("StaticBranchPanelComponent onInit()");
  }

}
