import {Component, OnInit, Input, ElementRef} from '@angular/core';
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

  setPanel(panel : StaticBranchPanel) {
    this.panel = panel;
    
    this.updateData();

    // temp pooling every 10s
    setInterval(() => this.updateData(), 10000);
  }

  updateData() {
    this.projectsApi.getProject(this.panel.projectId).subscribe(project => {
      this.pipeline = project.pipelines.find(pipeline => pipeline.ref == this.panel.staticBranchName);
    })
  }
  
  constructor(private projectsApi : ProjectsApiService, private elementRef : ElementRef) {
    console.log(elementRef);
    // TODO scale visualisation based on actual dimensions of component
    //elementRef.nativeElement.clientWidth;
  }

  getColorOfStage(stage : Stage) {
    const stageOfStage = this.getStateOfStage(stage);
    return this.statusColors.find(statusColor => statusColor.status == stageOfStage).color;
  }

  getStateOfStage(stage : Stage) : string {
    const finalStatuses : string[] = ["canceled", "failed", "running"];
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
      return "created";
    }

  }

  ngOnInit() { }

}
