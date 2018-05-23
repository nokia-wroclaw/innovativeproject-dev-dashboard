import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {Project, SupportedProviders} from '../../projects-manager/project';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Router} from '@angular/router';
import { NotificationService, SnackBar, NotificationType } from '../../snackbar/notification.service';
import { failureMessages, FailureMessage, successMessages, SuccessMessage } from '../../snackbar/notification-messages';

@Component({
  selector: 'app-panel-projects',
  templateUrl: './panel-projects.component.html',
  styleUrls: ['./panel-projects.component.css', './../panel.shared.css']
})
export class PanelProjectsComponent implements OnInit {

  project = new Project('', '', '', '', '', '');
  projectCiDataUpdateIntervalMinutes: number;
  dataProviderNames = new SupportedProviders(undefined);

  constructor(private projectApiService: ProjectsApiService, private notificationService: NotificationService, private router : Router, private zone : NgZone,) {
  }

  ngOnInit() {
   this.getProviderForProject();
  }
  
  addProject() {
    console.log(this.project);
    if (!this.project) {
      return;
    }

   if (this.projectCiDataUpdateIntervalMinutes == undefined){
     this.projectCiDataUpdateIntervalMinutes = 4;
    }

    this.project.setCiDataUpdateCronExpression(this.projectCiDataUpdateIntervalMinutes);

    this
      .projectApiService
      .addProject(this.project)
      .subscribe(project => {
        this.project = project;
       this.notificationService.addNotification(successMessages.get(SuccessMessage.PROJECT_SAVED), NotificationType.Success);
      }, err => {
        this.notificationService.addNotification(failureMessages.get(FailureMessage.PROJECT_SAVED_FAILED) + ": " + err.statusText, NotificationType.Failure);
      }, () => {
        this
          .zone
          .run(() => this.router.navigate(['/admin/listOfProjects']))

      });
  }
  private getProviderForProject(){
   this.projectApiService.getSupportedProvidersForProjects().subscribe(res =>{
   
    this.dataProviderNames.data = res;
    console.log(this.dataProviderNames.data);
    }  );
    
  }
}
