import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import {WelcomeComponent} from './welcome/welcome.component';
import {PageNotFoundComponent} from './error/page-not-found.component';
import { SharedModule } from 'app/shared/shared.module';
import {MdToolbarModule, MdButtonModule} from '@angular/material';
import { RoutingModule } from 'app/routing.module';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RoutingModule,
    HttpModule,
    SharedModule,
    MdToolbarModule,
    MdButtonModule
  ],
  providers: [
    {provide: 'API_URL', useValue: 'http://localhost:5000/api/'}

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
