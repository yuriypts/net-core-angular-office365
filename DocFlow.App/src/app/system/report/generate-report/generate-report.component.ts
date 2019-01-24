import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ReportTypeModel } from 'src/app/shared/models/reportType.model';
import { Subscription, SubscriptionLike } from 'rxjs';
import { ReportService } from '../../services/report.service';
import { NgForm } from '@angular/forms';
import { ReportLabelModel, LabelType } from 'src/app/shared/models/reportLabel.model';
import { LabelModel } from 'src/app/shared/models/label.model';
import { ReportRequestModel } from 'src/app/shared/models/report-request.model';
import { Router } from '@angular/router';
import { ReportUpdateValue } from 'src/app/shared/models/reportUpdateValue.model';
import { ToSentenceCasePipe } from 'src/app/shared/pipes/toSentenceCase.pipe';

@Component({
  selector: 'app-generate-report',
  templateUrl: './generate-report.component.html',
  styleUrls: ['./generate-report.component.scss']
})
export class GenerateReportComponent implements OnInit, OnDestroy {

  constructor(private reportService: ReportService, private router: Router, private toSentenceCasePipe: ToSentenceCasePipe) { }

  @Input() reportType: ReportTypeModel;
  reportValueList: ReportUpdateValue[] = [];
  sub: Subscription;
  sub1: Subscription;
  isLoaded = false;
  isUpdated = true;

  ngOnInit() {
    this.sub = this.reportService.getReportLabels(this.reportType.id)
    .subscribe((data: ReportLabelModel[]) => {
        this.reportValueList = data.map((item) => {
          const label = this.toSentenceCasePipe.transform(item.name);
          return new ReportUpdateValue(item.id, item.name, label, item.type);
        });
        this.isLoaded = true;
      }, error => {
    });
  }
  createReport(reportValueList: ReportUpdateValue[]) {
    const labelModel = reportValueList.map((item) => {
      return new LabelModel(item.id, item.key, item.value);
    });
    const model = new ReportRequestModel(this.reportType.name, this.reportType.id, labelModel);
    this.isUpdated = false;
    this.sub1 = this.reportService.generateReport(model).subscribe(() => {
      this.router.navigate(['reports']);
    }, error => {
      this.isUpdated = true;
    });
  }
  ngOnDestroy() {
    if (this.sub) {this.sub.unsubscribe(); }
    if (this.sub1) {this.sub1.unsubscribe(); }

  }
}
