import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {PanelsConfigApiService} from "../panels-config-api.service";
import {Observable} from "rxjs/Observable";
import {StaticBranchPanel} from "./static-branch";

@Component({template: `
    <p> static branch config </p>
`})
export class StaticBranchPanelConfigComponent implements OnInit, IPanelConfigComponent<StaticBranchPanel> {

    createPanelUrl : string = "/api/Panel/CreateStaticBranchPanel";

    private panel : StaticBranchPanel;

    constructor(private panelsConfigApi : PanelsConfigApiService) {}

    isValid() : boolean {
        return true;
    }

    setPanel(panel : any) {
        this.panel = panel;
        // temp // btw shouldnt it be one branch name? static card --> one static branch?
        this.panel.staticBranchNames = ["master"];
    }
    postPanel() : Observable<StaticBranchPanel> {
        return this.panelsConfigApi.savePanel<StaticBranchPanel>(this.createPanelUrl, this.panel)
        .map(response => {console.log(response); return response});
    }

    ngOnInit() {

    }

}
