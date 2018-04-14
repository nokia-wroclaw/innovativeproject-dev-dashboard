import {Component, OnInit, Input, ElementRef, ViewChild} from '@angular/core';
import {Pipeline, Stage} from '../../../projects-manager/project';

@Component({selector: 'app-pipeline-view', templateUrl: './pipeline-view.component.html', styleUrls: ['./pipeline-view.component.css']})
export class PipelineViewComponent implements OnInit {

  @Input()
  pipeline : Pipeline;

  @ViewChild('pipelineContainer')
  pipelineContainer : ElementRef;

  // display configuration
  style : number = 2; // style 1 - arrows, style 2 - rectangles
  pipelineContainerHeight : number = 70;
  pipelineBlockPadding : number = 3; 
  arrowIndent : number = 15;


  ngOnInit() {
    // temp
    setInterval(() => {
      this.style = this.style == 1 ? 2 : 1;
    }, 5000);

  }

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

  getOneStageWidth() : number {
    return(this.pipelineContainer.nativeElement.clientWidth - (this.pipeline.stages.length - 1) * this.pipelineBlockPadding) / this.pipeline.stages.length;
  }

  getFullWidth() : number {return this.pipelineContainer.nativeElement.clientWidth;}

  getXPositionForLabel(i : number) : number {
    const offset: number = i*(this.pipelineBlockPadding + this.getOneStageWidth());

    return offset + this.getOneStageWidth() / 2 + this.arrowIndent / 2;
  }

  getPolygonBoundaries(i : number) : string {

    const builder: PolygonBoundariesBuilder = new PolygonBoundariesBuilder();
    const oneStageWidth = this.getOneStageWidth();
    const offset: number = i*(this.pipelineBlockPadding + oneStageWidth);

    if (i == 0 && i == this.pipeline.stages.length - 1) {
      builder
        .appendVerticle(0, 0)
        .appendVerticle(oneStageWidth, 0)
        .appendVerticle(oneStageWidth, this.pipelineContainerHeight)
        .appendVerticle(0, this.pipelineContainerHeight);
    } else if (i == 0) {
      builder
        .appendVerticle(0, 0)
        .appendVerticle(oneStageWidth, 0)
        .appendVerticle(oneStageWidth + this.arrowIndent, this.pipelineContainerHeight / 2)
        .appendVerticle(oneStageWidth, this.pipelineContainerHeight)
        .appendVerticle(0, this.pipelineContainerHeight);
    } else if (i == this.pipeline.stages.length - 1) {
      builder
        .appendVerticle(offset, 0)
        .appendVerticle(offset + oneStageWidth, 0)
        .appendVerticle(offset + oneStageWidth, this.pipelineContainerHeight)
        .appendVerticle(offset, this.pipelineContainerHeight)
        .appendVerticle(offset + this.arrowIndent, this.pipelineContainerHeight / 2);
    } else {
      builder
        .appendVerticle(offset, 0)
        .appendVerticle(offset + oneStageWidth, 0)
        .appendVerticle(offset + oneStageWidth + this.arrowIndent, this.pipelineContainerHeight / 2)
        .appendVerticle(offset + oneStageWidth, this.pipelineContainerHeight)
        .appendVerticle(offset, this.pipelineContainerHeight)
        .appendVerticle(offset + this.arrowIndent, this.pipelineContainerHeight / 2);
    }

    return builder.build();
  }

}

class PolygonBoundariesBuilder {
  private builder : string = "";

  public appendVerticle(x : number, y : number) : PolygonBoundariesBuilder {
    this.builder += x.toString() + ',' + y.toString() + ' ';
    return this;
  }

  public build() : string {return this.builder;}

}
