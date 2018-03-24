import {Component, OnInit} from '@angular/core';
import './../panel.component';
import {RandomMemeService} from "./random-meme-service/random-meme.service";

@Component({selector: 'app-random-meme-panel', templateUrl: './random-meme-panel.component.html', styleUrls: ['./random-meme-panel.component.css']})
export class RandomMemePanelComponent implements OnInit {

  // TODO This component should periodicaly (some interval) call for
  // randomMemeService to load another image Loaded image should be then displayed
  // (change src or something)

  constructor(private randomMemeService : RandomMemeService) {}

  ngOnInit() {
    this
      .randomMemeService
      .sayHelloWorld();
  }

}
