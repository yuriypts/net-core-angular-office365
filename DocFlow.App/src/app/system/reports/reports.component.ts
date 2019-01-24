import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import saveAs from 'file-saver';
import { DocumentsService } from '../services/documents.service';
import { ReportService } from '../services/report.service';
import { Router } from '@angular/router';
import { ReportModel } from '../../shared/models/report.model';
import { formatDate } from '@angular/common';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DigitalSignatureModel } from '../../shared/models/digitalSignature.model';
import { ModalType } from 'src/app/shared/models/enums/modalType.enum';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit, OnDestroy {

  public isOpenModal = false;

  isLoaded = false;
  isDownload = true;
  model: ReportModel[] = [];
  selectedReportId: number;
  isReports = false;
  modalType = ModalType.SignReport;

  sub1: Subscription;
  sub: Subscription;
  constructor(
    private documentsService: DocumentsService,
    private reportService: ReportService,
    private router: Router
  ) { }

  ngOnInit() {

    this.sub1 = this.reportService.getReports().subscribe((data: ReportModel[]) => {
      this.model = data;
      this.isReports = data.length < 1 ? true : false;
      this.isLoaded = true;
    }, error => {
    });
  }

  downloadReport(reportId: number) {
    this.isDownload = false;
    this.sub = this.documentsService.downloadReport(reportId).subscribe(data => this.downloadFile(data));
  }

  downloadFile(data: any) {
    const blob = new Blob([data], { type: 'application/pdf' });
    const date = formatDate(Date.now(), 'dd/MM/yyyy', 'en');
    saveAs(blob, `Report-${date}.pdf`);
    this.isDownload = true;
  }

  openSignReportModal(reportId: number) {
    this.selectedReportId = reportId;
    this.isOpenModal = true;
  }

  hideSignReportModal() {
    this.selectedReportId = null;
    this.isOpenModal = false;
  }

  ngOnDestroy() {
    if (this.sub) { this.sub1.unsubscribe(); }
    if (this.sub1) { this.sub1.unsubscribe(); }
  }
}
