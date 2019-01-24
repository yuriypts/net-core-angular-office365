import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReportHistoryModel } from 'src/app/shared/models/reportHistory.model';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  constructor() { }
  @Input() historyList: ReportHistoryModel[] = [];
  @Output() closeEvent = new EventEmitter<any>();

  ngOnInit() {
  }
  close() {
    this.closeEvent.emit();
  }
}
