import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { SystemComponent } from './system.component';
import { SystemRoutingModule } from './system-routing.module';
import { ReportComponent } from './report/report.component';
import { SharedModule } from '../shared/shared.module';
import { DocumentsService } from './services/documents.service';
import { ReportsComponent } from './reports/reports.component';
import { DocFlowIntercaptor } from '../shared/services/docflow.interceptor';
import { ReportService } from './services/report.service';
import { DrivesComponent } from './drives/drives.component';
import { GenerateReportComponent } from './report/generate-report/generate-report.component';
import { AlertService } from '../shared/services/alert.service';
import { ReportEditComponent } from './report/report-edit/report-edit.component';
import { UpdateReportValuesComponent } from './report/update-report-values/update-report-values.component';
import { PipesModule } from '../shared/pipes/pipes.module';
import { ToSentenceCasePipe } from '../shared/pipes/toSentenceCase.pipe';
import { HistoryDetailsComponent } from './report/history-details/history-details.component';
import { HistoryComponent } from './report/report-edit/history/history.component';

@NgModule({
  declarations: [
      SystemComponent,
      ReportComponent,
      ReportsComponent,
      DrivesComponent,
      ReportEditComponent,
      GenerateReportComponent,
      UpdateReportValuesComponent,
      HistoryComponent,
      HistoryDetailsComponent
      ],
  imports: [
    CommonModule,
    SharedModule,
    SystemRoutingModule,
    PipesModule
  ],
  providers: [
    DocumentsService,
    ReportService,
    AlertService,
    ToSentenceCasePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: DocFlowIntercaptor,
      multi: true
    }],
})
export class SystemModule { }
