import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {Project, SupportedProviders} from '../../projects-manager/project';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Router, ActivatedRoute} from '@angular/router';
import { NotificationService, SnackBar, NotificationType } from '../../snackbar/notification.service';

@Component({
  selector: 'app-panel-projects',
  templateUrl: './panel-projects.component.html',
  styleUrls: ['./panel-projects.component.css', './../panel.shared.css']
})
export class PanelProjectsComponent implements OnInit {

  project = new Project(null, null, null, null, null, null);
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
              this.project = project;        
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
        if(this.editMode == true){
          this.notificationService.addNotification('Udalo sie edytowac projekt', NotificationType.Success);
        }else{
          this.notificationService.addNotification('Udalo sie dodac projekt', NotificationType.Success);
        }
      }, err => {
        if(this.editMode == true){
          this.notificationService.addNotification('Nie udalo sie edytowac projektu', NotificationType.Failure);
        }else{
          this.notificationService.addNotification('Nie udalo sie dodac projektu', NotificationType.Failure);
        }
        console.error('Error msg: ', err);
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
