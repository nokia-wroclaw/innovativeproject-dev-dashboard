import {Component, OnInit, Input} from '@angular/core';
import {PanelDataService} from './service/panel-data.service';
import {PanelType} from "../../panel-manager/panel";
import {PanelTypeMapperService} from "../../panel-manager/service/panel-type-mapper/panel-type-mapper.service";
import {ProjectsManagerService} from "../../projects-manager/projects-manager.service";
import {Project} from "../../projects-manager/project";

@Component({
  selector: 'app-panel-create',
  templateUrl: './panel-create.component.html',
  styleUrls: ['./panel-create.component.css', './../panel.shared.css']
})
export class PanelCreateComponent implements OnInit {

  constructor(public panelDataService : PanelDataService, private panelTypeMapper : PanelTypeMapperService, private projectsManager : ProjectsManagerService) {
    this.selectedColumns = this.panelDataService.columnNumber;
  }

  projects : Project[] = [];

  columns = [0, 1, 2];

  types = [];

  selectedPlugins : any;
  selectedColumns : number;
  selectedPanels : any;

  ngOnInit() {
    this.loadPossiblePanelTypes();
    this.loadPossibleProjects();
  }

  private loadPossibleProjects() {
    this
      .projectsManager
      .getProjects()
      .subscribe(projects => this.projects = projects);
  }

  private loadPossiblePanelTypes() {
    PanelType
      .getValues()
      .forEach(panelType => {
        this
          .types
          .push({
            value: panelType,
            viewValue: this
              .panelTypeMapper
              .getName(panelType)
          })
      })
  }

}
