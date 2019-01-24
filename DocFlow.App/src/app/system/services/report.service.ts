import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { ReportRequestModel } from '../../shared/models/report-request.model';
import { UpdateRequestModel } from '../../shared/models/update-request.model';

@Injectable()
export class ReportService {
  constructor(public http: HttpClient) {
  }

  getReportLabels(reportTypeId: number): Observable<any> {
    return this.http.get(`api/report/getReportLabels?reportTypeId=${reportTypeId}`);
  }

  getReportTypes(): Observable<any> {
    return this.http.get('api/report/getReportTypes');
  }

  generateReport(model: ReportRequestModel): Observable<any> {
    return this.http.post('api/report/generateReport', model);
  }
  getReports(): Observable<any> {
    return this.http.get('api/report/getReports');
  }

  getReport(reportId: number): Observable<any> {
    return this.http.get(`api/report/getReport?reportId=${reportId}`);
  }
  getReportHistory(reportId: number): Observable<any> {
    return this.http.get(`api/report/getReportHistory?reportId=${reportId}`);
  }
  getReportHistoryValues(reportHistoryId: number): Observable<any> {
    return this.http.get(`api/report/getReportHistoryValues?reportHistoryId=${reportHistoryId}`);
  }
  updateReport(model: UpdateRequestModel): Observable<any> {
    return this.http.post('api/report/updateReport', model);
  }
}
