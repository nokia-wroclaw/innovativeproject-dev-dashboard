import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class DashboardConfigurationService {


  hidePanelTitle: boolean = false;

  hideTopBar:  boolean = false;

  private subjectHideTopBar: Subject<boolean> = new Subject();
  
  constructor() { }

  setHideTopBar(hideTopBar: boolean) {
    this.hideTopBar = hideTopBar;
    this.subjectHideTopBar.next(hideTopBar);
  }

  getHideTopBarSubject() : Subject<boolean> {
    return this.subjectHideTopBar;
  }

  getHideTopBar(): boolean {
    return this.hideTopBar;
  }

  setPanelTitleStatus(panelTitleStatus: boolean){
    this.hidePanelTitle = panelTitleStatus;
  }
  getPanelTitleStatus(){
    return this.hidePanelTitle;
  }
}
