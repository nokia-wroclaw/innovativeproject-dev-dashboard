import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {RandomMemePanel} from "./random-meme-panel";
import {Observable} from "rxjs/Observable";
import { PanelApiService } from '../../panel-manager/service/api/panel-api.service';

@Component({template: ``})
export class RandomMemePanelConfigComponent implements OnInit, IPanelConfigComponent<RandomMemePanel> {

    panel : RandomMemePanel;

    constructor(private panelsApi : PanelApiService) {}

    isValid() : boolean {
        return true;
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
