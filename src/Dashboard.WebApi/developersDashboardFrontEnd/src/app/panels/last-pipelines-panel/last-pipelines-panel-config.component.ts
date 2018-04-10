import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {PanelsConfigApiService} from "../panels-config-api.service";
import {Observable} from "rxjs/Observable";
import {LastPipelinesPanel} from "./last-pipelines";

@Component({template: `
    <mat-form-field class="example-full-width">
        <input matInput placeholder="How many last pipelines to read?" required [(ngModel)]="panel.howManyLastPipelinesToRead" name="howMany">
    </mat-form-field>
`, styleUrls: ['./../../configuration/panel.shared.css']})
export class LastPipelinesPanelConfigComponent implements OnInit, IPanelConfigComponent<LastPipelinesPanel> {

    createPanelUrl : string = "/api/Panel/CreateDynamicPipelinesPanel";

    panel : LastPipelinesPanel;

    constructor(private panelsConfigApi : PanelsConfigApiService) {}

    isValid() : boolean {
        const value = this.panel.howManyLastPipelinesToRead;
        return value != null && (!isNaN(value) && value >= 1 && value <= 10) ;
    }

    setPanel(panel : any) {
        this.panel = panel;
    }
    postPanel() : Observable<LastPipelinesPanel> {
        return this.panelsConfigApi.savePanel<LastPipelinesPanel>(this.createPanelUrl, this.panel)
        .map(response => {console.log(response); return response});
    }

    ngOnInit() {

    }

}
