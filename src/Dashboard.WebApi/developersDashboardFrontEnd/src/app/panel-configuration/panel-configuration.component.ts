import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

@Component({ selector: 'app-panel-configuration', templateUrl: './panel-configuration.component.html', styleUrls: ['./panel-configuration.component.css'] })
export class PanelConfigurationComponent implements OnInit,
  OnDestroy {

  constructor(private route: ActivatedRoute) { }

  panelId: number;

  private routeParamsSubscription;

  ngOnInit() {
    this.routeParamsSubscription = this
      .route
      .params
      .subscribe(params => {
        this.panelId = params['id'];
      });
  }

  ngOnDestroy() {
    this
      .routeParamsSubscription
      .unsubscribe();
  }
}
