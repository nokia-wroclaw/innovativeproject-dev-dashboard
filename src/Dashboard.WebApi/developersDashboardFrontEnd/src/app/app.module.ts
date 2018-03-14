import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {MatButtonModule, MatIconModule, MatCardModule} from '@angular/material';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule, Routes} from '@angular/router';

import {AppComponent} from './app.component';
import {HelloWorldService} from './hello-world/hello-world.service';
import {DashboardComponent} from './dashboard/dashboard.component';
import {DashboardAdminComponent} from './dashboard-admin/dashboard-admin.component';
import {PanelConfigurationComponent} from './panel-configuration/panel-configuration.component';
import {PanelCreateComponent} from './panel-create/panel-create.component';

const appRoutes : Routes = [
  {
    path: '',
    component: DashboardComponent
  }, {
    path: 'admin',
    component: DashboardAdminComponent
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
    AppComponent, DashboardComponent, DashboardAdminComponent, PanelConfigurationComponent, PanelCreateComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes, {enableTracing: true}),
    BrowserModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    HttpClientModule
  ],
  providers: [HelloWorldService],
  bootstrap: [AppComponent]
})
export class AppModule {}