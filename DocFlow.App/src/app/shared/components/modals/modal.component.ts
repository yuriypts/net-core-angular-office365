import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ModalType } from '../../models/enums/modalType.enum';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {
  @ViewChild('autoShownModal') private autoShownModal: ModalDirective;

  @Input() isOpen = false;
  @Input() title: string;
  @Input() data: any;
  @Input() type: any;

  @Output() hideEvent = new EventEmitter<any>();

  modalTypes = ModalType;
  constructor() { }

  ngOnInit() {
  }

  showModal(): void {
    this.isOpen = true;
  }

  hideModal(): void {
    this.autoShownModal.hide();
  }

  onHidden(data: any): void {
    this.isOpen = false;
    this.hideEvent.emit(data);

  }
}
