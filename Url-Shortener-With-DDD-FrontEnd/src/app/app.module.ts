import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {CommonModule} from "@angular/common";
import { HomeComponent } from './components/home/home.component';
import {RouterModule, RouterOutlet} from "@angular/router";
import {AppRoutes} from "./app.routes";
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import { UrlValidatorDirective } from './directives/url-validator.directive';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    UrlValidatorDirective,

  ],
  imports: [
    BrowserModule,
    CommonModule,
    RouterOutlet,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
