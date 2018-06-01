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

  public hideTopBar : boolean;
  public hidePanelTitle : boolean;

  ngOnInit() {
    this.hidePanelTitle = this.dashboardConfigurationService.getPanelTitleStatus();
    this.hideTopBar = this.dashboardConfigurationService.getHideTopBar();
  }
  public onSubmit(){
    this.dashboardConfigurationService.setPanelTitleStatus(this.hidePanelTitle);
    this.dashboardConfigurationService.setHideTopBar(this.hideTopBar);
    this.router.navigate(['/']);
    
    }
  }
 

