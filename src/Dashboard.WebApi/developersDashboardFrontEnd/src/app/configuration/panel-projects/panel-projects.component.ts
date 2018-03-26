import {Component, OnInit, ViewChild, ElementRef, NgZone} from '@angular/core';
import {Project} from '../../projects-manager/project';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-panel-projects',
  templateUrl: './panel-projects.component.html',
  styleUrls: ['./panel-projects.component.css', './../panel.shared.css']
})
export class PanelProjectsComponent implements OnInit {

  project = new Project('', '', '', '', undefined);

  constructor(private projectApiService : ProjectsApiService, private router : Router, private zone : NgZone,) {}

  ngOnInit() {}
  
  addProject() {
    console.log('XD');
    if (!this.project) {
      return;
    }
    this
      .projectApiService
      .addProject(this.project)
      .subscribe(project => {
        this.project = project;
      }, err => {
        console.error('Error msg: ', err);
      }, () => {
        this
          .zone
          .run(() => this.router.navigate(['/admin/listOfProjects']))

      });
  }
}