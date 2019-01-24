import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { AuthService } from 'src/app/shared/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { StorageService } from 'src/app/shared/services/storage.service';
import { Subscription } from 'rxjs';
import { AuthUser } from 'src/app/shared/models/authUser.model';
import { Message } from '../../shared/models/message.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  form: FormGroup;
  sub1: Subscription;

  message: Message;

  isLoaded = true;

  constructor(private authServise: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private storageService: StorageService
  ) { }

  private showMessage(message: Message) {
    this.message = message;
  }

  ngOnInit() {
    this.message = new Message('danger', '');

    this.form = new FormGroup({
      'username': new FormControl(null, [Validators.required]),
      'password': new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    this.message = new Message('danger', '');
    this.isLoaded = false;

    const formData = this.form.value;
    const login = new AuthUser(formData.username, formData.password);
    this.sub1 = this.authServise.login(login)
      .subscribe((user: any) => {
        this.isLoaded = true;
        this.storageService.clearStorage();
        this.storageService.setItem('docflow_token', user.access_token);
        this.router.navigate(['']);
      },
      error => {
        this.isLoaded = true;
        this.showMessage({ text: 'Incorrect username or password', type: 'danger' });
      });
  }

  ngOnDestroy() {
    if (this.sub1) { this.sub1.unsubscribe(); }
  }
}
