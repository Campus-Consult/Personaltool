import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { PersonTableData } from '../personal.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.scss'],
})
export class PersonListComponent implements OnInit {
  @Input()
  personalData: PersonTableData[];

  @Input()
  displayedColumns: string[];

  @Output()
  itemSelectedEvent = new EventEmitter<number>();

  //dataSource = new MatTableDataSource<PersonTableData>(this.personalData);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor() {}

  ngOnInit(): void {
    if (!this.displayedColumns) {
      this.displayedColumns = [
        'firstName',
        'lastName',
        'personsMemberStatus',
        'personsCareerLevel',
        'personsPosition',
        'buttons',
      ];
    }
  }


  ngAfterViewInit() {
    // this.dataSource.paginator = this.paginator;
  }

  /** =============Person Action Methods ============== */

  edit(persID: number) {}

  details(persID: number) {}

  delete(persID: number) {}
}
