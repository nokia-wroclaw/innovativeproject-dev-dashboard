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
      status: 2, //Failed
      color: '#d9534f'
    }, {
      status: 0, //Running
      color: 'blue'
    }, {
      status: 1, //Manual
      color: 'pink'
    }, {
      status: 4, //Success
      color: 'green'
    }, {
      status: 5, //Created
      color: "white"
    }, {
      status: 3, //Skipped
      color: "gray"
    },
    {
      status: 6, //Canceled
      color: "gray"
    }
  ];

  getColorOfStage(stage : Stage) {
    return this
      .statusColors
      .find(statusColor => statusColor.status === stage.stageStatus)
      .color;
  }
  


  getFullWidth() : number {return this.pipelineContainer.nativeElement.clientWidth;}



  
}
