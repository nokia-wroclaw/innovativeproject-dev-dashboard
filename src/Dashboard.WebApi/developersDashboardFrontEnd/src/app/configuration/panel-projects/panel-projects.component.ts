import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {Project, SupportedProviders} from '../../projects-manager/project';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Router, ActivatedRoute} from '@angular/router';
import { NotificationService, SnackBar, NotificationType } from '../../snackbar/notification.service';
import { failureMessages, FailureMessage, successMessages, SuccessMessage } from '../../snackbar/notification-messages';

@Component({
  selector: 'app-panel-projects',
  templateUrl: './panel-projects.component.html',
  styleUrls: ['./panel-projects.component.css', './../panel.shared.css']
})
export class PanelProjectsComponent implements OnInit {

  project : Project = new Project();
  
  projectCiDataUpdateIntervalMinutes: number; 
  dataProviderNames = new SupportedProviders(undefined);
  private routeParamsSubscription;
  editMode : boolean = false;
  CiDataUpdateCronExpression : boolean = false;

  constructor(private projectApiService: ProjectsApiService,private route : ActivatedRoute, private notificationService: NotificationService, private router : Router, private zone : NgZone) {
  }
  ngOnDestroy() {
    this
      .routeParamsSubscription
      .unsubscribe();
  }
  ngOnInit() {
  this.routeParamsSubscription = this
      .route
      .params
      .subscribe(params => {
        if (params['id'] != null) {
          this.editMode = true;

          this
            .projectApiService
            .getProject(params['id'])
            .subscribe(project => {
              this.project = new Project(project.id, project.projectTitle, project.apiHostUrl, project.apiProjectId, project.apiAuthenticationToken, project.dataProviderName, project.ciDataUpdateCronExpression);        
            });
        } else {
          this.editMode = false;
        }
      });

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
      .saveOrUpdate(this.editMode, this.project)//tu zmienic
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
  onDelete() {
    if(this.editMode) {
      this.projectApiService.deleteProject(this.project).subscribe(response => {
        console.log(response);
        this.router.navigate(['admin/listOfProjects']);
      });
    }
  }
}
