import {Component, OnInit, Input} from '@angular/core';
import './../panel.component';
import {IPanelComponent} from "./../panel.component";
import {RandomMemePanel} from "./random-meme-panel";
import {RandomMemeService} from "./random-meme.service";

@Component({templateUrl: './random-meme-panel.component.html'})
export class RandomMemePanelComponent implements OnInit, IPanelComponent<RandomMemePanel> {

  panel : RandomMemePanel;

  setPanel(panel : RandomMemePanel) {
    this.panel = panel;
  }
  
  constructor(private randomMemeService : RandomMemeService) {}

  ngOnInit() {
    this
      .randomMemeService
      .sayHelloWorld();
  }

}
