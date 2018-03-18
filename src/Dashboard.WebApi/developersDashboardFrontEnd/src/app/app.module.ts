import {BrowserModule} from '@angular/platform-browser';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NavbarComponent} from './navbar/navbar.component';
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

import {AppComponent} from './app.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {PanelConfigurationComponent} from './panel-configuration/panel-configuration.component';
import {PanelCreateComponent} from './panel-create/panel-create.component';
import {HostDirective} from "./panel-host/host.directive";
import {StaticHostPanelComponent} from './panel-host/static-host-panel/static-host-panel.component';
import {DynamicHostPanelComponent} from './panel-host/dynamic-host-panel/dynamic-host-panel.component';
import {PanelManagerService} from "./panel-manager/service/panel-manager.service";
import {CdkTableModule} from '@angular/cdk/table';
import {PanelDataService} from './panel-create/service/panel-data.service';
import {PanelApiService} from "./panel-manager/service/api/panel-api.service";
import {PanelComponent} from "./panels/panel.component";
import {EmptyPanelComponent} from "./panels/empty-panel/empty-panel.component";
import {PanelTypeMapperService} from "./panel-manager/service/panel-type-mapper/panel-type-mapper.service";

const appRoutes : Routes = [
  {
    path: '',
    component: DashboardComponent,
    data: {
      adminMode: false
    }
  }, {
    path: 'admin',
    component: DashboardComponent,
    data: {
      adminMode: true
    }
  }, {
    path: 'admin/create',
    component: PanelCreateComponent
  }, {
    path: 'admin/:id',
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
    HostDirective,
    StaticHostPanelComponent,
    DynamicHostPanelComponent,
    NavbarComponent,
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
    HttpClientModule
  ],
  providers: [
    PanelManagerService, PanelDataService, PanelApiService, PanelTypeMapperService
  ],
  bootstrap: [AppComponent],
  entryComponents: [EmptyPanelComponent]
})
export class AppModule {}

platformBrowserDynamic().bootstrapModule(AppModule);