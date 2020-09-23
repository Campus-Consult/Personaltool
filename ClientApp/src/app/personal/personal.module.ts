import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonalComponent } from './personal.component';
import { PersonEditComponent } from './person-edit/person-edit.component';
import { PersonDetailsComponent } from './person-details/person-details.component';
import { PersonListComponent } from './person-list/person-list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import {MatExpansionModule} from '@angular/material/expansion';
import { ComponentsModule } from '../components/components.module';
import { PersonalDataComponent } from './person-details/personal-data/personal-data.component';

@NgModule({
  declarations: [
    PersonalComponent,
    PersonEditComponent,
    PersonDetailsComponent,
    PersonListComponent,
    PersonalDataComponent
  ],
  exports: [PersonalComponent],
  imports: [
    CommonModule,
    ComponentsModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatGridListModule,
    MatCardModule,
    MatExpansionModule
  ],
})
export class PersonalModule {}
