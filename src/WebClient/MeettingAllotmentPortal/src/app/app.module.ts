import { APP_INITIALIZER, NgModule, Provider } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

//Modules
import { ApiModule as MeetingApiModule } from '@proxy/Sgmarkets-meeting/api.module';

import { SharedModule } from '@shared/shared.module'
import { HomeModule } from '../modules/home/home.module'

//Components
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from 'src/modules/home/home.component';
import { ErrorIntercept } from 'src/core/interceptors/ErrorIntercept';
import { ApiConfiguration } from '@proxy/Sgmarkets-meeting/api-configuration';
import { environment } from 'src/environments/environment';

export const routes: Routes = [
  { path: '', redirectTo: '/meeting', pathMatch: 'full' },
  { path: 'meeting', component: HomeComponent }
];


export function initApiConfiguration(config: ApiConfiguration): Function {
  return () => {
    config.rootUrl = environment.apiUrl
  };
}
export const INIT_API_CONFIGURATION: Provider = {
  provide: APP_INITIALIZER,
  useFactory: initApiConfiguration,
  deps: [ApiConfiguration],
  multi: true
};

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    MeetingApiModule,
    RouterModule.forRoot(routes),
    // ProxyModule,
    HomeModule,
    SharedModule,
    BrowserAnimationsModule
  ],
  providers: [
    INIT_API_CONFIGURATION,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorIntercept,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
