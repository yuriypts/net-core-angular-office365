import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ReportService } from '../../services/report.service';
import { ToSentenceCasePipe } from 'src/app/shared/pipes/toSentenceCase.pipe';
import { ReportUpdateValue } from 'src/app/shared/models/reportUpdateValue.model';

@Component({
  selector: 'app-history-details',
  templateUrl: './history-details.component.html',
  styleUrls: ['./history-details.component.scss']
})
export class HistoryDetailsComponent implements OnInit, OnDestroy {

  constructor(
    private activatedRoute: ActivatedRoute,
    private reportService: ReportService,
    private toSentenceCasePipe: ToSentenceCasePipe) { }
  reportHistoryId: number;
  historyValueList = [];
  reportValueList: ReportUpdateValue[] = [];
  sub: Subscription;
  isLoaded = false;

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: any) => {
      const { reportHistoryId } = params;

      if (reportHistoryId) {
        this.reportHistoryId = reportHistoryId;
      }
    });
    this.sub = this.reportService.getReportHistoryValues(this.reportHistoryId).subscribe((data: any) => {
      this.historyValueList = data;
      this.isLoaded = true;
      this.reportValueList = data.values.map((item) => {
        const label = this.toSentenceCasePipe.transform(item.reportLabel.name);
        return new ReportUpdateValue(item.reportLabel.id, item.reportLabel.name, label, item.reportLabel.type, item.newValue);
      });
    }, error => {
    });
  }
  ngOnDestroy(): void {
    if (this.sub) { this.sub.unsubscribe(); }
  }
}
