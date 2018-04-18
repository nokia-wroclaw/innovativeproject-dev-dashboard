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
    widthHeightRatio: 1.0,
    responsiveOptions: [
      {
        breakpoint: 'sm',
        minWidth: 1024,
        lanes: 3,
        widthHeightRatio: 1.3,
      }, {
        breakpoint: 'md',
        minWidth: 1280,
        widthHeightRatio: 1.3,
        lanes: 4 ,
      }, {
        breakpoint: 'lg',
        minWidth: 1400,
        lanes: 5,
        widthHeightRatio: 1.1,
      }, {
        breakpoint: 'xl',
        minWidth: 1800,
        lanes: 6,
        widthHeightRatio: 1.3,
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
