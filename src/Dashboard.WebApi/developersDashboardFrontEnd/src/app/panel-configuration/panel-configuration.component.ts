import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute} from "@angular/router";

@Component({selector: 'app-panel-configuration', templateUrl: './panel-configuration.component.html', styleUrls: ['./panel-configuration.component.css']})
export class PanelConfigurationComponent implements OnInit,
OnDestroy {

  constructor(private route : ActivatedRoute) {}

  panelId : number;

  private routeParamsSubscription;

  ngOnInit() {
    this.routeParamsSubscription = this
      .route
      .params
      .subscribe(params => {
        this.panelId = params['id'];
      });

    this.typescriptPlayground();
  }

  ngOnDestroy() {
    this
      .routeParamsSubscription
      .unsubscribe();
  }

  typescriptPlayground() {

    let dog : any = {
      name: 'xd',
      flies: 5
    }
    
    let a = <Dog> dog;

    console.log(a);
    
  }

}

class Animal {
  name : string;
}

class Dog extends Animal {
  legs : number;
}
