import { Component, OnInit, Input, ElementRef, OnDestroy } from '@angular/core';
import './../panel.component';
import { IPanelComponent } from "./../panel.component";
import { LastPipelinesPanel } from "./last-pipelines";
import { ProjectsApiService } from '../../projects-manager/api/projects-api.service';
import { Pipeline, Stage } from '../../projects-manager/project';
import { Subscription } from 'rxjs';

@Component({ templateUrl: './last-pipelines-panel.component.html' })
export class LastPipelinesPanelComponent implements OnDestroy, IPanelComponent<LastPipelinesPanel> {

  panel: LastPipelinesPanel;

  numbers: number[];
  
  private pipelineSub : Subscription;

  /**
   * Pipelines to visualize.
   */
  pipelines : Pipeline[];

  setPanel(panel : LastPipelinesPanel) {
    this.panel = panel;

    // TODO get N newest? or it is guaranted to be N newest by the backend?

    // subscribtion for further updates of related project
    this.pipelineSub = this
      .projectsApi
      .getProject(this.panel.projectId)
      .filter(project => project != null)
      .filter(project => project.dynamicPipelines != null)
      .map(project => project.dynamicPipelines)
      .subscribe(pipelines => this.pipelines = pipelines.slice(0, this.panel.howManyLastPipelinesToRead));
  }

  constructor(private projectsApi: ProjectsApiService, private elementRef: ElementRef) {
    console.log(elementRef);
  }

  ngOnDestroy(): void {
  
  }
}
