import {Component, OnInit} from '@angular/core';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
import {Project, SupportedProviders} from '../../projects-manager/project';

@Component({selector: 'app-projects-list', templateUrl: './projects-list.component.html', styleUrls: ['./projects-list.component.css']})
export class ProjectsListComponent implements OnInit {

  constructor(private projectApiService : ProjectsApiService) {}

  projects : Project[] = [];

  gridsterOptions = {
    lanes: 4,
    direction: 'vertical',
    floating: true,
    dragAndDrop: false,
    responsiveView: true,
    resizable: false,
    useCSSTransforms: true,
    widthHeightRatio: 1.3,
    responsiveOptions: [
      {
        breakpoint: 'sm',
        lanes: 3
      }, {
        breakpoint: 'md',
        minWidth: 768,
        lanes: 4,
        dragAndDrop: true,
        resizable: true
      }, {
        breakpoint: 'lg',
        lanes: 6,
        dragAndDrop: true,
        resizable: true
      }, {
        breakpoint: 'xl',
        minWidth: 1800,
        lanes: 8,
        dragAndDrop: true,
        resizable: true
      }
    ]
  };
  

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
