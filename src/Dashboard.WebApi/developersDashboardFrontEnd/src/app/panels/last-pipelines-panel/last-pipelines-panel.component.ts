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

  setPanel(panel: LastPipelinesPanel) {
    this.panel = panel;
    
    // temp
    this.numbers = Array(this.panel.howManyLastPipelinesToRead).fill(0).map((x,i)=>i);
  }

  constructor(private projectsApi: ProjectsApiService, private elementRef: ElementRef) {
    console.log(elementRef);
    
  }

  ngOnDestroy(): void {
  
  }
}
