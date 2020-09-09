import { Component, OnInit } from '@angular/core';
import { Person, Gender } from '../models/person.class';

@Component({
  selector: 'app-personal',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.scss'],
})
export class PersonalComponent implements OnInit {
  public personalTableData: PersonTableData[];

  public searchValue= '';

  public selectedPerson: Person;

  constructor() {}

  ngOnInit(): void {
    this.personalTableData = new Array<PersonTableData>();
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

  getPersonDetails(personID: number): Person{
    const personList = new Array<Person>();
    for (let index = 0; index < 10; index++) {
      
      personList.push({
        personID: index,
        firstName: 'Test' + index,
        lastName: 'Subject' + index,

        birthdate: new Date(index),

        gender: index%2===0? Gender.MALE: Gender.FEMALE,
      
        emailPrivate: 'testMail@webkitCancelAnimationFrame.de',
    
        emailAssociaton: 'testMail@webkitCancelAnimationFrame.de',
      
        mobilePrivate: "+49123456789",
      
        /**  */
        adressStreet: 'Stra0enweg',
      
        /**  */
        adressNr: (index + 56).toString(),
      
        /**  */
        adressCity: 'paderborn',
      
        /**  */
        personsMemberStatus: [{
          personID: index,
          personsMemberStatusID: index,
          memberStatusID: index,
          begin: new Date()
        }],
      
        /**  */
        personsCareerLevels: [{
          personID: index,
          personsCareerLevelID: index,
          careerLevelID: index,
          begin: new Date()
        }],
      
        /**  */
        personsPositions: [{
          personID: index,
          personPositionID: index,
          positionID: index,
          begin: new Date()
        }]
      })
    }

    return personList.find((val)=>val.personID === personID);
  }
}

export interface PersonTableData{
  personID: number
  firstName: string;
  lastName: string;
  personsMemberStatus: string;
  personsCareerLevel: string;
  personsPosition: string;
}



