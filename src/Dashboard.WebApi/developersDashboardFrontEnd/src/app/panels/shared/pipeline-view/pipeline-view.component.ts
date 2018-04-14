import {Component, OnInit, Input, ElementRef, ViewChild} from '@angular/core';
import {Pipeline, Stage} from '../../../projects-manager/project';

@Component({selector: 'app-pipeline-view', templateUrl: './pipeline-view.component.html', styleUrls: ['./pipeline-view.component.css']})
export class PipelineViewComponent implements OnInit {

  @Input()
  pipeline : Pipeline;

  @ViewChild('pipelineContainer') 
  pipelineContainer : ElementRef; 

  pipelineContainerHeight : number = 70;

  pipelineBlockPadding : number = 3;

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

  constructor(private elementRef : ElementRef) {
    console.log(elementRef);

    // TODO scale visualisation based on actual dimensions of component
    // console.log('Client width: ' + elementRef.nativeElement.clientWidth);
  }

  getColorOfStage(stage : Stage) {

    return this
      .statusColors
      .find(statusColor => statusColor.status == stage.stageStatus)
      .color;
  }

  getOneStageWidth() : number {
    return (this.pipelineContainer.nativeElement.clientWidth ) / this.pipeline.stages.length;
  }

}
