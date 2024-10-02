import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CurrencyListComponent } from './components/currency-list/currency-list.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CurrencyHistoryComponent } from './components/currency-history/currency-history.component';

const routes: Routes = [
  { path: 'currencies', component: CurrencyListComponent }, 
  { path: 'currency-history/:code', component: CurrencyHistoryComponent },
  { path: 'login', component: LoginComponent },  
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: '/currencies', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
