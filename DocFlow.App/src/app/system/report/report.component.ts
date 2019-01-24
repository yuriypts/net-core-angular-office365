import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { mergeMap } from 'rxjs/operators';

import { Message } from '../../shared/models/message.model';
import { ReportRequestModel } from '../../shared/models/report-request.model';
import { Subscription } from 'rxjs';
import { ReportLabelModel } from 'src/app/shared/models/reportLabel.model';
import { ReportTypeModel } from '../../shared/models/reportType.model';
import { ReportService } from '../services/report.service';
import { DriveTypeModel } from '../../shared/models/driveType.model';
import { LabelModel } from '../../shared/models/label.model';
import { AlertService } from 'src/app/shared/services/alert.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit, OnDestroy {

  reportLabelList: ReportLabelModel[] = [];
  reportTypeList: ReportTypeModel[] = [];

  reportTypeId: number;
  selectedReportType: ReportTypeModel;
  showReport = false;

  labelModel: LabelModel[] = [];

  form: FormGroup;
  message: Message;

  sub: Subscription;
  sub1: Subscription;

  isLoaded = false;

  constructor(private reportService: ReportService) {}
  ngOnInit() {
    this.message = new Message('danger', '');
    this.sub = this.reportService.getReportTypes()
    .subscribe((data: ReportTypeModel[]) => {
      this.reportTypeList = data;
      this.reportTypeId = this.reportTypeList[0].id;
      this.selectedReportType = this.reportTypeList[0];
        this.isLoaded = true;
      }, error => {
    });
  }
  setReportType() {
    this.selectedReportType = this.reportTypeList.find(x => x.id === +this.reportTypeId);
    this.showReport = false;
  }
  ngOnDestroy(): void {
    if (this.sub) {this.sub.unsubscribe(); }
    if (this.sub1) {this.sub1.unsubscribe(); }
 }
}
