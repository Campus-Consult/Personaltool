import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeDe from "@angular/common/locales/de";

import { AuthUserService } from '../services/authuser.service';
import { PositionApiService } from '../services/positionapi.service';

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
import { SearchSelectComponent } from '../common/search-select/search-select.component';

import { PositionComponent, PositionAssignDialogComponent, PositionDismissDialogComponent, PositionCreateDialogComponent, PositionEditCialogComponent } from "./position.component";

@NgModule({
  declarations: [
    PositionComponent,
    PositionEditCialogComponent,
    PositionAssignDialogComponent,
    PositionDismissDialogComponent,
    PositionCreateDialogComponent,
    SearchSelectComponent,
  ],
  exports: [PositionComponent],
  imports: [
    BrowserModule,
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
})
export class PositionModule {}