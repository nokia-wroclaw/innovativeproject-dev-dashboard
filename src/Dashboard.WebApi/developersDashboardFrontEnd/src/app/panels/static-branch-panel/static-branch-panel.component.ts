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
    status: "manual",
    color: 'pink'
  }, {
    status: "success",
    color: 'green'
  }, {
    status: "created",
    color: "white"
  }, {
    status: "skipped",
    color: "gray"
  }];

  private pipelineSub : Subscription;

  setPanel(panel: StaticBranchPanel) {
    this.panel = panel;

    // subscribtion for further updates of related project
    this.pipelineSub = this.projectsApi.getProject(this.panel.projectId)
      .filter(project => project != null)
      .filter(project => project.staticPipelines != null)
      .map(project => project.staticPipelines.find(pipeline => pipeline.ref == this.panel.staticBranchName))
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
    return this.statusColors.find(statusColor => statusColor.status == stage.stageStatus).color;
  }

}
