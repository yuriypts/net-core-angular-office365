import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-rouing.module';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
      LoginComponent,
      AuthComponent,
      ],
  imports: [
    CommonModule,
    SharedModule,
    AuthRoutingModule
  ],
  providers: [],
})
export class AuthModule { }
