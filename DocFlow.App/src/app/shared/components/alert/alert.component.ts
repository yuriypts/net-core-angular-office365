import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { AlertType, Alert } from '../../models/alert/alert.model';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit {
  alerts: Alert[] = [];

  constructor(private alertService: AlertService) { }
  ngOnInit() {
    this.alertService.getAlert().subscribe((alert: Alert) => {
        if (!alert) {
            // clear alerts when an empty alert is received
            this.alerts = [];
            return;
        }

        // add alert to array
        this.alerts.push(alert);
        // remove messages affter 5 seconds
        if (alert.type === AlertType.Success) {
            window.setTimeout(() => {
                this.alerts = this.alerts.filter(x => x !== alert);
            }, 5000);
        }
    });
}

removeAlert(alert: Alert) {
    this.alerts = this.alerts.filter(x => x !== alert);
}

cssClass(alert: Alert) {
    if (!alert) {
        return;
    }

    // return css class based on alert type
    switch (alert.type) {
        case AlertType.Success:
            return 'alert alert-success';
        case AlertType.Error:
            return 'alert alert-danger';
        case AlertType.Info:
            return 'alert alert-info';
        case AlertType.Warning:
            return 'alert alert-warning';
    }
}
}
