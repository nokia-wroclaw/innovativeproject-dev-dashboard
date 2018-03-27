import {Component, OnInit, Input} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {StaticBranchPanel} from "./static-branch";
import { ProjectsApiService } from '../../projects-manager/api/projects-api.service';
import { Pipeline, Stage } from '../../projects-manager/project';

@Component({templateUrl: './static-branch-panel.component.html'})
export class StaticBranchPanelComponent implements OnInit, IPanelComponent<StaticBranchPanel> {

  panel : StaticBranchPanel;

  /**
   * A pipeline to visualize
   */
  pipeline : Pipeline;

  testColor : string = "white";

  statusColors : any[] = [{
    status: "failed",
    color: '#d9534f'
  }, {
    status: "running",
    color: 'blue'
  }, {
    status: "canceled",
    color: 'pink'
  }, {
    status: "success",
    color: 'green'
  }, {
    status: "created",
    color: "white"
  }];

  stages : any[] = [{name: "pre-test", color:"#5cb85c"}, {name: "test", color:"#d9534f"}, {name: "deploy", color:"white"}, {name: "xd", color:"white"}];

  setPanel(panel : StaticBranchPanel) {
    this.panel = panel;
    
    this.updateData();

    // temp pooling every 2s
    setInterval(() => this.updateData(), 2000);
  }

  updateData() {
    this.projectsApi.getProject(this.panel.projectId).subscribe(project => {
      this.pipeline = project.pipelines.find(pipeline => pipeline.ref == this.panel.staticBranchName);
    })
  }
  
  constructor(private projectsApi : ProjectsApiService) {
  }

  getColorOfStage(stage : Stage) {
    const stageOfStage = this.getStateOfStage(stage);
    return this.statusColors.find(statusColor => statusColor.status == stageOfStage).color;
  }

  getStateOfStage(stage : Stage) : string {
    const finalStatuses : string[] = ["canceled", "failed"];
    const finalJob = stage.jobs.find(job => finalStatuses.findIndex(status => job.status == status) != -1);

    if(finalJob != null) {
      return finalJob.status;
    } else {

      let allSuccesses : boolean = true;
      let allCreated : boolean = true;

      stage.jobs.forEach(job => {
        allSuccesses = allSuccesses && job.status == "success";
        allCreated = allCreated && job.status == "created";
      });

      if(allSuccesses) {
        return "success";
      } else if (allCreated) {
        return "created";
      }

      // check other conditions
      // temp
      return "running";
    }

  }

  ngOnInit() {
    console.log("StaticBranchPanelComponent onInit()");
  }

}
