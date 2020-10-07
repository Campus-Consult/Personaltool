import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-history-expansion',
  templateUrl: './history-expansion.component.html',
  styleUrls: ['./history-expansion.component.scss']
})
export class HistoryExpansionComponent implements OnInit {

  @Input() title: string;

  @Input() historyData: HistoryData[]

  @Input() panelDescription?: string;

  @Output() openDetails: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();

  displayedColumns: string[] = ['name', 'startDate', 'endDate'];

  dataSource = new Array<HistoryTableSource>();

  constructor() { }

  ngOnInit(): void { 
    for (const historyItem of this.historyData) {
      this.convertToDataSourceItem(historyItem)
    }
  }

  convertToDataSourceItem(historyData: HistoryData): HistoryTableSource{
    const endDate = historyData.endDate? historyData.endDate.toDateString(): '-';
    return {id: historyData.id, name: historyData.name, startDate: historyData.startDate.toDateString(), endDate: endDate }
  }

  emitOpenDetails(event: MouseEvent){
    this.openDetails.emit(event);
  }
}

export interface HistoryTableSource{id: number, name: string, startDate: string; endDate: string }
export interface HistoryData {id: number, name: string, startDate: Date; endDate: Date }
