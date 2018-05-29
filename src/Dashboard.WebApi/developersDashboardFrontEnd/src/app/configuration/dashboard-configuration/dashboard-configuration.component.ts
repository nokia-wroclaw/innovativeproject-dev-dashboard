import { Component, OnInit, NgZone } from '@angular/core';
import { DashboardConfigurationService } from './dashboard-service/dashboard-configuration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-configuration',
  templateUrl: './dashboard-configuration.component.html',
  styleUrls: ['./dashboard-configuration.component.css','./../panel.shared.css']
})
export class DashboardConfigurationComponent implements OnInit {

  constructor(private dashboardConfigurationService: DashboardConfigurationService, private router : Router) { }

  public hidePanelTitle : boolean;

  ngOnInit() {
    this.hidePanelTitle = this.dashboardConfigurationService.getPanelTitleStatus();
  }
  public onSubmit(){
    this.dashboardConfigurationService.setPanelTitleStatus(this.hidePanelTitle);
    this.router.navigate(['/']);
    

    }
  }
 

