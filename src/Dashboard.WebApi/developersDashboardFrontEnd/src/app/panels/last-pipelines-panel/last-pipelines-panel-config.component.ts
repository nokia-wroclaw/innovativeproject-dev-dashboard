import {Component, OnInit, ViewChild} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {Observable} from "rxjs/Observable";
import {LastPipelinesPanel} from "./last-pipelines";
import { PanelApiService } from '../../panel-manager/service/api/panel-api.service';
import { isUndefined } from 'util';
import { Form } from '@angular/forms';

@Component({templateUrl: 'last-pipelines-panel-config.template.html', styleUrls: ['./../../configuration/panel.shared.css']})
export class LastPipelinesPanelConfigComponent implements OnInit, IPanelConfigComponent<LastPipelinesPanel> {

    panel : LastPipelinesPanel;

    @ViewChild('lastPipelinesPanelForm') form: any;

    constructor(private panelApi : PanelApiService) {}

    isValid() : boolean {
        return this.form.valid ;
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

        if(isUndefined(this.panel.howManyLastPipelinesToRead)) {
            this.panel.howManyLastPipelinesToRead = 2;
        }
    }

}
