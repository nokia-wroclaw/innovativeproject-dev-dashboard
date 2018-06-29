import {Component, OnInit} from '@angular/core';
import {IPanelConfigComponent} from "../panel.component";
import {Observable} from "rxjs/Observable";
import {StaticBranchPanel} from "./static-branch";
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {FormControl} from '@angular/forms';
import { PanelApiService } from '../../panel-manager/service/api/panel-api.service';

@Component({template: `
    <mat-form-field class="example-full-width">
        <input matInput placeholder="Branch name" required [(ngModel)]="panel.staticBranchName" name="token" [formControl]="branchNameControl" [matAutocomplete]="auto">
    </mat-form-field>
    <mat-autocomplete #auto="matAutocomplete">
        <mat-option *ngFor="let option of branchNameAutocompleteOptions" [value]="option">
            {{ option }}
        </mat-option>
    </mat-autocomplete>
`, styleUrls: ['./../../configuration/panel.shared.css']})
export class StaticBranchPanelConfigComponent implements OnInit,
IPanelConfigComponent < StaticBranchPanel > {

    panel : StaticBranchPanel;

    branchNameControl : FormControl = new FormControl();

    branchNameAutocompleteOptions : string[];

    constructor(private panelApi : PanelApiService, private projectsApi : ProjectsApiService) {}

    isValid() : boolean {
        return this.panel.staticBranchName != null && this.panel.staticBranchName != '';
    }

    setPanel(panel : any) {
        this.panel = panel;
    }
    postPanel(edit : boolean) : Observable < StaticBranchPanel > {
        return this.panelApi.saveOrUpdate(edit, this.panel).map(response => {
            console.log(response);
          return response;
        });
    }

    ngOnInit() {
        this
            .branchNameControl
            .valueChanges
            .filter(val => val != null)
            .filter(val => val.length >= 3)
            .filter(val => this.panel.projectId != null)
            .subscribe(val => {
                this
                    .projectsApi
                    .getMatchingBranches(this.panel.projectId, val)
                    .filter(matchingBranches => matchingBranches != null)
                    .map(matchingBranches => matchingBranches.sort((a, b) => a.length - b.length))
                    .subscribe(matchingBranches => this.branchNameAutocompleteOptions = matchingBranches);
            });
    }

}
