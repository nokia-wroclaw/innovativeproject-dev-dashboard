import {Component, OnInit} from '@angular/core';
import {Project} from '../../projects-manager/project';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';

@Component({
  selector: 'app-panel-projects',
  templateUrl: './panel-projects.component.html',
  styleUrls: ['./panel-projects.component.css', './../panel.shared.css']
})
export class PanelProjectsComponent implements OnInit {

  constructor(private projectApiService : ProjectsApiService) {}

  project = new Project();

  ngOnInit() {
    this.addProject()
  }
  private addProject() {
    if (!this.project) {
      return;
    }
    this
      .projectApiService
      .addProject(this.project)
      .subscribe(project => this.project);
  }
}