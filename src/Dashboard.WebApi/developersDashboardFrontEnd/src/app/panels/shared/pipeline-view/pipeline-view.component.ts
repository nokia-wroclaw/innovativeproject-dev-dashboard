import {
  Component,
  OnInit,
  Input,
  ElementRef,
  ViewChild,
  DoCheck
} from '@angular/core';
import {Pipeline, Stage} from '../../../projects-manager/project';

interface StatusWithColor {
  statusCode : number,
  status : string,
  color : string
}

@Component({selector: 'app-pipeline-view', templateUrl: './pipeline-view.component.html', styleUrls: ['./pipeline-view.component.css']})
export class PipelineViewComponent {

  @Input()
  pipeline : Pipeline;

  @ViewChild('pipelineContainer')
  pipelineContainer : ElementRef;

  statusColors : StatusWithColor[] = [
    {
      status: 'running',
      statusCode: 0,
      color: '#8eccff'
    }, {
      status: 'failed',
      statusCode: 1,
      color: '#d9534f'
    }, {
      status: 'success',
      statusCode: 2,
      color: 'green'
    }, {
      status: 'created',
      statusCode: 3,
      color: 'white'
    }, {
      status: 'canceled',
      statusCode: 4,
      color: 'gray'
    }
  ];

  getColorOfStage(stage : Stage) {
    return this
      .statusColors
      .find(statusColor => statusColor.statusCode === stage.stageStatus)
      .color;
  }

  getFullWidth() : number {return this.pipelineContainer.nativeElement.clientWidth;}

}
