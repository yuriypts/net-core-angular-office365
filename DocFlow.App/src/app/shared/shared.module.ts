import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HeaderComponent } from './components/header/header.component';
import { RouterModule } from '@angular/router';
import { AlertComponent } from './components/alert/alert.component';
import { LoaderComponent } from './components/loader/loader.component';
import { PipesModule } from './pipes/pipes.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SignReportModalComponent } from './components/modals/sign-report-modal/sign-report-modal.component';
import { ModalComponent } from './components/modals/modal.component';

@NgModule({
    imports: [ReactiveFormsModule, FormsModule, BrowserModule, RouterModule, PipesModule, ModalModule.forRoot()
    ],
    exports: [
        ReactiveFormsModule,
        FormsModule,
        PipesModule,
        HeaderComponent,
        AlertComponent,
        LoaderComponent,
        ModalComponent
    ],
    declarations: [HeaderComponent, AlertComponent, LoaderComponent, ModalComponent, SignReportModalComponent],
})

export class SharedModule {}
