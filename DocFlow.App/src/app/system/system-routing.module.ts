import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { SystemComponent } from './system.component';
import { AuthGuard } from '../shared/services/auth-guard';
import { ReportComponent } from './report/report.component';
import { ReportsComponent } from './reports/reports.component';
import { DrivesComponent } from './drives/drives.component';
import { ReportEditComponent } from './report/report-edit/report-edit.component';
import { HistoryDetailsComponent } from './report/history-details/history-details.component';

const routes: Routes = [
    {
        path: '', component: SystemComponent, canActivate: [AuthGuard], children: [
            { path: 'report', children: [
                { path: '', component: ReportComponent },
                { path: ':reportId', component: ReportEditComponent },
                { path: ':reportId/:mode', component: ReportEditComponent },
            ] },
            { path: 'history/:reportHistoryId', component: HistoryDetailsComponent },

            { path: 'reports', component: ReportsComponent, },
            { path: 'drives', component: DrivesComponent }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class SystemRoutingModule {
}
