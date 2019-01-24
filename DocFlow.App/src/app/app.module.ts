import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { SystemComponent } from './system/system.component';
import { AuthComponent } from './auth/auth.component';
import { LoginComponent } from './auth/login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuard } from './shared/services/auth-guard';
import { AuthService } from './shared/services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { StorageService } from './shared/services/storage.service';
import { AuthModule } from './auth/auth.module';
import { SystemModule } from './system/system.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    SystemModule,
    AuthModule
  ],
  providers: [AuthGuard, AuthService, StorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
