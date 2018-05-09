import {Component, OnInit, Input, ElementRef, OnDestroy} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {LastPipelinesPanel} from "./last-pipelines";
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Pipeline, Stage} from '../../projects-manager/project';
import {Subscription} from 'rxjs';
import {Observable} from 'rxjs/Observable';
import {PipelineService} from '../shared/pipeline-view/pipeline-service/pipeline.service';

@Component({templateUrl: './last-pipelines-panel.component.html'})
export class LastPipelinesPanelComponent implements OnDestroy,
IPanelComponent < LastPipelinesPanel > {

  panel : LastPipelinesPanel;

  numbers : number[];

  private intervalSub : Subscription;

  /**
   * Pipelines to visualize.
   */
  pipelines : Pipeline[];

  constructor(private pipelineService : PipelineService) {}

  setPanel(panel : LastPipelinesPanel) {
    this.panel = panel;

    this.intervalSub = Observable
      .interval(5000)
      .startWith(0)
      .subscribe(() => {
        this
          .pipelineService
          .getPipelines(this.panel.id)
          .subscribe(pipelines => this.pipelines = pipelines)
      })
  }

  ngOnDestroy() : void {
    this
      .intervalSub
      .unsubscribe();
  }
}
