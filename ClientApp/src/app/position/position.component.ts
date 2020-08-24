import { Component, OnInit } from '@angular/core';
import { PositionApiService, Position } from '../services/positionapi.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.scss']
})
export class PositionComponent implements OnInit {

  detailPosition: Position | null = null;
  allPositions: Position[];
  loading = true;

  constructor(private positionApiService: PositionApiService) {

  }

  ngOnInit(): void {
    this.positionApiService.getAll().subscribe(pos => {
      this.allPositions = pos;
      this.loading = false;
    });
  }

  public loadDetails(positionID: number): void {
    this.positionApiService.get(positionID).subscribe(pos => {
      this.detailPosition = pos;
      console.log(pos);
    }, error => {
      alert("couldn't load position: "+error);
    });
  }

  public closeDetails(): void {
    this.detailPosition = null;
  }

}
