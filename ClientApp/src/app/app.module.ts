import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeDe from "@angular/common/locales/de";

import { AuthUserService } from './services/authuser.service';
import { PositionApiService } from './services/positionapi.service';
import { HomeComponent } from './home/home.component';
import { PrivacyComponent } from './privacy/privacy.component';
import { PositionComponent, PositionEditCialogComponent, PositionCreateDialogComponent, PositionAssignDialogComponent } from './position/position.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatRadioModule} from '@angular/material/radio';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from "@angular/material-moment-adapter";
import {MatAutocompleteModule} from '@angular/material/autocomplete';


registerLocaleData(localeDe);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PrivacyComponent,
    PositionComponent,
    PositionEditCialogComponent, // TODO: I'd rather not have all dialogues here, but angular is dumb and this works
    PositionCreateDialogComponent,
    PositionAssignDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,

    // BEGIN MATERIAL
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatInputModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatAutocompleteModule,
  ],
  providers: [
    AuthUserService,
    PositionApiService,
    {provide: LOCALE_ID, useValue: 'de-DE'},
    // workaround for dates, the date picker actually uses a datetime with 00:00 as time and with timezone this makes it
    // wrap around to the previou day
    {provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: {useUtc: true}},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
