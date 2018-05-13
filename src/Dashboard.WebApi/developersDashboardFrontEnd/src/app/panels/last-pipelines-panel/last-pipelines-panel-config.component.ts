import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {Observable} from "rxjs/Observable";
import {LastPipelinesPanel} from "./last-pipelines";
import { PanelApiService } from '../../panel-manager/service/api/panel-api.service';
import { isUndefined } from 'util';

@Component({template: `
    <mat-form-field class="example-full-width">
        <input matInput placeholder="How many last pipelines to read?" required [(ngModel)]="panel.howManyLastPipelinesToRead" name="howMany">
    </mat-form-field>
    <mat-form-field class="example-full-width">
        <input matInput placeholder="Branch regex expression" required [(ngModel)]="panel.panelRegex" name="panelRegex">
    </mat-form-field>
`, styleUrls: ['./../../configuration/panel.shared.css']})
export class LastPipelinesPanelConfigComponent implements OnInit, IPanelConfigComponent<LastPipelinesPanel> {

    panel : LastPipelinesPanel;

    constructor(private panelApi : PanelApiService) {}

    isValid() : boolean {
        const value = this.panel.howManyLastPipelinesToRead;

        var isRegexValid = true;
        try {
            new RegExp(this.panel.panelRegex);
        } catch(e) {
            isRegexValid = false;
        }

        return value != null && (!isNaN(value) && value >= 1 && value <= 10) && isRegexValid ;
    }

    setPanel(panel : any) {
        this.panel = panel;
    }
    postPanel(edit : boolean) : Observable<LastPipelinesPanel> {
        return this.panelApi.saveOrUpdate(edit, this.panel).map(response => {
            console.log(response);
            return response
        });
    }

    ngOnInit() {
        if(isUndefined(this.panel.panelRegex)) {
            this.panel.panelRegex = ".*";
        }
    }

}
