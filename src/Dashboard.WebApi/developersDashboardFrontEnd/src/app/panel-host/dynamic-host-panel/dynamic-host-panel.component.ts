import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-dynamic-host-panel',
  templateUrl: './dynamic-host-panel.component.html',
  styleUrls: ['./dynamic-host-panel.component.css']
})
export class DynamicHostPanelComponent implements OnInit {

  @Input()
  adminMode: Boolean = true;

  @Input()
  panelId: number;

  @Input()
  title: string;
  
  constructor() { }

  ngOnInit() {
  }

}
