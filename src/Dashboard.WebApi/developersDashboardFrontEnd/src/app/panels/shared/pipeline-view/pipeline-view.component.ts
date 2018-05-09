import {Component, OnInit, Input, ElementRef, ViewChild, DoCheck} from '@angular/core';
import {Pipeline, Stage} from '../../../projects-manager/project';

@Component({selector: 'app-pipeline-view', templateUrl: './pipeline-view.component.html', styleUrls: ['./pipeline-view.component.css']})
export class PipelineViewComponent implements OnInit {

  @Input()
  pipeline : Pipeline;

  @ViewChild('pipelineContainer')
  pipelineContainer : ElementRef;



  ngOnInit() { }

  statusColors : any[] = [
    {
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
    }
  ];

  getColorOfStage(stage : Stage) {
    return this
      .statusColors
      .find(statusColor => statusColor.status == stage.stageStatus)
      .color;
  }
  


  getFullWidth() : number {return this.pipelineContainer.nativeElement.clientWidth;}



  
}
