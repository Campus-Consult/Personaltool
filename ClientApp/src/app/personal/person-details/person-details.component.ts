import { Component, OnInit, Input } from '@angular/core';
import { Person, MemberStatus, Position, CareerLevel} from 'src/app/models/person.class';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.scss'],
})
export class PersonDetailsComponent implements OnInit {
  @Input()
  personDetails: Person;

  public displayedName: string;


  // Expansionpanels Desc
  public currentMemberState: MemberStatus;
  public currentCareerLevel: CareerLevel;
  public currentPositions: Position;


  constructor() {}

  ngOnInit(): void {
    this.displayedName =
      this.personDetails.firstName + ' ' + this.personDetails.lastName;

    // TODO AAM: Propertys could undefinded and need get from Backend 
    this.currentMemberState = this.personDetails.personsMemberStatus.find((val)=>!val.end).memberStatus;
    this.currentCareerLevel = this.personDetails.personsCareerLevels.find((val)=>!val.end).careerLevel;
    this.currentPositions = this.personDetails.personsPositions.find((val)=>!val.end).position;
  }
}
