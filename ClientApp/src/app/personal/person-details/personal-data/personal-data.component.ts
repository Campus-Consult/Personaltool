import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  Input,
} from '@angular/core';
import { Person } from 'src/app/models/person.class';

@Component({
  selector: 'app-personal-data',
  templateUrl: './personal-data.component.html',
  styleUrls: ['./personal-data.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PersonalDataComponent implements OnInit {
  @Input()
  personDetails: Person;

  constructor() {}

  ngOnInit(): void {
    if (!this.personDetails) {
      this.personDetails = this.getEmptypersonDetails();
    }
  }
  getEmptypersonDetails(): Person {
    return {
      firstName: undefined,
      lastName: undefined,
      personID: undefined,
      personsCareerLevels: [],
      personsMemberStatus: [],
      personsPositions: [],
    };
  }
}
