import {Component, ViewChild, ElementRef} from '@angular/core';
import {AdminModeService} from "./dashboard/admin-mode-service/admin-mode.service";

@Component({selector: 'app-root', templateUrl: './app.component.html', styleUrls: ['./app.component.css']})
export class AppComponent {

  navOpened : boolean;

  title = 'Developers dashboard';

  constructor(private adminModeService : AdminModeService) {}

  onNavOpened(opened : boolean) {}

  onOpenedStart() {
    this.navOpened = true;
    this
      .adminModeService
      .setAdminMode(true);
  }

  onClosedStart() {
    this.navOpened = false;
    this
      .adminModeService
      .setAdminMode(false);

    // TODO find a way to scroll back to top on admin mode exit
  }

}
