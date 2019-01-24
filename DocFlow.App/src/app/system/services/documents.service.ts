import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { ReportRequestModel } from '../../shared/models/report-request.model';
import { DigitalSignatureModel } from '../../shared/models/digitalSignature.model';

@Injectable()
export class DocumentsService {
  constructor(public http: HttpClient) {
  }
  getReportLabels(reportTypeId: number): Observable<any> {
    return this.http.get(`api/report/getReportLabelsAndReportTypes?reportTypeId=${reportTypeId}`);
  }
  createFiles(nameFile: string, data: ReportRequestModel): Observable<any> {
    return this.http.post('api/document/createFiles', { name: nameFile, values: data });
  }

  createFilesInSharedDrive(nameFile: string, data: ReportRequestModel): Observable<any> {
    return this.http.post('api/document/createFilesInSharedDrive', { name: nameFile, values: data });
  }

  getDrive(): Observable<any> {
    return this.http.get('api/document/drive');
  }

  downloadReport(reportId: number): Observable<any> {
    return this.http.get(`api/document/download?reportId=${reportId}`, { responseType: 'arraybuffer' });
  }

  getItemsInSharedDirectory(): Observable<any> {
    return this.http.get('api/document/getItemsInSharedDirectory');
  }

  signedReport(reportId: number, model: DigitalSignatureModel): Observable<any> {
    return this.http.post(`api/document/digitalSignatureReport?reportId=${reportId}`, model);
  }
}
