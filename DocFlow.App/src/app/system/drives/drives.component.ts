import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { DocumentsService } from '../services/documents.service';
import { ItemSharedDriveModel } from '../../shared/models/item.shared-drive.model';

@Component({
  selector: 'app-drives',
  templateUrl: './drives.component.html',
  styleUrls: ['./drives.component.scss']
})
export class DrivesComponent implements OnInit, OnDestroy {

  isLoaded = false;
  model: ItemSharedDriveModel[] = null;

  sub1: Subscription;

  constructor(private documentsService: DocumentsService) { }

  ngOnInit() {
    this.sub1 = this.documentsService.getItemsInSharedDirectory().subscribe((data: ItemSharedDriveModel[]) => {
      this.model = data;
      this.isLoaded = true;
    }, error => {
      this.isLoaded = false;
    });
  }

  ngOnDestroy(): void {
    if (this.sub1) { this.sub1.unsubscribe(); }
  }
}
