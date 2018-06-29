import {Component, OnInit, Input, ElementRef, OnDestroy} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {StaticBranchPanel} from "./static-branch";
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Pipeline, Stage} from '../../projects-manager/project';
import {Subscription} from 'rxjs';
import {PipelineService} from '../shared/pipeline-view/pipeline-service/pipeline.service';
import {Observable} from 'rxjs/Observable';

@Component({templateUrl: './static-branch-panel.component.html'})
export class StaticBranchPanelComponent implements OnDestroy,
IPanelComponent < StaticBranchPanel > {

  panel : StaticBranchPanel;

  /**
   * A pipeline to visualize
   */
  pipeline : Pipeline;

  private intervalSub : Subscription;

  constructor(private pipelineService : PipelineService) {}

  setPanel(panel : StaticBranchPanel) {
    this.panel = panel;

    this.intervalSub = Observable
      .interval(10000)
      .startWith(0)
      .subscribe(() => {
        this
          .pipelineService
          .getPipelines(this.panel.id)
          .filter(pipelines => pipelines.length > 0)
          .subscribe(pipelines => this.pipeline = pipelines[0]);
      })
  }

  ngOnDestroy() : void {
    this
      .intervalSub
      .unsubscribe();
  }

}