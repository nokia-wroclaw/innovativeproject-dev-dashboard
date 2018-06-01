import {Component, ViewChild, ElementRef} from '@angular/core';
import {AdminModeService} from "./dashboard/admin-mode-service/admin-mode.service";
import {MatSnackBar} from '@angular/material';
import {NotificationService, NotificationType} from './snackbar/notification.service';
import {DashboardConfigurationService} from './configuration/dashboard-configuration/dashboard-service/dashboard-configuration.service';

@Component({selector: 'app-root', templateUrl: './app.component.html', styleUrls: ['./app.component.css']})
export class AppComponent {

  navOpened : boolean;

  hideTopBar : boolean;

  title = 'Developers dashboard';

  constructor(private adminModeService : AdminModeService, public snackBar : MatSnackBar, private notificationService : NotificationService, private dashboardConfigurationService : DashboardConfigurationService) {

    this.hideTopBar = dashboardConfigurationService.getHideTopBar();
    this
      .dashboardConfigurationService
      .getHideTopBarSubject()
      .subscribe(hideTopBar => {
        this.hideTopBar = hideTopBar;
      });

    this
      .notificationService
      .subj_notification
      .subscribe(notification => {
        if (notification.type == NotificationType.Success) {
          snackBar.open(notification.message, '', {
            duration: 2000,
            verticalPosition: 'top',
            horizontalPosition: 'end'
          });
        } else {
          snackBar.open(notification.message, '', {
            duration: 4000,
            verticalPosition: 'top',
            horizontalPosition: 'end'
          });
        }
      });
  }

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
