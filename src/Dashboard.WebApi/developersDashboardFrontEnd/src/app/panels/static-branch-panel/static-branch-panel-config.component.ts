import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {PanelsConfigApiService} from "../panels-config-api.service";
import {Observable} from "rxjs/Observable";
import {StaticBranchPanel} from "./static-branch";

@Component({template: `
    <mat-form-field class="example-full-width">
        <input matInput placeholder="Branch name" required [(ngModel)]="panel.staticBranchName" name="token">
    </mat-form-field>
`, styleUrls: ['./../../configuration/panel.shared.css']})
export class StaticBranchPanelConfigComponent implements OnInit, IPanelConfigComponent<StaticBranchPanel> {

    createPanelUrl : string = "/api/Panel/CreateStaticBranchPanel";

    panel : StaticBranchPanel;

    constructor(private panelsConfigApi : PanelsConfigApiService) {}

    isValid() : boolean {
        return this.panel.staticBranchName != null && this.panel.staticBranchName != '';
    }

    setPanel(panel : any) {
        this.panel = panel;
    }
    postPanel() : Observable<StaticBranchPanel> {
        return this.panelsConfigApi.savePanel<StaticBranchPanel>(this.createPanelUrl, this.panel)
        .map(response => {console.log(response); return response});
    }

    ngOnInit() {

    }

}
