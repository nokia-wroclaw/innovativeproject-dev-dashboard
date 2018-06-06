import {Component, OnInit, Input} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {RandomMemePanel} from "./random-meme-panel";
import {RandomMemeService} from "./random-meme.service";
import { Observable } from 'rxjs';

@Component({templateUrl: './random-meme-panel.component.html'})
export class RandomMemePanelComponent implements OnInit, IPanelComponent<RandomMemePanel> {

  panel : RandomMemePanel;

  interval : Observable<any>;

  setPanel(panel : RandomMemePanel) {
    this.panel = panel;
  }
  
  constructor(private randomMemeService : RandomMemeService) {}

  ngOnInit() {

    this.interval = Observable.interval(60 * 1000); 

    this.interval.subscribe(next => {
      this.randomMemeService.refreshPanel(this.panel.id).filter(panel => panel != null).subscribe(panel => {
        this.panel = panel;  
      });
    });
      
  }

}
