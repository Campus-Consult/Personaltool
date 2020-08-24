import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonalComponent } from './personal.component';
import { PersonEditComponent } from './person-edit/person-edit.component';
import { PersonDetailsComponent } from './person-details/person-details.component';
import { PersonListComponent } from './person-list/person-list.component';



@NgModule({
  declarations: [PersonalComponent, PersonEditComponent, PersonDetailsComponent, PersonListComponent],
  exports: [PersonalComponent],
  imports: [
    CommonModule
  ]
})
export class PersonalModule { }
