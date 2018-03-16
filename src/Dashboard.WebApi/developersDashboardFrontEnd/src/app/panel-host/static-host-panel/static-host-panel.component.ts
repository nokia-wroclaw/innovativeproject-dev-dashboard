import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-static-host-panel',
  templateUrl: './static-host-panel.component.html',
  styleUrls: ['./static-host-panel.component.css']
})
export class StaticHostPanelComponent implements OnInit {

  @Input()
  adminMode: Boolean = true;

  @Input()
  panelId: number;

  @Input()
  tileTitle: String;

  constructor() { }

  ngOnInit() {
  }

}
