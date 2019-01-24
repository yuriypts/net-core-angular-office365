import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReportUpdateValue } from 'src/app/shared/models/reportUpdateValue.model';
import { NgForm } from '@angular/forms';
import { LabelType } from 'src/app/shared/models/reportLabel.model';

@Component({
  selector: 'app-update-report-values',
  templateUrl: './update-report-values.component.html',
  styleUrls: ['./update-report-values.component.scss']
})
export class UpdateReportValuesComponent implements OnInit {

  constructor() { }
  @Input() values: ReportUpdateValue[] = [];
  @Input() isView = false;
  @Input() submitTitle = 'Generate Report';
  @Output() submitClicked = new EventEmitter<ReportUpdateValue[]>();
  ngOnInit() {
  }
  getType(type: number) {
    switch (type) {
      case LabelType.String:
        return 'text';
      case LabelType.Int:
        return 'number';
      case LabelType.Bool:
        return 'checkbox';
      case LabelType.Date:
        return 'date';
      default:
        return 'text';
    }
  }
  onSubmit(form: NgForm) {
    const { ...data } = form.value;
    const updatedValues: ReportUpdateValue[] = [];
    Object.keys(data).filter(item => {
      const value: ReportUpdateValue = this.values.find(x => x.id === +item);
      value.value = data[item];
      updatedValues.push(value);
    });
    this.submitClicked.emit(updatedValues);
  }
}
