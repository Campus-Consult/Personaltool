import { Component, Input, OnInit } from '@angular/core';
import {
  CareerLevel,
  MemberStatus,
  Person,
  Position,
} from 'src/app/models/person.class';
import { HistoryData } from './history-expansion/history-expansion.component';

@Component({
  selector: 'app-history-panels',
  templateUrl: './history-panels.component.html',
  styleUrls: ['./history-panels.component.scss'],
})
export class HistoryPanelsComponent implements OnInit {
  @Input() personDetails: Person;

  // Expansionpanels Desc
  public currentMemberState: MemberStatus;
  public currentCareerLevel: CareerLevel;
  public currentPositions: Position;

  public memberStateHistory: HistoryData[];
  public careerLevelHistory: HistoryData[];
  public positionsHistory: HistoryData[];

  constructor() {}

  ngOnInit(): void {

    // TODO: transform into History data

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
}
