import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DocumentsService } from 'src/app/system/services/documents.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DigitalSignatureModel } from 'src/app/shared/models/digitalSignature.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-report-modal',
  templateUrl: './sign-report-modal.component.html',
  styleUrls: ['./sign-report-modal.component.scss']
})
export class SignReportModalComponent implements OnInit {

  constructor(private documentsService: DocumentsService, private router: Router
    ) { }
  @Input() reportId: number;
  @Output() hideEvent = new EventEmitter<number>();
  isLoading = false;
  form: FormGroup;

  ngOnInit() {
    this.form = new FormGroup({
      'location': new FormControl(null, [Validators.required]),
      'reason': new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    this.isLoading = true;
    const { value } = this.form;
    const model = new DigitalSignatureModel(value.location, value.reason);
    this.documentsService.signedReport(this.reportId, model).subscribe(data => {
      this.router.navigate(['/drives']);
    });
  }
}
