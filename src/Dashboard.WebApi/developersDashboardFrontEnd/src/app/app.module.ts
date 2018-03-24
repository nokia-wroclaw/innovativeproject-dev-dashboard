import {BrowserModule} from '@angular/platform-browser';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule
} from '@angular/material';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';

import {GridsterModule} from 'angular2gridster';

import {AppComponent} from './app.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {PanelConfigurationComponent} from './configuration/panel-configuration/panel-configuration.component';
import {PanelCreateComponent} from './configuration/panel-create/panel-create.component';
import {HostDirective} from "./panel-host/host.directive";
import {StaticHostPanelComponent} from './panel-host/static-host-panel/static-host-panel.component';
import {DynamicHostPanelComponent} from './panel-host/dynamic-host-panel/dynamic-host-panel.component';
import {PanelManagerService} from "./panel-manager/service/panel-manager.service";
import {CdkTableModule} from '@angular/cdk/table';
import {PanelApiService} from "./panel-manager/service/api/panel-api.service";
import {PanelComponent} from "./panels/panel.component";
import {EmptyPanelComponent} from "./panels/empty-panel/empty-panel.component";
import {PanelTypeMapperService} from "./panel-manager/service/panel-type-mapper/panel-type-mapper.service";
import {ProjectsManagerService} from "./projects-manager/projects-manager.service";
import {ProjectsApiService} from "./projects-manager/api/projects-api.service";
import {AdminModeService} from "./dashboard/admin-mode-service/admin-mode.service";
import {PanelProjectsComponent} from './configuration/panel-projects/panel-projects.component';
import {PanelDataService} from './configuration/panel-create/service/panel-data.service';

const appRoutes : Routes = [
  {
    path: '',
    component: DashboardComponent
  }, {
    path: 'admin/create',
    component: PanelCreateComponent
  }, {
    path: 'admin/project',
    component: PanelProjectsComponent
  }, {
    path: 'admin/',
    component: PanelConfigurationComponent
  }, {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    PanelConfigurationComponent,
    PanelCreateComponent,
    PanelProjectsComponent,
    HostDirective,
    StaticHostPanelComponent,
    DynamicHostPanelComponent,
    EmptyPanelComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes, {enableTracing: true}),
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    CdkTableModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    BrowserAnimationsModule,
    HttpClientModule,
    GridsterModule
  ],
  providers: [
    PanelManagerService,
    PanelApiService,
    PanelTypeMapperService,
    ProjectsManagerService,
    PanelDataService,
    ProjectsApiService,
    AdminModeService
  ],
  bootstrap: [AppComponent],
  entryComponents: [EmptyPanelComponent]
})
export class AppModule {}

platformBrowserDynamic().bootstrapModule(AppModule);