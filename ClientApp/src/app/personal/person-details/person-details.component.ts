import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import {
  Person,
  MemberStatus,
  Position,
  CareerLevel,
} from 'src/app/models/person.class';
import { PersonTableData } from '../personal.component';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.scss'],
})
export class PersonDetailsComponent implements OnInit, OnChanges {
  @Input()
  personTabledDTO: PersonTableData;

  personDetails: Person;

  public displayedName: string;

  // Expansionpanels Desc
  public currentMemberState: MemberStatus;
  public currentCareerLevel: CareerLevel;
  public currentPositions: Position;

  constructor() {}

  ngOnInit(): void {
    this.displayedName = this.getFullName();

    // TODO AAM: Propertys could undefinded and need get from Backend
    this.currentMemberState = this.personDetails.personsMemberStatus.find(
      (val) => !val.end
    ).memberStatus;
    this.currentCareerLevel = this.personDetails.personsCareerLevels.find(
      (val) => !val.end
    ).careerLevel;
    this.currentPositions = this.personDetails.personsPositions.find(
      (val) => !val.end
    ).position;
  }

  ngOnChanges(changes: SimpleChanges): void {
    for (const propName in changes) {
      const chng = changes[propName];
      const prev = chng.previousValue;
      console.log(propName);
      
      if (propName==='personTabledDTO' && chng.currentValue) {
        this.personTabledDTO = chng.currentValue as PersonTableData;
      }
    }
    if (this.personTabledDTO) {
      this.setPersonData(this.personTabledDTO.personID);
      this.displayedName = this.getFullName();
    }
  }

  getPersondata(personId: number): string {
    // TODO: Backend request
    return ''
  }

  setPersonData(personId: number) {}

  getFullName(): string {
    return this.personTabledDTO.firstName + ' ' + this.personTabledDTO.lastName;
  }
}
