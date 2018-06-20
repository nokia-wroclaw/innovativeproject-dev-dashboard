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

  constructor(){}

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

    let color = this
        .statusColors
        .find(statusColor => statusColor.statusCode === stage.stageStatus)
        .color

    // The color of running stage should be dependent on its progress
    if(color === '#8eccff') {
      color = 'rgba(142, 204, 255,' + (0.2 + stage.successed / stage.total) + ')';
    };

    return color;
  }

  getFullWidth() : number {return this.pipelineContainer.nativeElement.clientWidth;}

}
