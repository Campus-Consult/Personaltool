import { Component, OnInit, Inject } from '@angular/core';
import { PositionApiService, Position, PositionEdit } from '../services/positionapi.service';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.scss']
})
export class PositionComponent implements OnInit {

  detailPosition: Position | null = null;
  allPositions: Position[];
  loading = true;

  constructor(private positionApiService: PositionApiService, public dialog: MatDialog) {

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
      alert('couldn\'t load position: ' + error);
    });
  }

  public edit(position: Position): void {
    const posEdit: PositionEdit = {
      name: position.name,
      shortName: position.shortName,
      isActive: position.isActive,
      positionID: position.positionID,
    };
    const dialogRef = this.dialog.open(PositionEditCialogComponent, {
      width: '400px',
      data: posEdit,
    });

    dialogRef.afterClosed().subscribe((result: undefined | PositionEdit) => {
      if (!!result) {
        const changedPosition = this.allPositions.find(pos => pos.positionID === result.positionID);
        if (!changedPosition) {
          console.log('Error, changed non existsing position??');
        } else {
          changedPosition.isActive = result.isActive;
          changedPosition.name = result.name;
          changedPosition.shortName = result.shortName;
        }
      }
    });
  }

  public closeDetails(): void {
    this.detailPosition = null;
  }

}

@Component({
  selector: 'app-position-edit-dialog',
  templateUrl: 'position-edit-dialog.html',
})
export class PositionEditCialogComponent {

  // track state
  public savingBeforeClose = false;

  constructor(
    private positionApiService: PositionApiService,
    public dialogRef: MatDialogRef<PositionEditCialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PositionEdit) {
      this.savingBeforeClose = false;
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.savingBeforeClose) return;
    console.log('saving...');
    this.savingBeforeClose = true;
    this.dialogRef.disableClose = true;
    this.positionApiService.update(this.data).subscribe(val => {
      this.dialogRef.close(this.data);
    }, err => {
      // how do we want to handle errors? Notification top right?
      console.log(err);
      this.dialogRef.close();
    });
  }

}
