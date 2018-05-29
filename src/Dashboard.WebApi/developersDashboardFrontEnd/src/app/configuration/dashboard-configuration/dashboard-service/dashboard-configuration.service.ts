import { Injectable } from '@angular/core';

@Injectable()
export class DashboardConfigurationService {

  hidePanelTitle: boolean = false;
  

  constructor() { }

  setPanelTitleStatus(panelTitleStatus: boolean){
    this.hidePanelTitle = panelTitleStatus;
  }
  getPanelTitleStatus(){
    return this.hidePanelTitle;
  }
}
