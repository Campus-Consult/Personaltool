import { Component, OnInit } from '@angular/core';
import { Person, Gender } from '../models/person.class';

@Component({
  selector: 'app-personal',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent implements OnInit {
  public personalTableData: PersonListItem[];

  public searchValue= '';

  public selectedPerson: PersonListItem;

  constructor() {}

  ngOnInit(): void {
    this.personalTableData = new Array<PersonListItem>();
    for (let index = 0; index < 10; index++) {
      this.personalTableData.push({
        personID: index,
        firstName: 'Test' + index,
        lastName: 'Subject' + index,
        personsMemberStatus: 'MembverStautus',
        personsCareerLevel: 'Careerlevel',
        personsPosition: 'personsPositions'

      });      
    }
    setTimeout(() => {
      console.log(this.searchValue);
    }, 20000);

  }

  createPerson() {}

  changeDisplayedPerson(persId: number){
    this.selectedPerson = this.personalTableData.find((val)=>val.personID === persId);
  }

}

export interface PersonListItem{
  personID: number
  firstName: string;
  lastName: string;
  personsMemberStatus: string;
  personsCareerLevel: string;
  personsPosition: string;
}



