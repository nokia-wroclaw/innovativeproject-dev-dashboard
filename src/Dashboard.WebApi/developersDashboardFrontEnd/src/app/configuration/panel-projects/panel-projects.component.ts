import {Component, OnInit, ViewChild, ElementRef} from '@angular/core';
import {Project} from '../../projects-manager/project';
import {ProjectsApiService} from '../../projects-manager/api/projects-api.service';
@Component({
  selector: 'app-panel-projects',
  templateUrl: './panel-projects.component.html',
  styleUrls: ['./panel-projects.component.css', './../panel.shared.css']
})
export class PanelProjectsComponent implements OnInit {

  project = new Project('', '', '', '', undefined);

  constructor(private projectApiService : ProjectsApiService) {}

  ngOnInit() {}
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