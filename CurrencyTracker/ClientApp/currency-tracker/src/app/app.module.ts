import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms'; 
import { HttpClientModule } from '@angular/common/http';

import { MatTableModule } from '@angular/material/table'; 
import { MatToolbarModule } from '@angular/material/toolbar'; 
import { MatButtonModule } from '@angular/material/button'; 
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { CurrencyListComponent } from './components/currency-list/currency-list.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { CurrencyHistoryComponent } from './components/currency-history/currency-history.component';
import { SubscriptionDialogComponent } from './components/subscription-dialog/subscription-dialog.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { AppRoutingModule } from './app-routing.module';

import {MatCardModule} from '@angular/material/card';

@NgModule({
  declarations: [
    SubscriptionDialogComponent,
    AppComponent,
    CurrencyListComponent,
    CurrencyHistoryComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    MatSlideToggleModule,
        MatFormFieldModule,
        MatInputModule,
        MatCardModule,
        FormsModule,
        MatButtonModule,
        BrowserModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        MatDialogModule,
        MatTableModule,
        MatToolbarModule,
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        FormsModule,
        MatSelectModule,
        AppRoutingModule,
        HttpClientModule
  ],
  providers: [
    provideAnimationsAsync(),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
