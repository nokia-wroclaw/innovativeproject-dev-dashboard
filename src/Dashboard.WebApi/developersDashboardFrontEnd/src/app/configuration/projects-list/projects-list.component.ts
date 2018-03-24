import {Component, OnInit} from '@angular/core';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Project} from '../../projects-manager/project';

@Component({selector: 'app-projects-list', templateUrl: './projects-list.component.html', styleUrls: ['./projects-list.component.css']})
export class ProjectsListComponent implements OnInit {

  constructor(private projectApiService : ProjectsApiService) {}

  projects : Project[] = [];

  ngOnInit() {
    this.loadProjects();
  }

  private loadProjects() {
    this
      .projectApiService
      .getProjects()
      .subscribe(projects => this.projects = projects)
  }
}
