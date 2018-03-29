import { Component, OnInit, Input, ElementRef, OnDestroy } from '@angular/core';
import './../panel.component';
import { IPanelComponent } from "./../panel.component";
import { StaticBranchPanel } from "./static-branch";
import { ProjectsApiService } from '../../projects-manager/api/projects-api.service';
import { Pipeline, Stage } from '../../projects-manager/project';
import { Subscription } from 'rxjs';

@Component({ templateUrl: './static-branch-panel.component.html' })
export class StaticBranchPanelComponent implements OnDestroy, IPanelComponent<StaticBranchPanel> {

  panel: StaticBranchPanel;

  /**
   * A pipeline to visualize
   */
  pipeline: Pipeline;

  statusColors: any[] = [{
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

  private pipelineSub : Subscription;

  setPanel(panel: StaticBranchPanel) {
    this.panel = panel;

    // subscribtion for further updates of related project
    this.pipelineSub = this.projectsApi.getProject(this.panel.projectId)
      .filter(project => project != null)
      .filter(project => project.pipelines != null)
      .map(project => project.pipelines.find(pipeline => pipeline.ref == this.panel.staticBranchName))
      .subscribe(pipeline => this.pipeline = pipeline);
  }

  constructor(private projectsApi: ProjectsApiService, private elementRef: ElementRef) {
    console.log(elementRef);
    // TODO scale visualisation based on actual dimensions of component
    //elementRef.nativeElement.clientWidth;
  }

  ngOnDestroy(): void {
   this.pipelineSub.unsubscribe();
  }

  getColorOfStage(stage: Stage) {
    const stageOfStage = this.getStateOfStage(stage);
    return this.statusColors.find(statusColor => statusColor.status == stageOfStage).color;
  }

  // TODO it should and will be done at backend side, then we will get rid of unnecessary code
  getStateOfStage(stage: Stage): string {
    const finalStatuses: string[] = ["canceled", "failed", "running"];
    const finalJob = stage.jobs.find(job => finalStatuses.findIndex(status => job.status == status) != -1);

    if (finalJob != null) {
      return finalJob.status;
    } else {

      let allSuccesses: boolean = true;
      let allCreated: boolean = true;

      stage.jobs.forEach(job => {
        allSuccesses = allSuccesses && job.status == "success";
        allCreated = allCreated && job.status == "created";
      });

      if (allSuccesses) {
        return "success";
      } else if (allCreated) {
        return "created";
      }

      // check other conditions
      // temp
      return "created";
    }

  }

}
