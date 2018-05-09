import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {RandomMemePanel} from "./random-meme-panel";
import {Observable} from "rxjs/Observable";
import { PanelApiService } from '../../panel-manager/service/api/panel-api.service';

@Component({template: `
    <mat-form-field class="example-full-width">
        <input matInput placeholder="Meme url" required [(ngModel)]="panel.staticMemeUrl" name="url">
    </mat-form-field>
`, styleUrls: ['./../../configuration/panel.shared.css']})
export class RandomMemePanelConfigComponent implements OnInit, IPanelConfigComponent<RandomMemePanel> {

    createPanelUrl : string = "/api/Panel/CreateMemePanel";

    panel : RandomMemePanel;

    constructor(private panelsApi : PanelApiService) {}

    isValid() : boolean {
        return this.panel.staticMemeUrl != null && this.panel.staticMemeUrl != '';
    }

    setPanel(panel : any) {
        this.panel = panel;
    }
    postPanel(edit : boolean) : Observable<RandomMemePanel> {
        return this.panelsApi.saveOrUpdate(edit, this.panel)
        .map(response => {console.log(response); return response});
    }

    ngOnInit() {

    }

}
