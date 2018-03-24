import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {RandomMemePanel} from "./random-meme-panel";
import {PanelsConfigApiService} from "../panels-config-api.service";
import {Observable} from "rxjs/Observable";

@Component({template: `
    <mat-form-field class="example-full-width">
        <input matInput placeholder="Imgur token" required [(ngModel)]="panel.memeApiToken" name="token">
    </mat-form-field>
`})
export class RandomMemePanelConfigComponent implements OnInit, IPanelConfigComponent<RandomMemePanel> {

    createPanelUrl : string = "/api/Panel/CreateMemePanel";

    private panel : RandomMemePanel;

    constructor(private panelsConfigApi : PanelsConfigApiService) {}

    isValid() : boolean {
        return this.panel.memeApiToken != null && this.panel.memeApiToken != '';
    }

    setPanel(panel : any) {
        this.panel = panel;
    }
    postPanel() : Observable<RandomMemePanel> {
        return this.panelsConfigApi.savePanel<RandomMemePanel>(this.createPanelUrl, this.panel)
        .map(response => {console.log(response); return response});
    }

    ngOnInit() {

    }

}
