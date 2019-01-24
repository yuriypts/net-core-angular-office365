import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';

import { ReportService } from '../../services/report.service';
import { ReportModel } from '../../../shared/models/report.model';
import { UpdateRequestModel } from '../../../shared/models/update-request.model';
import { LabelModel } from '../../../shared/models/label.model';
import { ReportUpdateValue } from 'src/app/shared/models/reportUpdateValue.model';
import { ToSentenceCasePipe } from 'src/app/shared/pipes/toSentenceCase.pipe';
import { ReportHistoryModel } from 'src/app/shared/models/reportHistory.model';

@Component({
  selector: 'app-report-edit',
  templateUrl: './report-edit.component.html',
  styleUrls: ['./report-edit.component.scss']
})
export class ReportEditComponent implements OnInit, OnDestroy {

  report: ReportModel;
  reportId: number;
  isView = true;
  reportValueList: ReportUpdateValue[] = [];
  historyList: ReportHistoryModel[] = [];

  sub: Subscription;
  sub1: Subscription;
  sub2: Subscription;

  isLoaded = false;
  isUpdated = true;
  showHistory = false;

  constructor(
    private router: Router,
    private reportService: ReportService,
    private activatedRoute: ActivatedRoute,
    private toSentenceCasePipe: ToSentenceCasePipe
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: any) => {
      const { reportId, mode } = params;

      if (reportId) {
        this.reportId = reportId;
      }
      if (mode === 'edit') {
        this.isView = false;
      }
    });

    this.sub = this.reportService.getReport(this.reportId).subscribe((data: ReportModel) => {
      this.report = data;
      this.isLoaded = true;
      this.reportValueList = data.values.map((item) => {
        const label = this.toSentenceCasePipe.transform(item.reportLabel.name);
        return new ReportUpdateValue(item.reportLabelId, item.reportLabel.name, label, item.reportLabel.type, item.value);
      });
    }, error => {
    });
  }
  updateReport(reportValueList: ReportUpdateValue[]) {
    this.isUpdated = false;

    const labelModel = reportValueList.map((item) => {
      return new LabelModel(item.id, item.key, item.value);
    });
    const model = new UpdateRequestModel(
      this.report.reportType.name,
      this.report.driveType,
      this.report.reportType.id,
      this.reportId, labelModel
    );

    this.sub1 = this.reportService.updateReport(model).subscribe(() => {
      this.router.navigate(['reports']);
    }, error => {
    });
  }
  getHistory() {
    if (this.historyList.length === 0) {
      this.sub2 = this.reportService.getReportHistory(this.report.id).subscribe((data: ReportHistoryModel[]) => {
        this.historyList = data;
        this.showHistory = true;
      }, error => {
      });
    } else {
      this.showHistory = true;
    }
  }
  ngOnDestroy(): void {
    if (this.sub) { this.sub.unsubscribe(); }
    if (this.sub1) { this.sub1.unsubscribe(); }
    if (this.sub2) { this.sub2.unsubscribe(); }

  }
}
