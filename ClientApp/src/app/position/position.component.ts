import { Component, OnInit, Inject } from '@angular/core';
import { PositionApiService, Position, PositionEdit, PersonAssignment, PositionHolder } from '../services/positionapi.service';
import { Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { startWith, switchMap, map } from 'rxjs/operators';
import * as moment from "moment";
import { AssignmentOption } from '../common/search-select/search-select.component';

@Component({
  selector: 'app-position',
  templateUrl: './position.component.html',
  styleUrls: ['./position.component.scss']
})
export class PositionComponent implements OnInit {

  allPositions: Position[];

  filteredPositions: Position[];
  searchTerm: string;
  activeFilter: 'all' | 'active' | 'deprecated';

  loading = true;

  constructor(private positionApiService: PositionApiService, public dialog: MatDialog) {

  }

  ngOnInit(): void {
    this.activeFilter = 'all';
    this.searchTerm = '';
    this.positionApiService.getAll().subscribe(pos => {
      this.allPositions = pos;
      this.loading = false;
      this.updateFiltering();
    });
  }

  public updateFiltering(): void {
    const searchTerm = this.searchTerm.toLowerCase();
    this.filteredPositions = this.allPositions.filter(p => {
      if (p.isActive && this.activeFilter === 'deprecated') { return false; }
      if (!p.isActive && this.activeFilter === 'active') { return false; }
      return p.name.toLowerCase().includes(searchTerm) || p.shortName.toLowerCase().includes(searchTerm);
    });
  }

  public edit(position: Position): void {
    const posEdit: PositionEdit = {
      name: position.name,
      shortName: position.shortName,
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
          changedPosition.name = result.name;
          changedPosition.shortName = result.shortName;
        }
      }
    });
  }

  public assign(position: Position): void {
    this.dialog.open(PositionAssignDialogComponent, {
      data: position,
    });
  }

  public dismiss(position: Position, person: PositionHolder): void {
    this.dialog.open(PositionDismissDialogComponent, {
      data: {
        position,
        person,
      },
    });
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
    if (this.savingBeforeClose) { return; }
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

@Component({
  selector: 'app-position-create-dialog',
  templateUrl: 'position-create-dialog.html',
})
export class PositionCreateDialogComponent {

  // track state
  public savingBeforeClose = false;
  public data: PositionEdit;

  constructor(
    private positionApiService: PositionApiService,
    public dialogRef: MatDialogRef<PositionEditCialogComponent>) {
      this.savingBeforeClose = false;
      this.data = {
        name: '',
        shortName: '',
        positionID: -1, // ignored by api anyways
      };
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.savingBeforeClose) { return; }
    console.log('saving...');
    this.savingBeforeClose = true;
    this.dialogRef.disableClose = true;
    this.positionApiService.create(this.data).subscribe(val => {
      this.dialogRef.close(this.data);
    }, err => {
      // how do we want to handle errors? Notification top right?
      console.log(err);
      this.dialogRef.close();
    });
  }
}

@Component({
  selector: 'app-position-assign-dialog',
  templateUrl: 'position-assign-dialog.html',
})
export class PositionAssignDialogComponent implements OnInit {

  // track state
  public savingBeforeClose = false;
  public assignSuggestions: AssignmentOption[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private positionApiService: PositionApiService,
    public dialogRef: MatDialogRef<PositionEditCialogComponent>,
    @Inject(MAT_DIALOG_DATA) public position: Position) {
      this.savingBeforeClose = false;
  }

  ngOnInit(): void {
    this.positionApiService.getAssignmentsSuggestion().subscribe(suggestions => {
      this.assignSuggestions = suggestions.map(s => {
        return {name: s.name, id: s.personID};
      });
    });
    this.assignDate.setValue(moment());
    this.assignForm.valueChanges.subscribe(v => {
      console.log(v);
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  assignForm = this.formBuilder.group({
    assignPerson: [true, Validators.required],
    assignDate: [null, Validators.required],
  });

  get assignPerson() {
    return this.assignForm.get('assignPerson');
  }

  get assignDate() {
    return this.assignForm.get('assignDate');
  }

  onSubmit(): void {
    if (this.savingBeforeClose) { return; }
    // triggers errors
    this.assignForm.markAllAsTouched();
    if (this.assignForm.invalid) { return; }
    console.log('saving...');
    this.savingBeforeClose = true;
    this.dialogRef.disableClose = true;

    const personsToAssign: number[] = [];
    const personID = this.assignPerson.value.id;
    personsToAssign.push(personID);
    this.positionApiService.assign(this.position.positionID, this.assignDate.value.toJSON(), personsToAssign).subscribe(val => {
      this.dialogRef.close();
    }, err => {
      // how do we want to handle errors? Notification top right?
      console.log(err);
      this.dialogRef.close();
    });
  }
}

@Component({
  selector: 'app-position-dismiss-dialog',
  templateUrl: 'position-dismiss-dialog.html',
})
export class PositionDismissDialogComponent implements OnInit{
  // track state
  public savingBeforeClose = false;

  constructor(
    private formBuilder: FormBuilder,
    private positionApiService: PositionApiService,
    public dialogRef: MatDialogRef<PositionDismissDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {position: Position, person: PositionHolder}) {
      this.savingBeforeClose = false;
  }

  ngOnInit(): void {
    this.date.setValue(moment());
    this.date.valueChanges.subscribe(v => {
      console.log(v);
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  dismissForm = this.formBuilder.group({
    date: [null, Validators.required],
  });

  get date() {
    return this.dismissForm.get('date');
  }

  onSubmit(): void {
    if (this.savingBeforeClose) { return; }
    // triggers errors
    this.dismissForm.markAllAsTouched();
    if (this.dismissForm.invalid) { return; }
    console.log('saving...');
    this.savingBeforeClose = true;
    this.dialogRef.disableClose = true;

    const personsToDismiss: number[] = [this.data.person.personID];
    this.positionApiService.dismiss(this.data.position.positionID, this.date.value.toJSON(), personsToDismiss).subscribe(val => {
      this.dialogRef.close();
    }, err => {
      // how do we want to handle errors? Notification top right?
      console.log(err);
      this.dialogRef.close();
    });
  }
}